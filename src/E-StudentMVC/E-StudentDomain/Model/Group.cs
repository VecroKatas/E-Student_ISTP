using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class Group : Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();

    public virtual ICollection<LessonSchedule> LessonSchedules { get; set; } = new List<LessonSchedule>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
