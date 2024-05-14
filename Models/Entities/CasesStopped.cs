using System;
using System.Collections.Generic;

namespace PensionTemporary.Models.Entities;

public partial class CasesStopped
{
    public int? Uid { get; set; }

    public string? RefNo { get; set; }

    public string? DistrictLgdcode { get; set; }

    public string? DistrictName { get; set; }

    public string? Name { get; set; }

    public string? Parentage { get; set; }

    public string? InfRecvdInnicOn { get; set; }

    public string? ReasonForStoppingPension { get; set; }

    public string? LetterFrom { get; set; }

    public string? LetterNo { get; set; }

    public string? LetterDate { get; set; }

    public string? Reason1 { get; set; }

    public string? Reason2 { get; set; }

    public string? Reason3 { get; set; }
}
