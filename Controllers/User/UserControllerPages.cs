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
