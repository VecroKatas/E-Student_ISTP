using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class Dorm : Entity
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<DormInspection> DormInspections { get; set; } = new List<DormInspection>();

    public virtual ICollection<DormPass> DormPasses { get; set; } = new List<DormPass>();

    public virtual ICollection<DormRoom> DormRooms { get; set; } = new List<DormRoom>();
}
