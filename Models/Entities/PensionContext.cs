using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PensionTemporary.Models.Entities;

public partial class PensionContext : DbContext
{
    public PensionContext()
    {
    }

    public PensionContext(DbContextOptions<PensionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BankFile> BankFiles { get; set; }

    public virtual DbSet<CasesStopped> CasesStoppeds { get; set; }

    public virtual DbSet<DepartmentFileModel> DepartmentFileModels { get; set; }

    public virtual DbSet<JkSwdeliveredMay30> JkSwdeliveredMay30s { get; set; }

    public virtual DbSet<MsDistrict> MsDistricts { get; set; }

    public virtual DbSet<MsDivisionTop500> MsDivisionTop500s { get; set; }

    public virtual DbSet<MsTswotehsilTop500> MsTswotehsilTop500s { get; set; }

    public virtual DbSet<NsapDatum> NsapData { get; set; }

    public virtual DbSet<SearchCount> SearchCounts { get; set; }

    public virtual DbSet<UpdateHistory> UpdateHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankFile>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<CasesStopped>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("casesStopped");

            entity.Property(e => e.DistrictLgdcode)
                .HasMaxLength(255)
                .HasColumnName("districtLGDcode");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(255)
                .HasColumnName("districtName");
            entity.Property(e => e.InfRecvdInnicOn)
                .HasMaxLength(255)
                .HasColumnName("infRecvdINnicOn");
            entity.Property(e => e.LetterDate)
                .HasMaxLength(255)
                .HasColumnName("letterDate");
            entity.Property(e => e.LetterFrom)
                .HasMaxLength(255)
                .HasColumnName("letterFrom");
            entity.Property(e => e.LetterNo)
                .HasMaxLength(255)
                .HasColumnName("letterNo");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Parentage)
                .HasMaxLength(255)
                .HasColumnName("parentage");
            entity.Property(e => e.Reason1)
                .HasMaxLength(255)
                .HasColumnName("reason1");
            entity.Property(e => e.Reason2)
                .HasMaxLength(255)
                .HasColumnName("reason2");
            entity.Property(e => e.Reason3)
                .HasMaxLength(255)
                .HasColumnName("reason3");
            entity.Property(e => e.ReasonForStoppingPension)
                .HasMaxLength(255)
                .HasColumnName("reasonForStoppingPension");
            entity.Property(e => e.RefNo)
                .HasMaxLength(255)
                .HasColumnName("refNo");
            entity.Property(e => e.Uid).HasColumnName("uid");
        });

        modelBuilder.Entity<DepartmentFileModel>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.JkIsss).HasColumnName("JK_ISSS");
            entity.Property(e => e.PresentGpMuncipality).HasColumnName("Present_GP_Muncipality");
            entity.Property(e => e.PreviousPensionBankIfsccode).HasColumnName("PreviousPensionBankIFSCcode");
            entity.Property(e => e.TypeOfDisabilityAsPerUdid).HasColumnName("TypeOfDisabilityAsPerUDID");
        });

        modelBuilder.Entity<JkSwdeliveredMay30>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("jkSWdeliveredMay30");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(255)
                .HasComment("Defines the account number of the applicant")
                .HasColumnName("accountNo");
            entity.Property(e => e.AccountNoCorrectionByDirectorate)
                .HasMaxLength(255)
                .HasColumnName("accountNoCorrectionByDirectorate");
            entity.Property(e => e.AccountNoCorrectionDate)
                .HasColumnType("datetime")
                .HasColumnName("accountNoCorrectionDate");
            entity.Property(e => e.AccountNoCorrectionDirLetter)
                .HasMaxLength(255)
                .HasColumnName("accountNoCorrectionDirLetter");
            entity.Property(e => e.ActionOnDate)
                .HasMaxLength(255)
                .HasColumnName("actionOnDate");
            entity.Property(e => e.ActionOnDateVerified1)
                .HasMaxLength(255)
                .HasColumnName("actionOnDate_verified1");
            entity.Property(e => e.AppliedDistrict)
                .HasMaxLength(255)
                .HasColumnName("appliedDistrict");
            entity.Property(e => e.AppliedTehsil)
                .HasMaxLength(255)
                .HasColumnName("appliedTehsil");
            entity.Property(e => e.AprilChk)
                .HasMaxLength(255)
                .HasColumnName("aprilChk");
            entity.Property(e => e.AprilChk2)
                .HasMaxLength(255)
                .HasColumnName("aprilChk2");
            entity.Property(e => e.AprilChk3)
                .HasMaxLength(255)
                .HasColumnName("aprilChk3");
            entity.Property(e => e.ArearBankFileBankPaymentOk)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("arear_bank_file_bankPayment_ok");
            entity.Property(e => e.ArearBankFileBankPaymentOkDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("arear_bank_file_bankPayment_ok_date");
            entity.Property(e => e.ArrearApril23MinusMarch23).HasColumnName("arrear_April23_Minus_March23");
            entity.Property(e => e.ArrearAug23MinusJuly23).HasColumnName("arrear_Aug23_Minus_July23");
            entity.Property(e => e.ArrearBankFileGenerated)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("arrear_bank_file_generated");
            entity.Property(e => e.ArrearBankFileGeneratedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("arrear_bank_file_generated_date");
            entity.Property(e => e.ArrearDec23MinusNov23).HasColumnName("arrear_Dec23_Minus_Nov23");
            entity.Property(e => e.ArrearFeb24MinusJan24).HasColumnName("arrear_Feb24_Minus_Jan24");
            entity.Property(e => e.ArrearJan24MinusDec23).HasColumnName("arrear_Jan24_Minus_Dec23");
            entity.Property(e => e.ArrearJuly23MinusJune23).HasColumnName("arrear_July23_Minus_June23");
            entity.Property(e => e.ArrearJune23MinusMay23).HasColumnName("arrear_June23_Minus_May23");
            entity.Property(e => e.ArrearMar24MinusFeb24).HasColumnName("arrear_Mar24_Minus_Feb24");
            entity.Property(e => e.ArrearMarch23MinusFeb23).HasColumnName("arrear_March23_Minus_Feb23");
            entity.Property(e => e.ArrearMay23MinusApril23).HasColumnName("arrear_May23_Minus_April23");
            entity.Property(e => e.ArrearNov23MinusOct23).HasColumnName("arrear_Nov23_Minus_Oct23");
            entity.Property(e => e.ArrearOct23MinusSep23).HasColumnName("arrear_Oct23_Minus_Sep23");
            entity.Property(e => e.ArrearSep23MinusAug23).HasColumnName("arrear_Sep23_Minus_Aug23");
            entity.Property(e => e.ArrearTotalMonths).HasColumnName("arrear_total_months");
            entity.Property(e => e.ArrearTotalMonthsAmt).HasColumnName("arrear_total_months_amt");
            entity.Property(e => e.BankFileGeneratedBeforeDirCorrection)
                .HasMaxLength(255)
                .HasColumnName("bankFileGeneratedBeforeDirCorrection");
            entity.Property(e => e.BankName)
                .HasMaxLength(255)
                .HasColumnName("bankName");
            entity.Property(e => e.BankNameCorrectionByDirectorate)
                .HasMaxLength(255)
                .HasColumnName("bankNameCorrectionByDirectorate");
            entity.Property(e => e.BankNameCorrectionDate)
                .HasMaxLength(255)
                .HasColumnName("bankNameCorrectionDate");
            entity.Property(e => e.BankNameCorrectionDirLetter)
                .HasMaxLength(255)
                .HasColumnName("bankNameCorrectionDirLetter");
            entity.Property(e => e.BranchName)
                .HasMaxLength(255)
                .HasColumnName("branchName");
            entity.Property(e => e.BranchNameCorrectionByDirectorate)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("branchNameCorrectionByDirectorate");
            entity.Property(e => e.ChkAccountNoUpdated)
                .HasMaxLength(255)
                .HasColumnName("chkAccountNoUpdated");
            entity.Property(e => e.ChkIfscCodeUpdated)
                .HasMaxLength(255)
                .HasColumnName("chkIfscCodeUpdated");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(255)
                .HasColumnName("currentStatus");
            entity.Property(e => e.DeptVerified)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("deptVerified");
            entity.Property(e => e.DeptVerifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("deptVerifiedDate");
            entity.Property(e => e.DistrictLgdcode).HasColumnName("districtLGDCode");
            entity.Property(e => e.DistrictNameForBank)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("districtNameForBank");
            entity.Property(e => e.DistrictUidForBank)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("districtUidForBank");
            entity.Property(e => e.DivisionCode).HasColumnName("divisionCode");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(255)
                .HasColumnName("divisionName");
            entity.Property(e => e.Dob)
                .HasMaxLength(255)
                .HasColumnName("dob");
            entity.Property(e => e.DummyCol)
                .HasMaxLength(255)
                .HasColumnName("dummyCol");
            entity.Property(e => e.DuplicateBankAccountNo)
                .HasMaxLength(255)
                .HasColumnName("duplicateBankAccountNo");
            entity.Property(e => e.EligibleForPension)
                .HasMaxLength(255)
                .HasComment("Defines whether applicant is eligible to receive pension or not")
                .HasColumnName("eligibleForPension");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.GoiNsap)
                .HasMaxLength(255)
                .HasColumnName("GOI_NSAP");
            entity.Property(e => e.IfscCode)
                .HasMaxLength(255)
                .HasComment("Defines the Ifsc Code of the applicant")
                .HasColumnName("ifscCode");
            entity.Property(e => e.IfscCorrectionByDirectorate)
                .HasMaxLength(255)
                .HasColumnName("ifscCorrectionByDirectorate");
            entity.Property(e => e.IfscCorrectionDate)
                .HasColumnType("datetime")
                .HasColumnName("ifscCorrectionDate");
            entity.Property(e => e.IfscCorrectionDirLetter)
                .HasMaxLength(255)
                .HasColumnName("ifscCorrectionDirLetter");
            entity.Property(e => e.JanSugamDownloadCycle)
                .HasMaxLength(255)
                .HasColumnName("janSugamDownloadCycle");
            entity.Property(e => e.JkIsss)
                .HasMaxLength(255)
                .HasColumnName("JK_ISSS");
            entity.Property(e => e.JunePaymentMonth2ndBnkFile2023)
                .HasMaxLength(255)
                .HasColumnName("junePaymentMonth2ndBnkFile2023");
            entity.Property(e => e.LeftOutCasesDueToSchemeClarification)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("leftOutCasesDueToSchemeClarification");
            entity.Property(e => e.LgdStateCode).HasColumnName("lgdStateCode");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(255)
                .HasColumnName("mobileNo");
            entity.Property(e => e.MonthActionOnDate)
                .HasMaxLength(255)
                .HasColumnName("monthActionOnDate");
            entity.Property(e => e.MonthActionOnDateVerified1)
                .HasMaxLength(255)
                .HasColumnName("monthActionOnDate_verified1");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Defines the name of the applicant")
                .HasColumnName("name");
            entity.Property(e => e.NameCorrectionByDirectorate)
                .HasMaxLength(255)
                .HasColumnName("nameCorrectionByDirectorate");
            entity.Property(e => e.NameCorrectionDate)
                .HasColumnType("datetime")
                .HasColumnName("nameCorrectionDate");
            entity.Property(e => e.NameCorrectionDirLetter)
                .HasMaxLength(255)
                .HasColumnName("nameCorrectionDirLetter");
            entity.Property(e => e.NsapChk)
                .HasMaxLength(255)
                .HasColumnName("nsapChk");
            entity.Property(e => e.OctPaymentMonth2ndBnkFile2023)
                .HasMaxLength(255)
                .HasColumnName("octPaymentMonth2ndBnkFile2023");
            entity.Property(e => e.OctPaymentMonth2ndBnkFile20232)
                .HasMaxLength(255)
                .HasColumnName("octPaymentMonth2ndBnkFile2023_2");
            entity.Property(e => e.OldCdacbenificary)
                .HasMaxLength(255)
                .HasColumnName("oldCDACbenificary");
            entity.Property(e => e.Parentage)
                .HasMaxLength(255)
                .HasColumnName("parentage");
            entity.Property(e => e.PaymentOfMonthApril2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthApril2023");
            entity.Property(e => e.PaymentOfMonthApril2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthApril2023BankRes");
            entity.Property(e => e.PaymentOfMonthAug2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthAug2023");
            entity.Property(e => e.PaymentOfMonthAug2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthAug2023BankRes");
            entity.Property(e => e.PaymentOfMonthDec2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthDec2023");
            entity.Property(e => e.PaymentOfMonthDec2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthDec2023BankRes");
            entity.Property(e => e.PaymentOfMonthFeb2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthFeb2023");
            entity.Property(e => e.PaymentOfMonthFeb2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthFeb2023BankRes");
            entity.Property(e => e.PaymentOfMonthFeb2024)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthFeb2024");
            entity.Property(e => e.PaymentOfMonthFeb2024BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthFeb2024BankRes");
            entity.Property(e => e.PaymentOfMonthJan2024)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthJan2024");
            entity.Property(e => e.PaymentOfMonthJan2024BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthJan2024BankRes");
            entity.Property(e => e.PaymentOfMonthJuly2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthJuly2023");
            entity.Property(e => e.PaymentOfMonthJuly2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthJuly2023BankRes");
            entity.Property(e => e.PaymentOfMonthJune2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthJune2023");
            entity.Property(e => e.PaymentOfMonthJune2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthJune2023BankRes");
            entity.Property(e => e.PaymentOfMonthMarch2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthMarch2023");
            entity.Property(e => e.PaymentOfMonthMarch2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthMarch2023BankRes");
            entity.Property(e => e.PaymentOfMonthMay2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthMay2023");
            entity.Property(e => e.PaymentOfMonthMay2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthMay2023BankRes");
            entity.Property(e => e.PaymentOfMonthNov2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthNov2023");
            entity.Property(e => e.PaymentOfMonthNov2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthNov2023BankRes");
            entity.Property(e => e.PaymentOfMonthOct2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthOct2023");
            entity.Property(e => e.PaymentOfMonthOct2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthOct2023BankRes");
            entity.Property(e => e.PaymentOfMonthSep2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthSep2023");
            entity.Property(e => e.PaymentOfMonthSep2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfMonthSep2023BankRes");
            entity.Property(e => e.PaymentOfYearApril2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearApril2023");
            entity.Property(e => e.PaymentOfYearApril2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearApril2023BankRes");
            entity.Property(e => e.PaymentOfYearAug2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearAug2023");
            entity.Property(e => e.PaymentOfYearAug2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearAug2023BankRes");
            entity.Property(e => e.PaymentOfYearDec2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearDec2023");
            entity.Property(e => e.PaymentOfYearDec2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearDec2023BankRes");
            entity.Property(e => e.PaymentOfYearFeb2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearFeb2023");
            entity.Property(e => e.PaymentOfYearFeb2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearFeb2023BankRes");
            entity.Property(e => e.PaymentOfYearFeb2024)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearFeb2024");
            entity.Property(e => e.PaymentOfYearFeb2024BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearFeb2024BankRes");
            entity.Property(e => e.PaymentOfYearJan2024)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearJan2024");
            entity.Property(e => e.PaymentOfYearJan2024BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearJan2024BankRes");
            entity.Property(e => e.PaymentOfYearJuly2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearJuly2023");
            entity.Property(e => e.PaymentOfYearJuly2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearJuly2023BankRes");
            entity.Property(e => e.PaymentOfYearJune2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearJune2023");
            entity.Property(e => e.PaymentOfYearJune2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearJune2023BankRes");
            entity.Property(e => e.PaymentOfYearMarch2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearMarch2023");
            entity.Property(e => e.PaymentOfYearMarch2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearMarch2023BankRes");
            entity.Property(e => e.PaymentOfYearMay2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearMay2023");
            entity.Property(e => e.PaymentOfYearMay2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearMay2023BankRes");
            entity.Property(e => e.PaymentOfYearNov2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearNov2023");
            entity.Property(e => e.PaymentOfYearNov2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearNov2023BankRes");
            entity.Property(e => e.PaymentOfYearOct2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearOct2023");
            entity.Property(e => e.PaymentOfYearOct2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearOct2023BankRes");
            entity.Property(e => e.PaymentOfYearSep2023)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearSep2023");
            entity.Property(e => e.PaymentOfYearSep2023BankRes)
                .HasMaxLength(255)
                .HasColumnName("paymentOfYearSep2023BankRes");
            entity.Property(e => e.PensionType)
                .HasMaxLength(255)
                .HasColumnName("pensionType");
            entity.Property(e => e.PensionTypeShort)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("pensionTypeShort");
            entity.Property(e => e.PresentAddress)
                .HasMaxLength(255)
                .HasColumnName("presentAddress");
            entity.Property(e => e.PresentDistrict)
                .HasMaxLength(255)
                .HasColumnName("presentDistrict");
            entity.Property(e => e.PresentGpMuncipality)
                .HasMaxLength(255)
                .HasColumnName("present_GP_Muncipality");
            entity.Property(e => e.PresentTehsil)
                .HasMaxLength(255)
                .HasColumnName("presentTehsil");
            entity.Property(e => e.PresentVillage)
                .HasMaxLength(255)
                .HasColumnName("presentVillage");
            entity.Property(e => e.PreviousPension)
                .HasMaxLength(255)
                .HasColumnName("previousPension");
            entity.Property(e => e.PreviousPensionAccountNo)
                .HasMaxLength(255)
                .HasColumnName("previousPensionAccountNo");
            entity.Property(e => e.PreviousPensionBank)
                .HasMaxLength(255)
                .HasColumnName("previousPensionBank");
            entity.Property(e => e.PreviousPensionBankBranch)
                .HasMaxLength(255)
                .HasColumnName("previousPensionBankBranch");
            entity.Property(e => e.PreviousPensionBankIfsccode)
                .HasMaxLength(255)
                .HasColumnName("previousPensionBankIFSCcode");
            entity.Property(e => e.ReSanctionDate)
                .HasMaxLength(255)
                .HasColumnName("reSanctionDate");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.RefNo)
                .HasMaxLength(255)
                .HasColumnName("refNo");
            entity.Property(e => e.Remarks).HasMaxLength(255);
            entity.Property(e => e.Remarks2)
                .HasMaxLength(255)
                .HasColumnName("remarks2");
            entity.Property(e => e.Remarks3)
                .HasMaxLength(255)
                .HasColumnName("remarks3");
            entity.Property(e => e.RemarksForCorrectionInNameIfscaccountNo)
                .HasMaxLength(255)
                .HasColumnName("remarksForCorrectionInNameIFSCaccountNo");
            entity.Property(e => e.SanctionedByTask)
                .HasMaxLength(255)
                .HasColumnName("sanctionedByTask");
            entity.Property(e => e.SchemeClarification)
                .HasMaxLength(255)
                .HasColumnName("SCHEME_CLARIFICATION");
            entity.Property(e => e.SchemeSanctionedByDirJammu)
                .HasMaxLength(255)
                .HasColumnName("schemeSanctionedByDirJammu");
            entity.Property(e => e.SchemeSanctionedByDirKashmir)
                .HasMaxLength(255)
                .HasColumnName("schemeSanctionedByDirKashmir");
            entity.Property(e => e.SchemeSanctionedByDswo)
                .HasMaxLength(255)
                .HasColumnName("schemeSanctionedByDSWO");
            entity.Property(e => e.SharedWithDeptForVerification)
                .HasMaxLength(255)
                .HasColumnName("sharedWithDeptForVerification");
            entity.Property(e => e.Sno).HasColumnName("sno");
            entity.Property(e => e.TswoPreviousPensionScheme)
                .HasMaxLength(255)
                .HasColumnName("tswoPreviousPensionScheme");
            entity.Property(e => e.TswoPreviousPensionYesNo)
                .HasMaxLength(255)
                .HasColumnName("tswoPreviousPensionYesNo");
            entity.Property(e => e.TswotehsilCode).HasColumnName("TSWOtehsilCode");
            entity.Property(e => e.TypeOfDisabilityAsPerUdid)
                .HasMaxLength(255)
                .HasColumnName("typeOfDisabilityAsPerUDID");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.YearActionOnDate)
                .HasMaxLength(255)
                .HasColumnName("yearActionOnDate");
            entity.Property(e => e.YearActionOnDateVerified1)
                .HasMaxLength(255)
                .HasColumnName("yearActionOnDate_verified1");
        });

        modelBuilder.Entity<MsDistrict>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("msDistrict");

            entity.Property(e => e.DistrictLgdCode).HasColumnName("districtLgdCode");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("districtName");
            entity.Property(e => e.DistrictNameForBank)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("districtNameForBank");
            entity.Property(e => e.DivisionCode).HasColumnName("divisionCode");
        });

        modelBuilder.Entity<MsDivisionTop500>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("msDivision_top500");

            entity.Property(e => e.DivisionCode)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("divisionCode");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("divisionName");
            entity.Property(e => e.StateCode).HasColumnName("stateCode");
        });

        modelBuilder.Entity<MsTswotehsilTop500>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("msTSWOtehsil_top500");

            entity.Property(e => e.DistrictLgdcode).HasColumnName("districtLGDCode");
            entity.Property(e => e.DivisionCode).HasColumnName("divisionCode");
            entity.Property(e => e.TswoOfficeName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("tswoOfficeName");
            entity.Property(e => e.TswotehsilCode).HasColumnName("TSWOtehsilCode");
            entity.Property(e => e.TswotehsilName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TSWOtehsilName");
        });

        modelBuilder.Entity<NsapDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("nsapData");

            entity.Property(e => e.BankaccountNo)
                .HasMaxLength(255)
                .HasColumnName("bankaccount_no");
            entity.Property(e => e.BeneficiaryName)
                .HasMaxLength(255)
                .HasColumnName("beneficiary_name");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(255)
                .HasColumnName("district_code");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(255)
                .HasColumnName("district_name");
            entity.Property(e => e.DuplicateAccountNo)
                .HasMaxLength(50)
                .HasColumnName("duplicateAccountNo");
            entity.Property(e => e.GramPanchayatWardCode)
                .HasMaxLength(255)
                .HasColumnName("gram_panchayat_ward_code");
            entity.Property(e => e.GramPanchayatWardName)
                .HasMaxLength(255)
                .HasColumnName("gram_panchayat_ward_name");
            entity.Property(e => e.IfscCode)
                .HasMaxLength(255)
                .HasColumnName("ifsc_code");
            entity.Property(e => e.ParentageName)
                .HasMaxLength(255)
                .HasColumnName("parentage_name");
            entity.Property(e => e.SanctionOrderNo)
                .HasMaxLength(255)
                .HasColumnName("sanction_order_no");
            entity.Property(e => e.SchemeName)
                .HasMaxLength(255)
                .HasColumnName("scheme_name");
            entity.Property(e => e.StateCode)
                .HasMaxLength(255)
                .HasColumnName("state_code");
            entity.Property(e => e.StateName)
                .HasMaxLength(255)
                .HasColumnName("state_name");
            entity.Property(e => e.SubDistrictMunicipalAreaCode)
                .HasMaxLength(255)
                .HasColumnName("sub_district_municipal_area_code");
            entity.Property(e => e.SubDistrictMunicipalAreaName)
                .HasMaxLength(255)
                .HasColumnName("sub_district_municipal_area_name");
            entity.Property(e => e.VillageCode)
                .HasMaxLength(255)
                .HasColumnName("village_code");
            entity.Property(e => e.VillageName)
                .HasMaxLength(255)
                .HasColumnName("village_name");
        });

        modelBuilder.Entity<SearchCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SearchCount");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<UpdateHistory>(entity =>
        {
            entity.HasKey(e => e.Uuid);

            entity.ToTable("UpdateHistory");

            entity.Property(e => e.Uuid).HasColumnName("UUID");
            entity.Property(e => e.RefNo).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uuid);

            entity.Property(e => e.Uuid).HasColumnName("UUID");
            entity.Property(e => e.DivisionCode).HasColumnName("divisionCode");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.UserType).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
