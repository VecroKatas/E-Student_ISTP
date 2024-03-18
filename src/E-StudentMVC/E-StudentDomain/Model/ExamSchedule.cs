using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class ExamSchedule : Entity
{
    public int Id { get; set; }

    [Display(Name = "Id групи")]
    public int GroupId { get; set; }

    [Display(Name = "Рік екзаменів")]
    public int Year { get; set; }

    [Display(Name = "Посилання на розклад екзаменів")]
    public string File { get; set; } = null!;

    [Display(Name = "Номер групи")]
    public virtual Group Group { get; set; } = null!;
}
