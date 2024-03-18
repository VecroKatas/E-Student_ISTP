using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class Student : Entity
{
    public int Id { get; set; }

    [Display(Name = "ПІП")]
    public string Name { get; set; } = null!;

    [Display(Name = "Факультет")]
    public string Faculty { get; set; } = null!;

    [Display(Name = "Курс")]
    public int Course { get; set; }

    [Display(Name = "Id групи")]
    public int GroupId { get; set; }

    [Display(Name = "Номер студквитка")]
    public string StudentNumber { get; set; } = null!;

    [Display(Name = "Група")]
    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
