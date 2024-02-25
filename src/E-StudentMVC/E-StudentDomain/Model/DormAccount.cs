using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class DormAccount : Entity
{
    public int Id { get; set; }

    public int CurrentBalance { get; set; }

    public int Number { get; set; }

    public virtual ICollection<DormAccountTransaction> DormAccountTransactions { get; set; } = new List<DormAccountTransaction>();

    public virtual ICollection<DormResident> DormResidents { get; set; } = new List<DormResident>();
}
