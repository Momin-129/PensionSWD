using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PensionTemporary.Models.Entities;

namespace PensionTemporary.Controllers.Users
{
    public partial class UserController
    {
        private IActionResult GetViewResult()
        {
            var count = JsonConvert.DeserializeObject<Dictionary<string, int>>(dbContext.SearchCounts.FirstOrDefault()!.Count!);
            _logger.LogInformation($"COUNT : {count!["total"]}");

            string? username = HttpContext.Session.GetString("username");
            var Result = new List<dynamic>();
            Result.Add(new { user = username, Total = count["total"], usercount = count[username!.ToString()] });
            return View(Result);
        }

        public IActionResult Record()
        {
            return GetViewResult();
        }

        [HttpPost]
        public IActionResult GetRecord([FromForm] IFormCollection form)
        {
            string searchType = form["searchType"].ToString();
            string inputNumber = form["inputNumber"].ToString();
            string? username = HttpContext.Session.GetString("username");


            int.TryParse(form["total"], out int total);
            int.TryParse(form[username!], out int usercount);

            _logger.LogInformation($"Total: {total} Usercount:{usercount}");

            var divisionCode = HttpContext.Session.GetInt32("divisionCode");


            List<JkSwdeliveredMay30>? record = null;

            if (searchType == "refId")
            {
                if (divisionCode.HasValue && divisionCode != 0)
                {
                    record = dbContext.JkSwdeliveredMay30s
                            .Where(u => u.RefNo == inputNumber && u.DivisionCode == divisionCode)
                            .ToList();
                }
                else
                {
                    record = dbContext.JkSwdeliveredMay30s
                            .Where(u => u.RefNo == inputNumber)
                            .ToList();
                }
            }

            if (searchType == "bankAccount")
            {
                if (divisionCode.HasValue && divisionCode != 0)
                {
                    record = dbContext.JkSwdeliveredMay30s
                            .Where(u => u.AccountNo == inputNumber && u.DivisionCode == divisionCode)
                            .ToList();
                }
                else
                {
                    record = dbContext.JkSwdeliveredMay30s
                            .Where(u => u.AccountNo == inputNumber)
                            .ToList();
                }
            }


            var totalcountParam = new SqlParameter("@totalCount", total);
            var usernameParam = new SqlParameter("@username", username);
            var usercountParam = new SqlParameter("@usernameCount", usercount);


            var resultCount = dbContext.SearchCounts.FromSqlRaw("EXEC UpdateCount @totalCount,@username,@usernameCount", totalcountParam, usernameParam, usercountParam).ToList()[0].Count;

            var result = JsonConvert.DeserializeObject<Dictionary<string, int>>(resultCount!);


            return Json(new { success = true, list = record, total = result!["total"], usercount = result[username!] });
        }

