using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class DormResident : Entity
{
    public int Id { get; set; }

    [Display(Name = "ПІП")]
    public string Name { get; set; } = null!;

    [Display(Name = "Id перепустки")]
    public int PassId { get; set; }

    [Display(Name = "Id рахунку")]
    public int AccountId { get; set; }

    [Display(Name = "Номер рахунку")]
    public virtual DormAccount Account { get; set; } = null!;

    [Display(Name = "Номер перепустки")]
    public virtual DormPass Pass { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
