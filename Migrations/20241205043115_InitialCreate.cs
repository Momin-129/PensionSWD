using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PensionTemporary.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankFiles",
                columns: table => new
                {
                    Column1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column9 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column10 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column11 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column12 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column13 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "casesStopped",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: true),
                    refNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    districtLGDcode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    districtName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    parentage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    infRecvdINnicOn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    reasonForStoppingPension = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    letterFrom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    letterNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    letterDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    reason1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    reason2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    reason3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DepartmentFileModels",
                columns: table => new
                {
                    RefNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppliedDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppliedTehsil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PensionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfDisabilityAsPerUDID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentTehsil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Present_GP_Muncipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentVillage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfscCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPensionBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPensionBankBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPensionBankIFSCcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPensionAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TswoPreviousPensionYesNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TswoPreviousPensionScheme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionOnDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearActionOnDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthActionOnDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SanctionedByTask = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JK_ISSS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JanSugamDownloadCycle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "jkSWdeliveredMay30",
                columns: table => new
                {
                    uid = table.Column<int>(type: "int", nullable: true),
                    sno = table.Column<double>(type: "float", nullable: true),
                    districtUidForBank = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    districtNameForBank = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    refNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    lgdStateCode = table.Column<int>(type: "int", nullable: true),
                    divisionCode = table.Column<int>(type: "int", nullable: true),
                    divisionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    districtLGDCode = table.Column<int>(type: "int", nullable: true),
                    appliedDistrict = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TSWOtehsilCode = table.Column<int>(type: "int", nullable: true),
                    appliedTehsil = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Defines the name of the applicant"),
                    nameCorrectionByDirectorate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nameCorrectionDirLetter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nameCorrectionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    parentage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    dob = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    mobileNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pensionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pensionTypeShort = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    typeOfDisabilityAsPerUDID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    presentAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    presentDistrict = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    presentTehsil = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    present_GP_Muncipality = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    presentVillage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    bankName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    bankNameCorrectionByDirectorate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    bankNameCorrectionDirLetter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    bankNameCorrectionDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    branchName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    branchNameCorrectionByDirectorate = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    ifscCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Defines the Ifsc Code of the applicant"),
                    ifscCorrectionByDirectorate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ifscCorrectionDirLetter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ifscCorrectionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    bankFileGeneratedBeforeDirCorrection = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    accountNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Defines the account number of the applicant"),
                    accountNoCorrectionByDirectorate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    accountNoCorrectionDirLetter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    accountNoCorrectionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    previousPension = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    previousPensionBank = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    previousPensionBankBranch = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    previousPensionBankIFSCcode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    previousPensionAccountNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    tswoPreviousPensionYesNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    tswoPreviousPensionScheme = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    actionOnDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    yearActionOnDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    monthActionOnDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    actionOnDate_verified1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    yearActionOnDate_verified1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    monthActionOnDate_verified1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    reSanctionDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    currentStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sanctionedByTask = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    schemeSanctionedByDirKashmir = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    schemeSanctionedByDirJammu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    schemeSanctionedByDSWO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    JK_ISSS = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GOI_NSAP = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SCHEME_CLARIFICATION = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    leftOutCasesDueToSchemeClarification = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    duplicateBankAccountNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sharedWithDeptForVerification = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    eligibleForPension = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Defines whether applicant is eligible to receive pension or not"),
                    deptVerified = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    deptVerifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    aprilChk = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    aprilChk2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    aprilChk3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    chkIfscCodeUpdated = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    chkAccountNoUpdated = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    remarks2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    remarks3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    remarksForCorrectionInNameIFSCaccountNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nsapChk = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    janSugamDownloadCycle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    oldCDACbenificary = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    arrear_total_months = table.Column<int>(type: "int", nullable: true),
                    arrear_total_months_amt = table.Column<int>(type: "int", nullable: true),
                    arrear_bank_file_generated = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    arrear_bank_file_generated_date = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    arear_bank_file_bankPayment_ok = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    arear_bank_file_bankPayment_ok_date = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    arrear_March23_Minus_Feb23 = table.Column<int>(type: "int", nullable: true),
                    arrear_April23_Minus_March23 = table.Column<int>(type: "int", nullable: true),
                    arrear_May23_Minus_April23 = table.Column<int>(type: "int", nullable: true),
                    arrear_June23_Minus_May23 = table.Column<int>(type: "int", nullable: true),
                    arrear_July23_Minus_June23 = table.Column<int>(type: "int", nullable: true),
                    arrear_Aug23_Minus_July23 = table.Column<int>(type: "int", nullable: true),
                    arrear_Sep23_Minus_Aug23 = table.Column<int>(type: "int", nullable: true),
                    arrear_Oct23_Minus_Sep23 = table.Column<int>(type: "int", nullable: true),
                    arrear_Nov23_Minus_Oct23 = table.Column<int>(type: "int", nullable: true),
                    arrear_Dec23_Minus_Nov23 = table.Column<int>(type: "int", nullable: true),
                    arrear_Jan24_Minus_Dec23 = table.Column<int>(type: "int", nullable: true),
                    arrear_Feb24_Minus_Jan24 = table.Column<int>(type: "int", nullable: true),
                    arrear_Mar24_Minus_Feb24 = table.Column<int>(type: "int", nullable: true),
                    paymentOfYearFeb2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthFeb2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearFeb2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthFeb2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearMarch2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthMarch2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearMarch2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthMarch2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearApril2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthApril2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearApril2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthApril2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearMay2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthMay2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearMay2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthMay2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearJune2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthJune2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearJune2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthJune2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearJuly2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthJuly2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearJuly2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthJuly2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearAug2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthAug2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearAug2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthAug2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearSep2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthSep2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearSep2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthSep2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearOct2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthOct2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearOct2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthOct2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearNov2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthNov2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearNov2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthNov2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearDec2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthDec2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearDec2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthDec2023BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearJan2024 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthJan2024 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearJan2024BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthJan2024BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearFeb2024 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthFeb2024 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfYearFeb2024BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    paymentOfMonthFeb2024BankRes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    junePaymentMonth2ndBnkFile2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    octPaymentMonth2ndBnkFile2023 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    octPaymentMonth2ndBnkFile2023_2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    dummyCol = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "msDistrict",
                columns: table => new
                {
                    divisionCode = table.Column<int>(type: "int", nullable: true),
                    districtLgdCode = table.Column<int>(type: "int", nullable: true),
                    districtName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    districtNameForBank = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "msDivision_top500",
                columns: table => new
                {
                    stateCode = table.Column<int>(type: "int", nullable: true),
                    divisionCode = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    divisionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "msTSWOtehsil_top500",
                columns: table => new
                {
                    divisionCode = table.Column<int>(type: "int", nullable: true),
                    districtLGDCode = table.Column<int>(type: "int", nullable: true),
                    TSWOtehsilCode = table.Column<int>(type: "int", nullable: true),
                    TSWOtehsilName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    tswoOfficeName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "nsapData",
                columns: table => new
                {
                    sanction_order_no = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    state_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    state_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    district_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    district_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sub_district_municipal_area_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sub_district_municipal_area_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    gram_panchayat_ward_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    gram_panchayat_ward_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    village_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    village_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    beneficiary_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    parentage_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    bankaccount_no = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ifsc_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    scheme_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    duplicateAccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SearchCount",
                columns: table => new
                {
                    Count = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UpdateHistory",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdationDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateHistory", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UUID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    divisionCode = table.Column<int>(type: "int", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UUID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankFiles");

            migrationBuilder.DropTable(
                name: "casesStopped");

            migrationBuilder.DropTable(
                name: "DepartmentFileModels");

            migrationBuilder.DropTable(
                name: "jkSWdeliveredMay30");

            migrationBuilder.DropTable(
                name: "msDistrict");

            migrationBuilder.DropTable(
                name: "msDivision_top500");

            migrationBuilder.DropTable(
                name: "msTSWOtehsil_top500");

            migrationBuilder.DropTable(
                name: "nsapData");

            migrationBuilder.DropTable(
                name: "SearchCount");

            migrationBuilder.DropTable(
                name: "UpdateHistory");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
