using System.Data;
using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        var TableName = new SqlParameter("@TableName", "jkSWdeliveredMay30");
        var ColumnName = new SqlParameter("@ColumnName", columnToEdit);
        var refNoParam = new SqlParameter("@refNo", refNo);
        var OldValueParam = new SqlParameter("@OldValue", OldValue);
        var NewValueParam = new SqlParameter("@NewValue", NewValue);
        var BulkUpdate = new SqlParameter("@BulkUpdate", filePath.Length != 0 ? 1 : 0);
        var FileUsed = new SqlParameter("@FileUsed", filePath);
        var UpdatedBy = new SqlParameter("@UpdatedBy", username);
        var IpAddress = new SqlParameter("@IpAddress", ipAddress);
        var UpdatedAt = new SqlParameter("@UpdatedAt", DateTime.Now.ToString());
        var ReasonParam = new SqlParameter("@Reason", Reason);
        var descriptionParameter = new SqlParameter("@Description", SqlDbType.NVarChar, -1)
        {
            Direction = ParameterDirection.Output
        };


        dbContext.Database.ExecuteSqlRaw("EXEC UpdateHistoryTable @TableName, @ColumnName, @RefNo, @OldValue, @NewValue,@BulkUpdate,@FileUsed,@UpdatedBy, @IpAddress, @UpdatedAt, @Reason, @Description OUTPUT", TableName, ColumnName, refNoParam, OldValueParam, NewValueParam, BulkUpdate, FileUsed, UpdatedBy, IpAddress, UpdatedAt, ReasonParam, descriptionParameter);
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
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateEligibility @refNo,@accountNo,@ifscCode,@NewEligibleForPension,@divisionCode", refNo, accountNo, ifscCode, eligibleForPension, divisionCodeParam);
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
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateEligibility @refNo,@accountNo,@ifscCode,@NewEligibleForPension,@divisionCode", refNo, accountNo, ifscCode, eligibleForPension, divisionCodeParam);
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
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateAccountNo @refNo,@OldAccountNo,@NewAccountNo,@ifscCode,@divisionCode", refNo, oldaccountNo, newaccoutNo, ifscCode, divisionCodeParam);
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
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateIfscCode @refNo,@AccountNo,@OldIfsc,@NewIfsc,@divisionCode", refNo, accountNo, oldifsc, newifsc, divisionCodeParam);

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
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateApplicantName @refNo,@AccountNo,@IfscCode,@OldName,@NewName,@divisionCode", refNo, accountNo, ifsc, oldname, newname, divisionCodeParam);
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
                Status = status,
            };
            responseList.Add(updatedData);

        }
        workbook.SaveAs(stream);
        return responseList;
    }
}