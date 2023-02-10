using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TASK9.Models;

public partial class Prescription
{

    public int IdPrescription { get; set; }
    public int IdDoctor { get; set; }
    public int IdPatient { get; set; }

    public DateTime Date { get; set; }


    public DateTime DueDate { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
   
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; } = new List<PrescriptionMedicament>();


}