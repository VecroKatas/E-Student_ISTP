using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class User : Entity
{
    public int Id { get; set; }

    [Display(Name="Id студента")]
    public int StudentId { get; set; }

    [Display(Name = "ПІП")]
    public string Name { get; set; } = null!;

    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;

    [Display(Name = "Мешканець гуртожитку?")]
    public bool IsDormResident { get; set; }

    [Display(Name = "Зареєстрований")]
    public DateOnly CreatedAt { get; set; }

    [Display(Name = "Id мешканця гуртожитку")]
    public int? DormResidentId { get; set; }

    public virtual DormResident? DormResident { get; set; }

    public virtual Student Student { get; set; } = null!;
}