        [HttpGet]
        public IActionResult UpdateMultiple()
        {
            return GetViewResult();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMultiple([FromForm] IFormCollection form)
        {
            int? divisionCode = HttpContext.Session.GetInt32("divisionCode") ?? 0;
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString()!;
            string? username = HttpContext.Session.GetString("username");

            IFormFile? file = form.Files["excel"];

            bool isExcelFile = IsExcelFile(file!);

            if (!isExcelFile)
                return Json(new { status = false, response = "Not an excel file." });

            string? excelColumn = form["excelColumn"];
            if (file != null)
            {
                (string filePath, string uniqueName) = await _helper.GetFilePath(file);
                var list = new List<dynamic>();
                if (excelColumn == "eligibleForPension")
                {
                    list = _helper.UpdateEligibilityData(filePath, divisionCode, ipAddress, username!);
                }
                else if (excelColumn == "accountNo")
                {
                    list = _helper.UpdateAccountNumber(filePath, divisionCode, ipAddress, username!);
                }
                else if (excelColumn == "ifscCode")
                {
                    list = _helper.UpdateIfscCode(filePath, divisionCode, ipAddress, username!);
                }
                else if (excelColumn == "applicantName")
                {
                    list = _helper.UpdateApplicantName(filePath, divisionCode, ipAddress, username!);
                }
                else if (excelColumn == "weedout")
                {
                    list = _helper.WeedoutCases(filePath, divisionCode, ipAddress, username!);
                }
                else
                    return Json(new { status = false });

                return Json(new { status = true, response = list, fileName = uniqueName });

            }
            else
                return Json(new { status = false, response = "File is  empty." });
        }

        [HttpGet]
        public IActionResult UpdateIndividual()
        {

            return GetViewResult();
        }
        [HttpPost]
        public IActionResult UpdateIndividual([FromForm] IFormCollection form)
        {
            string? columnToEdit = form["column"].ToString();
            int? divisionCode = HttpContext.Session.GetInt32("divisionCode") ?? 0;
            var divisionCodeParam = new SqlParameter("@divisionCode", divisionCode);
            string username = HttpContext.Session.GetString("username") ?? "";
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString()!;


            int? refDivisionCode = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == form["refNo"].ToString()).FirstOrDefault()!.DivisionCode;

            if (divisionCode != 0 && divisionCode != refDivisionCode)
            {
                return Json(new { status = false, response = "Unauthorized to access this record." });
            }
            else
            {
                string? OldValue = "";
                string? NewValue = "";
                string? Reason = form["Reason"].ToString();
                string response = "";
                int rowsAffected = 0;
                bool status = true;

                if (columnToEdit == "applicantName")
                {
                    var applicant = new NameExcelModel
                    {
                        RefNo = form["refNo"].ToString(),
                        AccountNo = form["accountNo"].ToString(),
                        IfscCode = form["ifscCode"].ToString(),
                        OldName = form["OldapplicantName"].ToString(),
                        NewName = form["NewapplicantName"].ToString(),
                        EligibleForPension = form["eligibleForPension"].ToString(),
                        Reason = form["Reason"].ToString(),
                    };

                    var errorList = validation.ValidateUpdateApplicantName(applicant);

                    if (errorList.Count == 0)
                    {
                        OldValue = form["OldapplicantName"].ToString();
                        NewValue = form["NewapplicantName"].ToString();
                        var refNo = new SqlParameter("@RefNo", form["refNo"].ToString());
                        var accountNo = new SqlParameter("@AccountNo", form["accountNo"].ToString().PadLeft(16, '0'));
                        var ifsccode = new SqlParameter("@IfscCode", form["ifscCode"].ToString());
                        var oldname = new SqlParameter("@OldName", form["OldapplicantName"].ToString());
                        var newname = new SqlParameter("@NewName", form["NewapplicantName"].ToString());

                        rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateApplicantName @RefNo,@AccountNo,@IfscCode,@OldName,@NewName,@divisionCode", refNo, accountNo, ifsccode, oldname, newname, divisionCodeParam);
                        response = rowsAffected == 0 ? "Failed to update record." : "Updated " + columnToEdit + " value from " + OldValue + " to " + NewValue;
                    }
                    else
                    {
                        response = "Failure because " + string.Join(", ", errorList);
                        status = false;
                    }




                }
                else if (columnToEdit == "accountNo")
                {

                    var account = new AccountNoExcelModel
                    {
                        RefNo = form["OldapplicantName"].ToString(),
                        OldAccountNo = form["OldapplicantName"].ToString(),
                        NewAccountNo = form["OldapplicantName"].ToString(),
                        IfscCode = form["OldapplicantName"].ToString(),
                        EligibleForPension = form["OldapplicantName"].ToString(),
                        Reason = form["OldapplicantName"].ToString()
                    };

                    OldValue = form["OldaccountNo"].ToString();
                    NewValue = form["NewaccountNo"].ToString();
                    var refNo = new SqlParameter("@RefNo", form["refNo"].ToString());
                    var oldaccountno = new SqlParameter("@OldAccountNo", form["OldaccountNo"].ToString().PadLeft(16, '0'));
                    var newaccountno = new SqlParameter("@NewAccountNo", form["NewaccountNo"].ToString().PadLeft(16, '0'));
                    var ifsccode = new SqlParameter("@IfscCode", form["ifscCode"].ToString());
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateAccountNo @refNo,@OldAccountNo,@NewAccountNo,@ifscCode,@divisionCode", refNo, oldaccountno, newaccountno, ifsccode, divisionCodeParam);
                    response = rowsAffected == 0 ? "Failed to update record." : "Updated " + columnToEdit + " value from " + OldValue + " to " + NewValue;

                }
                else if (columnToEdit == "ifscCode")
                {
                    OldValue = form["OldifscCode"].ToString();
                    NewValue = form["NewifscCode"].ToString();
                    var refNo = new SqlParameter("@RefNo", form["refNo"].ToString());
                    var accountNo = new SqlParameter("@AccountNo", form["accountNo"].ToString().PadLeft(16, '0'));
                    var oldifsccode = new SqlParameter("@OldIfsc", form["OldifscCode"].ToString());
                    var newifsccode = new SqlParameter("@NewIfsc", form["NewifscCode"].ToString());
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateIfscCode @refNo,@AccountNo,@OldIfsc,@NewIfsc,@divisionCode", refNo, accountNo, oldifsccode, newifsccode, divisionCodeParam);
                    response = rowsAffected == 0 ? "Failed to update record." : "Updated " + columnToEdit + " value from " + OldValue + " to " + NewValue;

                }
                else if (columnToEdit == "eligibleForPension")
                {
                    OldValue = form["OldeligibleForPension"].ToString();
                    NewValue = form["NeweligibleForPension"].ToString();
                    var refNo = new SqlParameter("@RefNo", form["refNo"].ToString());
                    var accountNo = new SqlParameter("@AccountNo", form["accountNo"].ToString().PadLeft(16, '0'));
                    var ifsccode = new SqlParameter("@IfscCode", form["ifscCode"].ToString());
                    var neweligibleForPension = new SqlParameter("@NewEligibleForPension", form["NeweligibleForPension"].ToString());
                    rowsAffected = dbContext.Database.ExecuteSqlRaw("EXEC UpdateEligibility @refNo,@accountNo,@ifscCode,@NewEligibleForPension,@divisionCode", refNo, accountNo, ifsccode, neweligibleForPension, divisionCodeParam);
                    response = rowsAffected == 0 ? "Failed to update record." : "Updated " + columnToEdit + " value from " + OldValue + " to " + NewValue;

                }

                if (status)
                    _helper.UpdateHistory(form["refNo"].ToString(), OldValue, NewValue, ipAddress, username, columnToEdit, Reason, "");

                return Json(new { status = true, response });

            }

        }

