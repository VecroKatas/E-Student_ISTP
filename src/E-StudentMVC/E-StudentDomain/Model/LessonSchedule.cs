using System;
using System.Collections.Generic;

namespace E_StudentDomain.Model;

public partial class LessonSchedule : Entity
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int Year { get; set; }

    public string File { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
