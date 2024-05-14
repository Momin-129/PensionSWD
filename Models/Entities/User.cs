using System;
using System.Collections.Generic;

namespace PensionTemporary.Models.Entities;

public partial class User
{
    public int Uuid { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? UserType { get; set; }

    public int? DivisionCode { get; set; }

    public string Password { get; set; } = null!;
}
