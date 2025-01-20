using System.Data;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PensionTemporary.Models.Entities;

public class HelperFunction
{
    private readonly PensionContext dbContext;
    private readonly ILogger<HelperFunction> _logger;
    private readonly ValidationFunctions validation;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public HelperFunction(PensionContext dbContext, ILogger<HelperFunction> logger, IWebHostEnvironment webHostEnvironment, ValidationFunctions _validation)
    {
        this.dbContext = dbContext;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        validation = _validation;
    }

    public async Task<(string, string)> GetFilePath(IFormFile file)
    {
        string docPath = "";
        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        string uniqueName = Guid.NewGuid().ToString()[..6] + "_" + DateTime.Now.ToString("dd_MM_yyyy") + "_" + file?.FileName;
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        if (file != null && file.Length > 0)
        {
            string filePath = Path.Combine(uploadsFolder, uniqueName);
            _logger.LogInformation($"IN GET FILE PATH: {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            docPath = filePath;
        }

        return (docPath, uniqueName);
    }

    public void UpdateHistory(string refNo, string OldValue, string NewValue, string ipAddress, string username, string columnToEdit, string Reason, string filePath)
    {
        // Fetch existing update history record
        var updationHistory = dbContext.UpdateHistories.FirstOrDefault(uh => uh.RefNo == refNo);
        List<dynamic> updationDetails = new List<dynamic>();

        if (updationHistory != null && !string.IsNullOrEmpty(updationHistory.UpdationDetails))
        {
            // Deserialize existing update details if available
            updationDetails = JsonConvert.DeserializeObject<List<dynamic>>(updationHistory.UpdationDetails) ?? new List<dynamic>();
        }

        // Create new update detail
        var UpdationDetail = new
        {
            TableName = "jkSWdeliveredMay30",
            ColumnName = columnToEdit,
            OldValue = OldValue,
            NewValue = NewValue,
            BulkUpdate = !string.IsNullOrEmpty(filePath) ? 1 : 0,
            FileUsed = filePath,
            UpdatedBy = username,
            IpAddress = ipAddress,
            UpdatedAt = DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"),
            Reason = Reason,
        };

        if (updationHistory == null)
        {
            // If no history exists, create a new one
            var newHistory = new UpdateHistory
            {
                RefNo = refNo,
                UpdationDetails = JsonConvert.SerializeObject(new List<dynamic> { UpdationDetail })
            };

            dbContext.UpdateHistories.Add(newHistory);
        }
        else
        {
            // Append the new detail to the existing list and update the record
            updationDetails.Add(UpdationDetail);
            updationHistory.UpdationDetails = JsonConvert.SerializeObject(updationDetails);
        }

        // Save changes to the database
        dbContext.SaveChanges();
    }


    public dynamic UpdatedData(string RefNo, string AccountNo, string IfscCode, string EligibleForPension, string status, string Reason)
    {
        var updatedData = new
        {
            RefrenceNumber = RefNo,
            AccountNumber = AccountNo,
            IfscCode = IfscCode,
            EligibleForPension = EligibleForPension,
            Status = status,
            Reason = Reason
        };
        return updatedData;
    }


    public List<dynamic> UpdateEligibilityData(string filePath, int? divisionCode, string ipAddress, string username)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
        var newColumnNumber = worksheet.LastColumnUsed().ColumnNumber() + 1;
        worksheet.Cell(1, newColumnNumber).Value = "Status";


        var responseList = new List<dynamic>();

        foreach (var row in rows)
        {
            var eligible = new EligibleExcelModel
            {
                RefNo = row.Cell(1).Value.ToString(),
                AccountNo = row.Cell(2).Value.ToString(),
                IfscCode = row.Cell(3).Value.ToString(),
                OldEligibleForPension = row.Cell(4).Value.ToString(),
                NewEligibleForPension = row.Cell(5).Value.ToString(),
                Reason = row.Cell(6).Value.ToString()
            };

            int? refDivisionCode = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == eligible.RefNo).FirstOrDefault()!.DivisionCode;
            string? status = "";
            int rowsAffected = 0;
            var refNo = new SqlParameter("@refNo", eligible.RefNo);
            var accountNo = new SqlParameter("@accountNo", eligible.AccountNo);
            var ifscCode = new SqlParameter("@ifscCode", eligible.IfscCode);
            var eligibleForPension = new SqlParameter("@NewEligibleForPension", eligible.NewEligibleForPension);
            var reason = new SqlParameter("@Reason", eligible.Reason);
            var divisionCodeParam = new SqlParameter("@divisionCode", divisionCode);


            if (divisionCode != 0 && divisionCode != refDivisionCode)
            {
                status = "Unauthorized to access this.";
            }
            else
            {
                var errorList = validation.ValidateUpdateEligibility(eligible);
                _logger.LogInformation($"ERROR : {errorList.Count}");
                if (errorList.Count != 0)
                {
                    status = "Falied to update because, " + string.Join(", ", errorList);
                }
                else
                {
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateEligibility @refNo,@accountNo,@ifscCode,@NewEligibleForPension,@Reason", refNo, accountNo, ifscCode, eligibleForPension, reason);
                    status = "Updated Successfully.";

                    UpdateHistory(eligible.RefNo, eligible.OldEligibleForPension, eligible.NewEligibleForPension, ipAddress, username, "eligibleForPension", eligible.Reason, filePath);
                }

            }

            row.Cell(newColumnNumber).Value = status;
            responseList.Add(UpdatedData(eligible.RefNo, eligible.AccountNo, eligible.IfscCode, rowsAffected == 0 ? eligible.OldEligibleForPension : eligible.NewEligibleForPension, status, eligible.Reason));

        }
        workbook.SaveAs(stream);
        return responseList;
    }

    public List<dynamic> WeedoutCases(string filePath, int? divisionCode, string ipAddress, string username)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
        var newColumnNumber = worksheet.LastColumnUsed().ColumnNumber() + 1;
        worksheet.Cell(1, newColumnNumber).Value = "Status";


        var responseList = new List<dynamic>();

        foreach (var row in rows)
        {
            var weedout = new WeedoutExcelModel
            {
                RefNo = row.Cell(1).Value.ToString(),
                AccountNo = row.Cell(2).Value.ToString(),
                IfscCode = row.Cell(3).Value.ToString(),
                EligibleForPension = row.Cell(4).Value.ToString(),
                Reason = row.Cell(5).Value.ToString()
            };

            int? refDivisionCode = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == weedout.RefNo).FirstOrDefault()!.DivisionCode;
            string? status = "";
            int rowsAffected = 0;
            var refNo = new SqlParameter("@refNo", weedout.RefNo);
            var accountNo = new SqlParameter("@accountNo", weedout.AccountNo);
            var ifscCode = new SqlParameter("@ifscCode", weedout.IfscCode);
            var eligibleForPension = new SqlParameter("@NewEligibleForPension", weedout.EligibleForPension);
            var reason = new SqlParameter("@Reason", weedout.Reason);
            var divisionCodeParam = new SqlParameter("@divisionCode", divisionCode);


            if (divisionCode != 0 && divisionCode != refDivisionCode)
            {
                status = "Unauthorized to access this.";
            }
            else
            {
                var errorList = validation.ValidateWeedoutCase(weedout);
                _logger.LogInformation($"ERROR : {errorList.Count}");
                if (errorList.Count != 0)
                {
                    status = "Falied to update because, " + string.Join(", ", errorList);
                }
                else
                {
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateEligibility @refNo,@accountNo,@ifscCode,@NewEligibleForPension,@Reason", refNo, accountNo, ifscCode, eligibleForPension, reason);
                    status = "Updated Successfully.";

                    UpdateHistory(weedout.RefNo, "YES", weedout.EligibleForPension, ipAddress, username, "eligibleForPension", weedout.Reason, filePath);
                }

            }

            row.Cell(newColumnNumber).Value = status;
            responseList.Add(UpdatedData(weedout.RefNo, weedout.AccountNo, weedout.IfscCode, rowsAffected == 0 ? "YES" : weedout.EligibleForPension, status, weedout.Reason));

        }
        workbook.SaveAs(stream);
        return responseList;
    }

    public List<dynamic> UpdateAccountNumber(string filePath, int? divisionCode, string ipAddress, string username)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
        var newColumnNumber = worksheet.LastColumnUsed().ColumnNumber() + 1;
        worksheet.Cell(1, newColumnNumber).Value = "Status";

        var responseList = new List<dynamic>();

        foreach (var row in rows)
        {
            var account = new AccountNoExcelModel
            {
                RefNo = row.Cell(1).Value.ToString(),
                OldAccountNo = row.Cell(2).Value.ToString(),
                NewAccountNo = row.Cell(3).Value.ToString(),
                IfscCode = row.Cell(4).Value.ToString(),
                EligibleForPension = row.Cell(5).Value.ToString(),
                Reason = row.Cell(6).Value.ToString()
            };

            int? refDivisionCode = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == account.RefNo).FirstOrDefault()!.DivisionCode;
            string? status = "";
            var refNo = new SqlParameter("@refNo", account.RefNo);
            var oldaccountNo = new SqlParameter("@OldAccountNo", account.OldAccountNo);
            var newaccoutNo = new SqlParameter("@NewAccountNo", account.NewAccountNo);
            var ifscCode = new SqlParameter("@ifscCode", account.IfscCode);
            var reason = new SqlParameter("@Reason", account.Reason);
            var divisionCodeParam = new SqlParameter("@divisionCode", divisionCode);
            int rowsAffected = 0;

            if (divisionCode != 0 && divisionCode != refDivisionCode)
            {
                status = "Unauthorized to access this.";
            }
            else
            {
                var errorList = validation.ValidateUpdateAccountNo(account);

                if (errorList.Count != 0)
                {
                    status = "Falied to update because, " + string.Join(", ", errorList);
                }
                else
                {
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateAccountNo @refNo,@OldAccountNo,@NewAccountNo,@ifscCode,@Reason", refNo, oldaccountNo, newaccoutNo, ifscCode, reason);
                    status = "Updated Successfully.";

                    UpdateHistory(account.RefNo, account.OldAccountNo, account.NewAccountNo, ipAddress, username, "accountNo", account.Reason!, filePath);
                }
            }

            row.Cell(newColumnNumber).Value = status;


            responseList.Add(UpdatedData(account.RefNo, rowsAffected == 0 ? account.OldAccountNo : account.NewAccountNo, account.IfscCode, account.EligibleForPension, status, account.Reason!));


        }
        workbook.SaveAs(stream);
        return responseList;
    }

    public List<dynamic> UpdateIfscCode(string filePath, int? divisionCode, string ipAddress, string username)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
        var newColumnNumber = worksheet.LastColumnUsed().ColumnNumber() + 1;
        worksheet.Cell(1, newColumnNumber).Value = "Status";
        var responseList = new List<dynamic>();

        foreach (var row in rows)
        {
            var ifsc = new IfscExcelModel
            {
                RefNo = row.Cell(1).Value.ToString(),
                AccountNo = row.Cell(2).Value.ToString(),
                OldIfscCode = row.Cell(3).Value.ToString(),
                NewIfscCode = row.Cell(4).Value.ToString(),
                EligibleForPension = row.Cell(5).Value.ToString(),
                Reason = row.Cell(6).Value.ToString()
            };

            int? refDivisionCode = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == ifsc.RefNo).FirstOrDefault()!.DivisionCode;
            string? status = "";
            var refNo = new SqlParameter("@refNo", ifsc.RefNo);
            var accountNo = new SqlParameter("@AccountNo", ifsc.AccountNo);
            var oldifsc = new SqlParameter("@OldIfsc", ifsc.OldIfscCode);
            var newifsc = new SqlParameter("@NewIfsc", ifsc.NewIfscCode);
            var reason = new SqlParameter("@Reason", ifsc.Reason);
            var divisionCodeParam = new SqlParameter("@divisionCode", divisionCode);
            int rowsAffected = 0;

            if (divisionCode != 0 && divisionCode != refDivisionCode)
            {
                status = "Unauthorized to access this.";
            }
            else
            {
                var errorList = validation.ValidateUpdateIfscCode(ifsc);

                if (errorList.Count != 0)
                {
                    status = "Falied to update because, " + string.Join(", ", errorList);
                }
                else
                {
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateIfscCode @refNo,@AccountNo,@OldIfsc,@NewIfsc,@Reason", refNo, accountNo, oldifsc, newifsc, reason);

                    UpdateHistory(ifsc.RefNo, ifsc.OldIfscCode, ifsc.NewIfscCode, ipAddress, username, "ifscCode", ifsc.Reason!, filePath);
                    status = "Updated Successfully.";
                }
            }


            row.Cell(newColumnNumber).Value = status;
            responseList.Add(UpdatedData(ifsc.RefNo, ifsc.AccountNo, rowsAffected == 0 ? ifsc.OldIfscCode : ifsc.NewIfscCode, ifsc.EligibleForPension, status, ifsc.Reason!));


        }
        workbook.SaveAs(stream);
        return responseList;
    }

    public List<dynamic> UpdateApplicantName(string filePath, int? divisionCode, string ipAddress, string username)
    {
        using var stream = new FileStream(filePath, FileMode.Open);
        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
        var newColumnNumber = worksheet.LastColumnUsed().ColumnNumber() + 1;
        worksheet.Cell(1, newColumnNumber).Value = "Status";
        var responseList = new List<dynamic>();

        foreach (var row in rows)
        {
            var applicant = new NameExcelModel
            {
                RefNo = row.Cell(1).Value.ToString(),
                AccountNo = row.Cell(2).Value.ToString(),
                IfscCode = row.Cell(3).Value.ToString(),
                OldName = row.Cell(4).Value.ToString(),
                NewName = row.Cell(5).Value.ToString(),
                EligibleForPension = row.Cell(6).Value.ToString(),
                Reason = row.Cell(7).Value.ToString()
            };

            int? refDivisionCode = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == applicant.RefNo).FirstOrDefault()!.DivisionCode;
            string? status = "";
            var refNo = new SqlParameter("@refNo", applicant.RefNo);
            var accountNo = new SqlParameter("@AccountNo", applicant.AccountNo);
            var ifsc = new SqlParameter("@IfscCode", applicant.IfscCode);
            var oldname = new SqlParameter("@OldName", applicant.OldName);
            var newname = new SqlParameter("@NewName", applicant.NewName);
            var reason = new SqlParameter("@Reason", applicant.Reason);
            var divisionCodeParam = new SqlParameter("@divisionCode", divisionCode);
            int rowsAffected = 0;

            if (divisionCode != 0 && divisionCode != refDivisionCode)
            {
                status = "Unauthorized to access this.";
            }
            else
            {
                var errorList = validation.ValidateUpdateApplicantName(applicant);

                if (errorList.Count != 0)
                {
                    status = "Falied to update because, " + string.Join(", ", errorList);
                }
                else
                {
                    status = "Updated Successfully.";
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateApplicantName @refNo,@AccountNo,@IfscCode,@OldName,@NewName,@Reason", refNo, accountNo, ifsc, oldname, newname, reason);
                    UpdateHistory(applicant.RefNo, applicant.OldName, applicant.NewName, ipAddress, username, "applicantName", applicant.Reason!, filePath);
                }
            }

            row.Cell(newColumnNumber).Value = status;

            var updatedData = new
            {
                RefrenceNumber = applicant.RefNo,
                applicant = applicant.NewName,
                AccountNumber = applicant.AccountNo.PadLeft(16, '0'),
                IfscCode = applicant.IfscCode,
                EligibleForPension = applicant.EligibleForPension,
                Reason = applicant.Reason,
                Status = status,
            };
            responseList.Add(updatedData);

        }
        workbook.SaveAs(stream);
        return responseList;
    }



}