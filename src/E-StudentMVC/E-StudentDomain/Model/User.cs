using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class User : Entity
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsDormResident { get; set; }

    public DateOnly CreatedAt { get; set; }

    public int? DormResidentId { get; set; }

    public virtual DormResident? DormResident { get; set; }

    public virtual Student Student { get; set; } = null!;
}
