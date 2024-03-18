using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class Group : Entity
{
    public int Id { get; set; }

    [Display(Name = "Номер групи")]
    public string Name { get; set; } = null!;

    [Display(Name = "Рік утворення групи")]
    public int Year { get; set; }

    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();

    public virtual ICollection<LessonSchedule> LessonSchedules { get; set; } = new List<LessonSchedule>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
