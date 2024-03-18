using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_StudentDomain.Model;

public partial class DormAccountTransaction : Entity
{
    public int Id { get; set; }

    [Display(Name = "Id рахунку")]
    public int AccountId { get; set; }

    [Display(Name = "Сума переказу")]
    public int Amount { get; set; }

    [Display(Name = "Баланс")]
    public int Balance { get; set; }

    [Display(Name = "Отримувач/відправник")]
    public string SecondParty { get; set; } = null!;

    [Display(Name = "Дата")]
    public DateTime Date { get; set; }

    [Display(Name = "Номер рахунку")]
    public virtual DormAccount Account { get; set; } = null!;
}
