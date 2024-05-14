using System;
using System.Collections.Generic;

namespace PensionTemporary.Models.Entities;

public partial class NsapDatum
{
    public string? SanctionOrderNo { get; set; }

    public string? StateCode { get; set; }

    public string? StateName { get; set; }

    public string? DistrictCode { get; set; }

    public string? DistrictName { get; set; }

    public string? SubDistrictMunicipalAreaCode { get; set; }

    public string? SubDistrictMunicipalAreaName { get; set; }

    public string? GramPanchayatWardCode { get; set; }

    public string? GramPanchayatWardName { get; set; }

    public string? VillageCode { get; set; }

    public string? VillageName { get; set; }

    public string? BeneficiaryName { get; set; }

    public string? ParentageName { get; set; }

    public string? BankaccountNo { get; set; }

    public string? IfscCode { get; set; }

    public string? SchemeName { get; set; }

    public string? DuplicateAccountNo { get; set; }
}