        [HttpGet]
        public IActionResult GetFileUploaded()
        {
            var count = JsonConvert.DeserializeObject<Dictionary<string, int>>(dbContext.SearchCounts.FirstOrDefault()!.Count!);
            int? divisionCode = HttpContext.Session.GetInt32("divisionCode") ?? 0;
            string? username = HttpContext.Session.GetString("username");
            var user = new SqlParameter("@Username", username);
            var filesUploaded = dbContext.Set<FileUploaded>().FromSqlRaw("EXEC GetFilesUploaded @Username", user).ToList();
            var Result = new List<dynamic>();
            if (divisionCode == 0)
            {
                var allFilesUploaded = dbContext.UpdateHistories.ToList();
            }
            Result.Add(new { user = username, Total = count!["total"], usercount = count[username!.ToString()], filesUploaded });
            return View(Result);
        }

        [HttpGet]
        public IActionResult InsertNewCycle()
        {
            return GetViewResult();
        }

        [HttpPost]
        public async Task<IActionResult> InsertNewCycle([FromForm] IFormCollection form)
        {
            IFormFile? file = form.Files["excelFile"];

            bool isExcelFile = IsExcelFile(file!);

            if (!isExcelFile)
                return Json(new { status = false, response = "Not an excel file." });

            (string filePath, string uniqueName) = await _helper.GetFilePath(file!);

            var responseList = InsertNewCyclesFromExcel(filePath);

            return Json(new { status = true, filename = uniqueName, response = responseList });
        }

