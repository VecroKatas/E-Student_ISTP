using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class Student : Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Faculty { get; set; } = null!;

    public int Course { get; set; }

    public int GroupId { get; set; }

    public string StudentNumber { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
