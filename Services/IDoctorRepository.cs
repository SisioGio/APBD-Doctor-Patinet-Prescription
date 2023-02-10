
using TASK9.Models;

using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace TASK9.Services
{
    public interface IDoctorRepository
    {

        public Task<Object> GetDoctorByID(int idDoctor);
        public Task<IEnumerable<Prescription>> GetPrescriptions();
        public Task<IEnumerable<Doctor>> GetDoctors();
        public Task<IEnumerable<Patient>> GetPatients();
        public Task<IEnumerable<PrescriptionMedicament>> GetPrescriptionMedicaments();
        public Task<IEnumerable<Medicament>> GetMedicaments();
 


        public Task<IEnumerable<Doctor>> DeleteDoctor(int idDoctor);

        public void PopulateDatabase();



    }
}