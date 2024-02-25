using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class DormRoom : Entity
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int DormId { get; set; }

    public virtual Dorm Dorm { get; set; } = null!;

    public virtual ICollection<DormPass> DormPasses { get; set; } = new List<DormPass>();
}
