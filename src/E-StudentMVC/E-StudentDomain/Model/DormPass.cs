using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class DormPass : Entity
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string Name { get; set; } = null!;

    public int DormId { get; set; }

    public int RoomId { get; set; }

    public DateOnly Issued { get; set; }

    public DateOnly Expires { get; set; }

    public virtual Dorm Dorm { get; set; } = null!;

    public virtual ICollection<DormResident> DormResidents { get; set; } = new List<DormResident>();

    public virtual DormRoom Room { get; set; } = null!;
}
