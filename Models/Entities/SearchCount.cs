using System;
using System.Collections.Generic;

namespace PensionTemporary.Models.Entities;

public partial class SearchCount
{
    public int CountId { get; set; }

    public string Username { get; set; } = null!;

    public int Count { get; set; }
}
