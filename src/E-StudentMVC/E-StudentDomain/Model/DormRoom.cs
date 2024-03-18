using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class DormRoom : Entity
{
    public int Id { get; set; }

    [Display(Name = "Номер")]
    public int Number { get; set; }

    [Display(Name = "Id гуртожитку")]
    public int DormId { get; set; }

    [Display(Name = "Номер гуртожитку")]
    public virtual Dorm Dorm { get; set; } = null!;

    public virtual ICollection<DormPass> DormPasses { get; set; } = new List<DormPass>();
}
