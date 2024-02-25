using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class DormInspection : Entity
{
    public int Id { get; set; }

    public int DormId { get; set; }

    public DateTime Date { get; set; }

    public virtual Dorm Dorm { get; set; } = null!;
}
