using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace TASK9.Models;

public partial class Patient
{

    public int IdPatient { get; set; }


    public string FirstName { get; set; } = null!;


    public string LastName { get; set; } = null!;


    public DateTime Birthdate { get; set; }

    public virtual ICollection<Prescription> ClientPrescriptions { get; } = new List<Prescription>();


}