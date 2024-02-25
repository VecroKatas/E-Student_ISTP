using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class DormAccountTransaction : Entity
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int Amount { get; set; }

    public int Balance { get; set; }

    public string SecondParty { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual DormAccount Account { get; set; } = null!;
}