        [HttpGet]
        public IActionResult Reports()
        {
            return GetViewResult();
        }

        [HttpPost]
        public IActionResult GetHistory([FromForm] IFormCollection form)
        {
            int? divisionCode = HttpContext.Session.GetInt32("divisionCode") ?? 0;
            int? refDivisionCode = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == form["refNo"].ToString()).FirstOrDefault()!.DivisionCode;

            if (refDivisionCode != divisionCode && divisionCode != 0)
            {
                return Json(new { status = false, response = "NO RECORD FOUND." });
            }

            string refNo = form["refNo"].ToString();
            var historyList = dbContext.UpdateHistories.Where(u => u.RefNo == refNo).FirstOrDefault()!.UpdationDetails;
            return Json(new { status = true, history = historyList });
        }

        [HttpGet]
        public IActionResult GenerateBankFile()
        {
            var districts = dbContext.MsDistricts.ToList();
            var count = JsonConvert.DeserializeObject<Dictionary<string, int>>(dbContext.SearchCounts.FirstOrDefault()!.Count!);
            _logger.LogInformation($"COUNT : {count!["total"]}");

            string? username = HttpContext.Session.GetString("username");
            var Result = new List<dynamic>
            {
                new { user = username, Total = count["total"], usercount = count[username!.ToString()], districts }
            };
            return View(Result);
        }

        [HttpPost]
        public IActionResult GetListForBankPayamentFile([FromForm] IFormCollection form)
        {
            string username = HttpContext.Session.GetString("username") ?? "";
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString()!;
            var codes = form["district"].ToString().Split(' ');

            _logger.LogInformation($"codes 0: {codes[0]} Codes[1] :{codes[1]}");
            int.TryParse(codes[0], out int LGDCode);

            var bankDetails = GetBankDetails(codes[1]);

            var parameters = new[]
            {
                new SqlParameter("@districtLGDCode", LGDCode),
                new SqlParameter("@month", form["month"].ToString()),
                new SqlParameter("@year", form["year"].ToString()),
                new SqlParameter("@payingBankName", bankDetails.BankName),
                new SqlParameter("@payingAccountNo", bankDetails.AccountNo),
                new SqlParameter("@payingIfscCode", bankDetails.IfscCode)
            };

            var data = dbContext.BankFiles.FromSqlRaw("EXEC GetListforBankPaymentFile @districtLGDCode,@month,@year,@payingAccountNo,@payingIfscCode,@payingBankName", parameters).ToList();

            var duplicateRefNo = GetDuplicates(data, d => d.Column1);
            if (duplicateRefNo.Any())
                return Json(new { status = false, response = duplicateRefNo, duplicateType = "Reference Number" });

            var duplicateAccountNo = GetDuplicates(data, d => d.Column11);
            if (duplicateAccountNo.Any())
                return Json(new { status = false, response = duplicateAccountNo, duplicateType = "Account Number" });

            RemoveInvalidItems(data, username, ipAddress);

            string filePath = GenerateReport(data);

            return Json(new { status = true, filepath = filePath, filename = "/" + Path.GetFileName(filePath) });
        }


        [HttpGet]
        public IActionResult GenerateFileForDepartment()
        {
            return GetViewResult();
        }

