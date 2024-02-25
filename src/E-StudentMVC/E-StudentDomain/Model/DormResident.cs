using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class DormResident : Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int PassId { get; set; }

    public int AccountId { get; set; }

    public virtual DormAccount Account { get; set; } = null!;

    public virtual DormPass Pass { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
