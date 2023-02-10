using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
namespace TASK9.Models;

public partial class PrescriptionMedicament
{


    // Used to store the prescription details on api /api/prescription/{idPrescription}
    public int Dose { get; set; }

    public int IdPrescription { get; set; }
    public int IdMedicament { get; set; }
    public string Details { get; set; }

    public virtual Medicament Medicament { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;

}