        [HttpPost]
        public IActionResult GenerateFileForDepartment([FromForm] IFormCollection form)
        {
            var divisionCode = new SqlParameter("@divisionCode", form["division"].ToString() == "jammu" ? 1 : 2);
            var cycle = new SqlParameter("@janSugamDownloadCycle", form["cycle"].ToString());
            _logger.LogInformation($"Cycle :{form["cycle"].ToString()} DivisionCode:{(form["division"].ToString() == "jammu" ? 1 : 2)}");

            var result = dbContext.DepartmentFileModels.FromSqlRaw("EXEC FileDataForDepartment @divisionCode,@janSugamDownloadCycle", divisionCode, cycle).ToList();

            if (result.Count > 0)
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Sheet1");


                // Add column headers
                var headers = typeof(DepartmentFileModel).GetProperties().Select(p => p.Name).ToList();
                for (int i = 0; i < headers.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = headers[i];
                }

                // Write data
                for (int i = 0; i < result.Count; i++)
                {
                    var refNo = new SqlParameter("@refNo", result[i].RefNo);
                    dbContext.Database.ExecuteSqlRaw("EXEC SharedWithDepartment @refNo", refNo);
                    for (int j = 0; j < headers.Count; j++)
                    {
                        var propertyValue = result[i].GetType().GetProperty(headers[j])!.GetValue(result[i], null);
                        worksheet.Cell(i + 2, j + 1).Value = propertyValue?.ToString() ?? string.Empty;
                    }
                }


                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "reports");
                string dateTime = DateTime.Now.ToString("ddMMMyyyyhh_mm_ss");
                string filePath = uploadsFolder + "/" + dateTime + "_departmentFile.xlsx";
                workbook.SaveAs(filePath);

                return Json(new { status = true, filepath = uploadsFolder, filename = "/" + dateTime + "_departmentFile.xlsx" });
            }
            else
            {
                return Json(new { status = false, response = "No Record Found." });
            }
        }

        [HttpGet]
        public IActionResult DepartmentVerified()
        {
            return GetViewResult();
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentVerified([FromForm] IFormCollection form)
        {
            IFormFile? file = form.Files["excelFile"];

            bool isExcelFile = IsExcelFile(file!);

            if (!isExcelFile)
                return Json(new { status = false, response = "Not an excel file." });

            (string filePath, string uniqueName) = await _helper.GetFilePath(file!);
            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            var headerRow = worksheet.FirstRowUsed();
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
            var newColumnNumber = worksheet.LastColumnUsed().ColumnNumber() + 1;
            worksheet.Cell(1, newColumnNumber).Value = "Status";

            var columnNames = headerRow.CellsUsed().Select(cell => cell.Value.ToString().ToLower()).ToList();
            var requiredColumns = new List<string> { "refno", "accountno", "ifsccode", "name", "parentage", "applieddistrict" };
            var missingColumns = requiredColumns.Where(column => !columnNames.Contains(column)).ToList();


            if (missingColumns.Count != 0)
                return Json(new { status = false, response = "These fields are missing from the file " + string.Join(", ", missingColumns) });

            var columnMap = new Dictionary<string, int>();
            foreach (var cell in headerRow.CellsUsed())
            {
                columnMap[cell.Value.ToString()] = cell.Address.ColumnNumber;
            }
            foreach (var row in rows)
            {
                var refNo = new SqlParameter("@refNo", row.Cell(columnMap["RefNo"]).GetValue<string>());
                var accountNo = new SqlParameter("@accountNo", row.Cell(columnMap["AccountNo"]).GetValue<string>());
                var ifscCode = new SqlParameter("@ifscCode", row.Cell(columnMap["IfscCode"]).GetValue<string>());

                var updated = dbContext.Database.ExecuteSqlRaw("EXEC UpdateWithDepartmentFile @refNo,@accountNo,@ifscCode", refNo, accountNo, ifscCode);

                worksheet.Cell(row.RowNumber(), newColumnNumber).Value = updated > 0 ? "Update Successfully." : "Failed to update some conditions didn't matched.";
            }

            workbook.SaveAs(filePath);


            return Json(new { status = true, filename = uniqueName, filePath });
        }
    }
}