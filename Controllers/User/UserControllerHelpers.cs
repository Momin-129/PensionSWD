using System.Data;
using System.Dynamic;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.CustomXmlDataProperties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PensionTemporary.Models.Entities;

namespace PensionTemporary.Controllers.Users
{
    public partial class UserController
    {
        public string CopyAndRenameFile(string sourceFilePath)
        {

            var destinationFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "reports");

            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
            }
            string fileName = Path.GetFileName(sourceFilePath);
            var destinationFilePath = Path.Combine(destinationFolderPath, "Report_" + fileName);

            System.IO.File.Copy(sourceFilePath, destinationFilePath, true);

            return destinationFilePath;
        }

        [HttpPost]
        public IActionResult DownloadExcelFile([FromForm] IFormCollection form)
        {
            string filePath = form["excel"].ToString();
            if (filePath.Length != 0)
            {
                var request = _httpContextAccessor.HttpContext!.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
                string reportFilePath = CopyAndRenameFile(filePath);
                using var stream = new FileStream(reportFilePath, FileMode.Open);
                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);
                var lastRow = worksheet.LastRowUsed();
                var newRow = lastRow.RowNumber() + 3;

                worksheet.Cell(newRow, 1).Value = "Report Downloaded on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet.Cell(newRow, 2).Value = "URL: " + baseUrl;

                workbook.Save();
                return Json(new { status = true, filePath = reportFilePath });

            }

            return Json(new { status = false });
        }

        [HttpPost]
        public IActionResult DeleteReportFile([FromForm] IFormCollection form)
        {
            string filePath = form["reportFile"].ToString();
            _logger.LogInformation($"File Path to Delete: {filePath}");
            System.IO.File.Delete(filePath);
            return Json(new { status = true });
        }

        public bool IsExcelFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            // Check the file's MIME type
            var allowedMimeTypes = new[] { "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
            if (!allowedMimeTypes.Contains(file.ContentType))
            {
                return false;
            }

            // Check the file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (extension != ".xls" && extension != ".xlsx")
            {
                return false;
            }

            // Read the first few bytes to check the file signature
            using var reader = new BinaryReader(file.OpenReadStream());
            var headerBytes = reader.ReadBytes(8);
            // Check for Excel file signatures
            if (headerBytes.Length >= 4 && headerBytes[0] == 0x50 && headerBytes[1] == 0x4B &&
                (headerBytes[2] == 0x03 || headerBytes[2] == 0x04))
            {
                return true;
            }

            return false;
        }

        public List<dynamic> InsertNewCyclesFromExcel(string filePath)
        {
            // Load the Excel file
            using var stream = new FileStream(filePath, FileMode.Open);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);
            var firstRow = worksheet.FirstRowUsed();
            var columnMap = new Dictionary<string, int>();



            for (int i = 1; i <= firstRow.LastCellUsed().Address.ColumnNumber; i++)
            {
                var cellValue = firstRow.Cell(i).GetValue<string>();
                if (!string.IsNullOrWhiteSpace(cellValue))
                {
                    columnMap[cellValue.Trim()] = i;
                }
            }

            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
            var newColumnNumber = worksheet.LastColumnUsed().ColumnNumber() + 1;
            worksheet.Cell(1, newColumnNumber).Value = "Status";
            string status = "";
            var responseList = new List<dynamic>();

            foreach (var row in rows)
            {
                var model = new NewCycleExcelModel
                {
                    RefNo = row.Cell(columnMap["refNo"]).GetValue<string>(),
                    DistrictUidForBank = row.Cell(columnMap["districtUidForBank"]).GetValue<string>(),
                    DistrictNameForBank = row.Cell(columnMap["districtNameForBank"]).GetValue<string>(),
                    LgdStateCode = row.Cell(columnMap["lgdStateCode"]).GetValue<string>(),
                    DivisionCode = row.Cell(columnMap["divisionCode"]).GetValue<string>(),
                    DivisionName = row.Cell(columnMap["divisionName"]).GetValue<string>(),
                    DistrictLGDcode = row.Cell(columnMap["districtLGDcode"]).GetValue<string>(),
                    AppliedDistrict = row.Cell(columnMap["appliedDistrict"]).GetValue<string>(),
                    TSWOTehsilCode = row.Cell(columnMap["TSWOtehsilCode"]).GetValue<string>(),
                    AppliedTehsil = row.Cell(columnMap["appliedTehsil"]).GetValue<string>(),
                    Name = row.Cell(columnMap["name"]).GetValue<string>(),
                    Parentage = row.Cell(columnMap["parentage"]).GetValue<string>(),
                    DateOfBirth = row.Cell(columnMap["dateOfBirth"]).GetValue<string>(),
                    Gender = row.Cell(columnMap["gender"]).GetValue<string>(),
                    MobileNo = row.Cell(columnMap["mobileNo"]).GetValue<string>(),
                    Email = row.Cell(columnMap["email"]).GetValue<string>(),
                    PensionType = row.Cell(columnMap["pensionType"]).GetValue<string>(),
                    PensionTypeShort = row.Cell(columnMap["pensionTypeShort"]).GetValue<string>(),
                    TypeOfDisabilityAsPerUDID = row.Cell(columnMap["typeOfDisabilityAsPerUDID"]).GetValue<string>(),
                    PresentAddress = row.Cell(columnMap["presentAddress"]).GetValue<string>(),
                    PresentDistrict = row.Cell(columnMap["presentDistrict"]).GetValue<string>(),
                    PresentTehsil = row.Cell(columnMap["presentTehsil"]).GetValue<string>(),
                    Present_GP_Municipality = row.Cell(columnMap["present_GP_Muncipality"]).GetValue<string>(),
                    PresentVillage = row.Cell(columnMap["presentVillage"]).GetValue<string>(),
                    BankName = row.Cell(columnMap["bankName"]).GetValue<string>(),
                    BranchName = row.Cell(columnMap["branchName"]).GetValue<string>(),
                    IFSCCode = row.Cell(columnMap["ifscCode"]).GetValue<string>(),
                    AccountNo = row.Cell(columnMap["accountNo"]).GetValue<string>(),
                    PreviousPension = row.Cell(columnMap["previousPension"]).GetValue<string>(),
                    PreviousPensionBank = row.Cell(columnMap["previousPensionBank"]).GetValue<string>(),
                    PreviousPensionBankBranch = row.Cell(columnMap["previousPensionBankBranch"]).GetValue<string>(),
                    PreviousPensionBankIFSCCode = row.Cell(columnMap["previousPensionBankIFSCcode"]).GetValue<string>(),
                    PreviousPensionAccountNo = row.Cell(columnMap["previousPensionAccountNo"]).GetValue<string>(),
                    TSWOPreviousPensionYesNo = row.Cell(columnMap["tswoPreviousPensionYesNo"]).GetValue<string>(),
                    TSWOPreviousPensionScheme = row.Cell(columnMap["tswoPreviousPensionScheme"]).GetValue<string>(),
                    ActionOnDate = row.Cell(columnMap["actionOnDate"]).GetValue<string>(),
                    YearActionOnDate = row.Cell(columnMap["yearActionOnDate"]).GetValue<string>(),
                    MonthActionOnDate = row.Cell(columnMap["monthActionOnDate"]).GetValue<string>(),
                    CurrentStatus = row.Cell(columnMap["currentStatus"]).GetValue<string>(),
                    SanctionedByTask = row.Cell(columnMap["sanctionedByTask"]).GetValue<string>(),
                    SchemeSanctionedByDirKashmir = row.Cell(columnMap["schemeSanctionedByDirKashmir"]).GetValue<string>(),
                    SchemeSanctionedByDirJammu = row.Cell(columnMap["schemeSanctionedByDirJammu"]).GetValue<string>(),
                    SchemeSanctionedByDSWO = row.Cell(columnMap["schemeSanctionedByDSWO"]).GetValue<string>(),
                    JK_ISSS = row.Cell(columnMap["JK_ISSS"]).GetValue<string>(),
                    GOI_NSAP = row.Cell(columnMap["GOI_NSAP"]).GetValue<string>(),
                    DuplicateBankAccountNo = row.Cell(columnMap["duplicateBankAccountNo"]).GetValue<string>(),
                    EligibleForPension = row.Cell(columnMap["eligibleForPension"]).GetValue<string>(),
                    NSAPChk = row.Cell(columnMap["nsapChk"]).GetValue<string>(),
                    JanSugamDownloadCycle = row.Cell(columnMap["janSugamDownloadCycle"]).GetValue<string>(),
                };

                var properties = model.GetType().GetProperties();
                var parameters = new Dictionary<string, object>();
                foreach (var property in properties)
                {
                    parameters[property.Name] = property.GetValue(model)!;
                }

                var errorList = validation.ValidateNewCycleExcelModel(model);
                if (errorList.Count != 0)
                {
                    status = "Falied to update because, " + string.Join(", ", errorList);
                    parameters["Status"] = status;
                }
                else
                {
                    status = "Successfully Inserted.";
                    parameters["Status"] = status;
                }
                var isDuplicateRefNo = dbContext.JkSwdeliveredMay30s.Where(u => u.RefNo == model.RefNo).ToList();
                if (isDuplicateRefNo.Count == 0)
                {
                    var isDuplicateAccountNo = dbContext.JkSwdeliveredMay30s.Where(u => u.AccountNo == model.AccountNo).ToList();
                    status += isDuplicateAccountNo.Count > 0 ? ", Duplicate Account Number." : "";
                    // Create SQL parameters
                    var refNoParam = new SqlParameter("@refNo", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.RefNo) ? (object)DBNull.Value : model.RefNo };
                    var districtUidForBankParam = new SqlParameter("@districtUidForBank", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.DistrictUidForBank) ? (object)DBNull.Value : model.DistrictUidForBank };
                    var districtNameForBankParam = new SqlParameter("@districtNameForBank", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.DistrictNameForBank) ? (object)DBNull.Value : model.DistrictNameForBank };
                    var lgdStateCodeParam = new SqlParameter("@lgdStateCode", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.LgdStateCode) ? (object)DBNull.Value : model.LgdStateCode };
                    var divisionCodeParam = new SqlParameter("@divisionCode", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.DivisionCode) ? (object)DBNull.Value : model.DivisionCode };
                    var divisionNameParam = new SqlParameter("@divisionName", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.DivisionName) ? (object)DBNull.Value : model.DivisionName };
                    var districtLGDCodeParam = new SqlParameter("@districtLGDCode", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.DistrictLGDcode) ? (object)DBNull.Value : model.DistrictLGDcode };
                    var appliedDistrictParam = new SqlParameter("@appliedDistrict", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.AppliedDistrict) ? (object)DBNull.Value : model.AppliedDistrict };
                    var tSWOTehsilCodeParam = new SqlParameter("@tSWOTehsilCode", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.TSWOTehsilCode) ? (object)DBNull.Value : model.TSWOTehsilCode };
                    var appliedTehsilParam = new SqlParameter("@appliedTehsil", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.AppliedTehsil) ? (object)DBNull.Value : model.AppliedTehsil };
                    var nameParam = new SqlParameter("@name", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.Name) ? (object)DBNull.Value : model.Name };
                    var parentageParam = new SqlParameter("@parentage", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.Parentage) ? (object)DBNull.Value : model.Parentage };
                    var dobParam = new SqlParameter("@dateOfBirth", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.DateOfBirth) ? (object)DBNull.Value : model.DateOfBirth };
                    var genderParam = new SqlParameter("@gender", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.Gender) ? (object)DBNull.Value : model.Gender };
                    var mobileNoParam = new SqlParameter("@mobileNo", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.MobileNo) ? (object)DBNull.Value : model.MobileNo };
                    var emailParam = new SqlParameter("@email", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.Email) ? (object)DBNull.Value : model.Email };
                    var pensionTypeParam = new SqlParameter("@pensionType", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PensionType) ? (object)DBNull.Value : model.PensionType };
                    var pensionTypeShortParam = new SqlParameter("@pensionTypeShort", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PensionTypeShort) ? (object)DBNull.Value : model.PensionTypeShort };
                    var typeOfDisabilityAsPerUDIDParam = new SqlParameter("@typeOfDisabilityAsPerUDID", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.TypeOfDisabilityAsPerUDID) ? (object)DBNull.Value : model.TypeOfDisabilityAsPerUDID };
                    var presentAddressParam = new SqlParameter("@presentAddress", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PresentAddress) ? (object)DBNull.Value : model.PresentAddress };
                    var presentDistrictParam = new SqlParameter("@presentDistrict", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PresentDistrict) ? (object)DBNull.Value : model.PresentDistrict };
                    var presentTehsilParam = new SqlParameter("@presentTehsil", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PresentTehsil) ? (object)DBNull.Value : model.PresentTehsil };
                    var present_GP_MuncipalityParam = new SqlParameter("@present_GP_Muncipality", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.Present_GP_Municipality) ? (object)DBNull.Value : model.Present_GP_Municipality };
                    var presentVillageParam = new SqlParameter("@presentVillage", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PresentVillage) ? (object)DBNull.Value : model.PresentVillage };
                    var bankNameParam = new SqlParameter("@bankName", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.BankName) ? (object)DBNull.Value : model.BankName };
                    var branchNameParam = new SqlParameter("@branchName", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.BranchName) ? (object)DBNull.Value : model.BranchName };
                    var ifscCodeParam = new SqlParameter("@ifscCode", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.IFSCCode) ? (object)DBNull.Value : model.IFSCCode };
                    var accountNoParam = new SqlParameter("@accountNo", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.AccountNo) ? (object)DBNull.Value : model.AccountNo };
                    var previousPensionParam = new SqlParameter("@previousPension", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PreviousPension) ? (object)DBNull.Value : model.PreviousPension };
                    var previousPensionBankParam = new SqlParameter("@previousPensionBank", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PreviousPensionBank) ? (object)DBNull.Value : model.PreviousPensionBank };
                    var previousPensionBankBranchParam = new SqlParameter("@previousPensionBankBranch", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PreviousPensionBankBranch) ? (object)DBNull.Value : model.PreviousPensionBankBranch };
                    var previousPensionBankIFSCcodeParam = new SqlParameter("@previousPensionBankIFSCcode", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PreviousPensionBankIFSCCode) ? (object)DBNull.Value : model.PreviousPensionBankIFSCCode };
                    var previousPensionAccountNoParam = new SqlParameter("@previousPensionAccountNo", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.PreviousPensionAccountNo) ? (object)DBNull.Value : model.PreviousPensionAccountNo };
                    var tswoPreviousPensionYesNoParam = new SqlParameter("@tswoPreviousPensionYesNo", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.TSWOPreviousPensionYesNo) ? (object)DBNull.Value : model.TSWOPreviousPensionYesNo };
                    var tswoPreviousPensionSchemeParam = new SqlParameter("@tswoPreviousPensionScheme", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.TSWOPreviousPensionScheme) ? (object)DBNull.Value : model.TSWOPreviousPensionScheme };
                    var actionOnDateParam = new SqlParameter("@actionOnDate", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.ActionOnDate) ? (object)DBNull.Value : model.ActionOnDate };
                    var yearActionOnDateParam = new SqlParameter("@yearActionOnDate", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.YearActionOnDate) ? (object)DBNull.Value : model.YearActionOnDate };
                    var monthActionOnDateParam = new SqlParameter("@monthActionOnDate", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.MonthActionOnDate) ? (object)DBNull.Value : model.MonthActionOnDate };
                    var currentStatusParam = new SqlParameter("@currentStatus", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.CurrentStatus) ? (object)DBNull.Value : model.CurrentStatus };
                    var sanctionedByTaskParam = new SqlParameter("@sanctionedByTask", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.SanctionedByTask) ? (object)DBNull.Value : model.SanctionedByTask };
                    var schemeSanctionedByDirKashmirParam = new SqlParameter("@schemeSanctionedByDirKashmir", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.SchemeSanctionedByDirKashmir) ? (object)DBNull.Value : model.SchemeSanctionedByDirKashmir };
                    var schemeSanctionedByDirJammuParam = new SqlParameter("@schemeSanctionedByDirJammu", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.SchemeSanctionedByDirJammu) ? (object)DBNull.Value : model.SchemeSanctionedByDirJammu };
                    var schemeSanctionedByDSWOParam = new SqlParameter("@schemeSanctionedByDSWO", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.SchemeSanctionedByDSWO) ? (object)DBNull.Value : model.SchemeSanctionedByDSWO };
                    var JK_ISSSParam = new SqlParameter("@JK_ISSS", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.JK_ISSS) ? (object)DBNull.Value : model.JK_ISSS };
                    var GOI_NSAPParam = new SqlParameter("@GOI_NSAP", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.GOI_NSAP) ? (object)DBNull.Value : model.GOI_NSAP };
                    var duplicateBankAccountNoParam = new SqlParameter("@duplicateBankAccountNo", SqlDbType.NVarChar) { Value = isDuplicateAccountNo.Any() ? "YES" : (string.IsNullOrEmpty(model.DuplicateBankAccountNo) ? (object)DBNull.Value : model.DuplicateBankAccountNo) };
                    var eligibleForPensionParam = new SqlParameter("@eligibleForPension", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.EligibleForPension) ? (object)DBNull.Value : model.EligibleForPension };
                    var nsapChkParam = new SqlParameter("@nsapChk", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.NSAPChk) ? (object)DBNull.Value : model.NSAPChk };
                    var janSugamDownloadCycleParam = new SqlParameter("@janSugamDownloadCycle", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(model.JanSugamDownloadCycle) ? (object)DBNull.Value : model.JanSugamDownloadCycle };



                    // Define the stored procedure call
                    string storedProcedure = "InsertNewCycles @refNo, @districtUidForBank, @districtNameForBank, @lgdStateCode, @divisionCode, " +
                                              "@divisionName, @districtLGDcode, @appliedDistrict, @TSWOtehsilCode, @appliedTehsil, @name, " +
                                              "@parentage, @dateOfBirth, @gender, @mobileNo, @email, @pensionType, @pensionTypeShort, " +
                                              "@typeOfDisabilityAsPerUDID, @presentAddress, @presentDistrict, @presentTehsil, " +
                                              "@present_GP_Muncipality, @presentVillage, @bankName, @branchName, @ifscCode, @accountNo, " +
                                              "@previousPension, @previousPensionBank, @previousPensionBankBranch, @previousPensionBankIFSCcode, " +
                                              "@previousPensionAccountNo, @tswoPreviousPensionYesNo, @tswoPreviousPensionScheme, @actionOnDate, " +
                                              "@yearActionOnDate, @monthActionOnDate, @currentStatus, @sanctionedByTask, @schemeSanctionedByDirKashmir, " +
                                              "@schemeSanctionedByDirJammu, @schemeSanctionedByDSWO, @JK_ISSS, @GOI_NSAP, @duplicateBankAccountNo, " +
                                              "@eligibleForPension, @nsapChk, @janSugamDownloadCycle";

                    // Execute the stored procedure with SQL parameters
                    dbContext.Database.ExecuteSqlRaw(storedProcedure,
                        refNoParam, districtUidForBankParam, districtNameForBankParam, lgdStateCodeParam, divisionCodeParam,
                        divisionNameParam, districtLGDCodeParam, appliedDistrictParam, tSWOTehsilCodeParam, appliedTehsilParam,
                        nameParam, parentageParam, dobParam, genderParam, mobileNoParam, emailParam, pensionTypeParam,
                        pensionTypeShortParam, typeOfDisabilityAsPerUDIDParam, presentAddressParam, presentDistrictParam,
                        presentTehsilParam, present_GP_MuncipalityParam, presentVillageParam, bankNameParam, branchNameParam,
                        ifscCodeParam, accountNoParam, previousPensionParam, previousPensionBankParam, previousPensionBankBranchParam,
                        previousPensionBankIFSCcodeParam, previousPensionAccountNoParam, tswoPreviousPensionYesNoParam,
                        tswoPreviousPensionSchemeParam, actionOnDateParam, yearActionOnDateParam, monthActionOnDateParam,
                        currentStatusParam, sanctionedByTaskParam, schemeSanctionedByDirKashmirParam, schemeSanctionedByDirJammuParam,
                        schemeSanctionedByDSWOParam, JK_ISSSParam, GOI_NSAPParam, duplicateBankAccountNoParam, eligibleForPensionParam,
                        nsapChkParam, janSugamDownloadCycleParam);

                }
                else
                {
                    status = "Not Inserted, duplicate reference number.";
                    parameters["Status"] = status;
                }

                worksheet.Cell(row.RowNumber(), newColumnNumber).Value = status;
                responseList.Add(parameters);

            }

            workbook.SaveAs(stream);
            return responseList;
        }


        [HttpPost]
        public IActionResult UpdateEligibilityBankFile([FromForm] IFormCollection form)
        {
            var duplicates = JsonConvert.DeserializeObject<string[]>(form["duplicates"].ToString())!;
            string username = HttpContext.Session.GetString("username") ?? "";
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString()!;

            foreach (var duplicate in duplicates)
            {
                var parts = duplicate.Split(' ');
                string refNo = parts[0];
                string accountNo = parts[2];
                string ifscCode = parts[1];

                _logger.LogInformation($"DUPLICATE : {refNo} {accountNo} {ifscCode}");

                dbContext.Database.ExecuteSqlRaw("EXEC UpdateEligibilityBankFile @refNo,@accountNo,@ifscCode", new SqlParameter("@refNo", refNo), new SqlParameter("@accountNo", accountNo), new SqlParameter("@ifscCode", ifscCode));
                _helper.UpdateHistory(refNo, "YES", "NO", ipAddress, username, "eligibleForPension", "Already present in Nasp", "");
            }

            return Json(new { status = true });
        }

        private (string BankName, string AccountNo, string IfscCode) GetBankDetails(string code)
        {
            if (code == "jammu")
            {
                return ("THE JAMMU AND KASHMIR BANK LTD", "0022010200000048", "JAKA0ERAILH");
            }
            else
            {
                return ("THE JAMMU AND KASHMIR BANK LTD", "0367010200000520", "JAKA0TANKEE");
            }
        }

        private List<IGrouping<TKey, TSource>> GetDuplicates<TSource, TKey>(List<TSource> data, Func<TSource, TKey> keySelector)
        {
            return data.GroupBy(keySelector).Where(g => g.Count() > 1).ToList();
        }

        private void RemoveInvalidItems(List<BankFile> data, string username, string ipAddress)
        {
            foreach (var item in data)
            {
                var isPresentInNasp = dbContext.NsapData.Any(nasp => nasp.BankaccountNo == item.Column11);
                var isPresentInCaseClosed = dbContext.CasesStoppeds.Any(closed => closed.RefNo == item.Column1);

                if (isPresentInNasp || isPresentInCaseClosed)
                {
                    dbContext.Database.ExecuteSqlRaw("EXEC UpdateEligibilityBankFile @refNo,@accountNo,@ifscCode",
                        new SqlParameter("@refNo", item.Column1),
                        new SqlParameter("@accountNo", item.Column11),
                        new SqlParameter("@ifscCode", item.Column10));

                    string reason = isPresentInNasp ? "Already present in Nasp" : "Present in cases closed table.";
                    _helper.UpdateHistory(item.Column1, "YES", "NO", ipAddress, username, "eligibleForPension", reason, "");
                }
            }
        }

        private string GenerateReport(List<BankFile> data)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            for (int i = 1; i <= 12; i++)
            {
                worksheet.Cell(1, i).Value = "column" + i;
            }

            int row = 2;
            foreach (var item in data)
            {
                for (int i = 1; i <= 12; i++)
                {
                    worksheet.Cell(row, i).Value = item.GetType().GetProperty($"Column{i}")?.GetValue(item)?.ToString();
                }
                row++;
            }

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "reports");
            string dateTime = DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss");
            string filePath = Path.Combine(uploadsFolder, $"{dateTime}_bankFile.xlsx");

            workbook.SaveAs(filePath);

            return filePath;
        }
    }
}