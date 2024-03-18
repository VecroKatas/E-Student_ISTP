using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class DormPass : Entity
{
    public int Id { get; set; }

    [Display(Name = "Номер перепустки")]
    public int Number { get; set; }

    [Display(Name = "ПІП")]
    public string Name { get; set; } = null!;

    [Display(Name = "Id гуртожитку")]
    public int DormId { get; set; }

    [Display(Name = "Id кімнати")]
    public int RoomId { get; set; }

    [Display(Name = "Виданий")]
    public DateOnly Issued { get; set; }

    [Display(Name = "Дійсний до")]
    public DateOnly Expires { get; set; }

    [Display(Name = "Номер гуртожитку")]
    public virtual Dorm Dorm { get; set; } = null!;

    public virtual ICollection<DormResident> DormResidents { get; set; } = new List<DormResident>();

    [Display(Name = "Номер кімнати")]
    public virtual DormRoom Room { get; set; } = null!;
}
