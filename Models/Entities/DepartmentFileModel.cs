﻿using System;
using System.Collections.Generic;

namespace PensionTemporary.Models.Entities;

public partial class DepartmentFileModel
{
    public string? RefNo { get; set; }

    public string? AppliedDistrict { get; set; }

    public string? AppliedTehsil { get; set; }

    public string? Name { get; set; }

    public string? Parentage { get; set; }

    public string? Dob { get; set; }

    public string? Gender { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? PensionType { get; set; }

    public string? TypeOfDisabilityAsPerUdid { get; set; }

    public string? PresentAddress { get; set; }

    public string? PresentDistrict { get; set; }

    public string? PresentTehsil { get; set; }

    public string? PresentGpMuncipality { get; set; }

    public string? PresentVillage { get; set; }

    public string? BankName { get; set; }

    public string? BranchName { get; set; }

    public string? IfscCode { get; set; }

    public string? AccountNo { get; set; }

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

    public string? CurrentStatus { get; set; }

    public string? SanctionedByTask { get; set; }

    public string? JkIsss { get; set; }

    public string? JanSugamDownloadCycle { get; set; }
}
