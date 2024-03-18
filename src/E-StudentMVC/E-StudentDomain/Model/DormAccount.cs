using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class DormAccount : Entity
{
    public int Id { get; set; }

    [Display(Name = "Поточний баланс")]
    public int CurrentBalance { get; set; }

    [Display(Name = "Номер рахунку")]
    public int Number { get; set; }

    public virtual ICollection<DormAccountTransaction> DormAccountTransactions { get; set; } = new List<DormAccountTransaction>();

    public virtual ICollection<DormResident> DormResidents { get; set; } = new List<DormResident>();
}
