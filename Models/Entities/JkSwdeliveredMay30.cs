using System;
using System.Collections.Generic;

namespace PensionTemporary.Models.Entities;

public partial class JkSwdeliveredMay30
{
    public int? Uid { get; set; }

    public double? Sno { get; set; }

    public string? DistrictUidForBank { get; set; }

    public string? DistrictNameForBank { get; set; }

    public string? RefNo { get; set; }

    public int? LgdStateCode { get; set; }

    public int? DivisionCode { get; set; }

    public string? DivisionName { get; set; }

    public int? DistrictLgdcode { get; set; }

    public string? AppliedDistrict { get; set; }

    public int? TswotehsilCode { get; set; }

    public string? AppliedTehsil { get; set; }

    /// <summary>
    /// Defines the name of the applicant
    /// </summary>
    public string? Name { get; set; }

    public string? NameCorrectionByDirectorate { get; set; }

    public string? NameCorrectionDirLetter { get; set; }

    public DateTime? NameCorrectionDate { get; set; }

    public string? Parentage { get; set; }

    public string? Dob { get; set; }

    public string? Gender { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? PensionType { get; set; }

    public string? PensionTypeShort { get; set; }

    public string? TypeOfDisabilityAsPerUdid { get; set; }

    public string? PresentAddress { get; set; }

    public string? PresentDistrict { get; set; }

    public string? PresentTehsil { get; set; }

    public string? PresentGpMuncipality { get; set; }

    public string? PresentVillage { get; set; }

    public string? BankName { get; set; }

    public string? BankNameCorrectionByDirectorate { get; set; }

    public string? BankNameCorrectionDirLetter { get; set; }

    public string? BankNameCorrectionDate { get; set; }

    public string? BranchName { get; set; }

    public string? BranchNameCorrectionByDirectorate { get; set; }

    /// <summary>
    /// Defines the Ifsc Code of the applicant
    /// </summary>
    public string? IfscCode { get; set; }

    public string? IfscCorrectionByDirectorate { get; set; }

    public string? IfscCorrectionDirLetter { get; set; }

    public DateTime? IfscCorrectionDate { get; set; }

    public string? BankFileGeneratedBeforeDirCorrection { get; set; }

    /// <summary>
    /// Defines the account number of the applicant
    /// </summary>
    public string? AccountNo { get; set; }

    public string? AccountNoCorrectionByDirectorate { get; set; }

    public string? AccountNoCorrectionDirLetter { get; set; }

    public DateTime? AccountNoCorrectionDate { get; set; }

    public string? PreviousPension { get; set; }

    public string? PreviousPensionBank { get; set; }

    public string? PreviousPensionBankBranch { get; set; }

    public string? PreviousPensionBankIfsccode { get; set; }

    public string? PreviousPensionAccountNo { get; set; }

    public string? TswoPreviousPensionYesNo { get; set; }

    public string? TswoPreviousPensionScheme { get; set; }

    public string? ActionOnDate { get; set; }

    public string? YearActionOnDate { get; set; }

    public string? MonthActionOnDate { get; set; }

    public string? ActionOnDateVerified1 { get; set; }

    public string? YearActionOnDateVerified1 { get; set; }

    public string? MonthActionOnDateVerified1 { get; set; }

    public string? ReSanctionDate { get; set; }

    public string? CurrentStatus { get; set; }

    public string? SanctionedByTask { get; set; }

    public string? SchemeSanctionedByDirKashmir { get; set; }

    public string? SchemeSanctionedByDirJammu { get; set; }

    public string? SchemeSanctionedByDswo { get; set; }

    public string? JkIsss { get; set; }

    public string? GoiNsap { get; set; }

    public string? SchemeClarification { get; set; }

    public string? LeftOutCasesDueToSchemeClarification { get; set; }

    public string? DuplicateBankAccountNo { get; set; }

    public string? SharedWithDeptForVerification { get; set; }

    /// <summary>
    /// Defines whether applicant is eligible to receive pension or not
    /// </summary>
    public string? EligibleForPension { get; set; }

    public string? DeptVerified { get; set; }

    public DateTime? DeptVerifiedDate { get; set; }

    public string? AprilChk { get; set; }

    public string? AprilChk2 { get; set; }

    public string? AprilChk3 { get; set; }

    public string? ChkIfscCodeUpdated { get; set; }

    public string? ChkAccountNoUpdated { get; set; }

    public string? Reason { get; set; }

    public string? Remarks { get; set; }

    public string? Remarks2 { get; set; }

    public string? Remarks3 { get; set; }

    public string? RemarksForCorrectionInNameIfscaccountNo { get; set; }

    public string? NsapChk { get; set; }

    public string? JanSugamDownloadCycle { get; set; }

    public string? OldCdacbenificary { get; set; }

    public int? ArrearTotalMonths { get; set; }

    public int? ArrearTotalMonthsAmt { get; set; }

    public string? ArrearBankFileGenerated { get; set; }

    public string? ArrearBankFileGeneratedDate { get; set; }

    public string? ArearBankFileBankPaymentOk { get; set; }

    public string? ArearBankFileBankPaymentOkDate { get; set; }

    public int? ArrearMarch23MinusFeb23 { get; set; }

    public int? ArrearApril23MinusMarch23 { get; set; }

    public int? ArrearMay23MinusApril23 { get; set; }

    public int? ArrearJune23MinusMay23 { get; set; }

    public int? ArrearJuly23MinusJune23 { get; set; }

    public int? ArrearAug23MinusJuly23 { get; set; }

    public int? ArrearSep23MinusAug23 { get; set; }

    public int? ArrearOct23MinusSep23 { get; set; }

    public int? ArrearNov23MinusOct23 { get; set; }

    public int? ArrearDec23MinusNov23 { get; set; }

    public int? ArrearJan24MinusDec23 { get; set; }

    public int? ArrearFeb24MinusJan24 { get; set; }

    public int? ArrearMar24MinusFeb24 { get; set; }

    public string? PaymentOfYearFeb2023 { get; set; }

    public string? PaymentOfMonthFeb2023 { get; set; }

    public string? PaymentOfYearFeb2023BankRes { get; set; }

    public string? PaymentOfMonthFeb2023BankRes { get; set; }

    public string? PaymentOfYearMarch2023 { get; set; }

    public string? PaymentOfMonthMarch2023 { get; set; }

    public string? PaymentOfYearMarch2023BankRes { get; set; }

    public string? PaymentOfMonthMarch2023BankRes { get; set; }

    public string? PaymentOfYearApril2023 { get; set; }

    public string? PaymentOfMonthApril2023 { get; set; }

    public string? PaymentOfYearApril2023BankRes { get; set; }

    public string? PaymentOfMonthApril2023BankRes { get; set; }

    public string? PaymentOfYearMay2023 { get; set; }

    public string? PaymentOfMonthMay2023 { get; set; }

    public string? PaymentOfYearMay2023BankRes { get; set; }

    public string? PaymentOfMonthMay2023BankRes { get; set; }

    public string? PaymentOfYearJune2023 { get; set; }

    public string? PaymentOfMonthJune2023 { get; set; }

    public string? PaymentOfYearJune2023BankRes { get; set; }

    public string? PaymentOfMonthJune2023BankRes { get; set; }

    public string? PaymentOfYearJuly2023 { get; set; }

    public string? PaymentOfMonthJuly2023 { get; set; }

    public string? PaymentOfYearJuly2023BankRes { get; set; }

    public string? PaymentOfMonthJuly2023BankRes { get; set; }

    public string? PaymentOfYearAug2023 { get; set; }

    public string? PaymentOfMonthAug2023 { get; set; }

    public string? PaymentOfYearAug2023BankRes { get; set; }

    public string? PaymentOfMonthAug2023BankRes { get; set; }

    public string? PaymentOfYearSep2023 { get; set; }

    public string? PaymentOfMonthSep2023 { get; set; }

    public string? PaymentOfYearSep2023BankRes { get; set; }

    public string? PaymentOfMonthSep2023BankRes { get; set; }

    public string? PaymentOfYearOct2023 { get; set; }

    public string? PaymentOfMonthOct2023 { get; set; }

    public string? PaymentOfYearOct2023BankRes { get; set; }

    public string? PaymentOfMonthOct2023BankRes { get; set; }

    public string? PaymentOfYearNov2023 { get; set; }

    public string? PaymentOfMonthNov2023 { get; set; }

    public string? PaymentOfYearNov2023BankRes { get; set; }

    public string? PaymentOfMonthNov2023BankRes { get; set; }

    public string? PaymentOfYearDec2023 { get; set; }

    public string? PaymentOfMonthDec2023 { get; set; }

    public string? PaymentOfYearDec2023BankRes { get; set; }

    public string? PaymentOfMonthDec2023BankRes { get; set; }

    public string? PaymentOfYearJan2024 { get; set; }

    public string? PaymentOfMonthJan2024 { get; set; }

    public string? PaymentOfYearJan2024BankRes { get; set; }

    public string? PaymentOfMonthJan2024BankRes { get; set; }

    public string? PaymentOfYearFeb2024 { get; set; }

    public string? PaymentOfMonthFeb2024 { get; set; }

    public string? PaymentOfYearFeb2024BankRes { get; set; }

    public string? PaymentOfMonthFeb2024BankRes { get; set; }

    public string? JunePaymentMonth2ndBnkFile2023 { get; set; }

    public string? OctPaymentMonth2ndBnkFile2023 { get; set; }

    public string? OctPaymentMonth2ndBnkFile20232 { get; set; }

    public string? DummyCol { get; set; }
}
