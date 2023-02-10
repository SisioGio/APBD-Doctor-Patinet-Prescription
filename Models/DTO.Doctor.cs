
namespace TASK9.Models;

 public class DoctorDTO
    {
        public int idDoctor { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public IEnumerable<Prescription> prescriptions{get;set;}

    }