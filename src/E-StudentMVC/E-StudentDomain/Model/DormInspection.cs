using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class DormInspection : Entity
{
    public int Id { get; set; }

    [Display(Name = "Id гуртожитку")]
    public int DormId { get; set; }

    [Display(Name = "Дата та час")]
    public DateTime Date { get; set; }

    [Display(Name = "Номер гутрожитку")]
    public virtual Dorm Dorm { get; set; } = null!;
}
