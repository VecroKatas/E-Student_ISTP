using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class LessonSchedule : Entity
{
    public int Id { get; set; }

    [Display(Name = "Id групи")]
    public int GroupId { get; set; }

    [Display(Name = "Рік розкладу")]
    public int Year { get; set; }

    [Display(Name = "Посилання на файл з розкладом")]
    public string File { get; set; } = null!;

    [Display(Name = "Номер групи")]
    public virtual Group Group { get; set; } = null!;
}
