using System;
using System.Collections.Generic;

namespace PensionTemporary.Models.Entities;

public partial class UpdateHistory
{
    public int Uuid { get; set; }

    public string RefNo { get; set; } = null!;

    public string UpdationDetails { get; set; } = null!;
}
