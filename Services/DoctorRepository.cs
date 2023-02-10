
using TASK9.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace TASK9.Services
{
    public class DoctorRepository : IDoctorRepository
    {

        // Methods to work with the Doctor table
        public static MainDbContext _db;
        public DoctorRepository(MainDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Patient>> GetPatients()
        {

       
            return await _db.Patient.ToListAsync();
        }

                public async Task<IEnumerable<Prescription>> GetPrescriptions()
        {
          
       
            return await _db.Prescription.ToListAsync();
        }

                public async Task<IEnumerable<PrescriptionMedicament>> GetPrescriptionMedicaments()
        {
  
       
            return await _db.PrescriptionMedicament.ToListAsync();
        }
        public async Task<IEnumerable<Medicament>> GetMedicaments()
        {
  
       
            return await _db.Medicament.ToListAsync();
        }
        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
  
       
            return await _db.Doctors.ToListAsync();
        }
        public async Task<Object> GetDoctorByID(int idDoctor)
        {
            var doctor = await _db.Doctors.Where(d=>d.IdDoctor == idDoctor).Select(d=>new{
                name= d.FirstName,
                surname= d.LastName,
                email= d.Email,
                prescription = d.Prescriptions.Select( p=> new {
                    idPrescription=p.IdPrescription,
                    date=p.Date,
                    dueDate=p.DueDate,
                    patient= new {name=p.Patient.FirstName,lastname = p.Patient.LastName,birthDate = p.Patient.Birthdate},
                    medicaments = p.PrescriptionMedicaments.Select(pd=>new{
                                details=pd.Details,
                                dose= pd.Dose,
                                medicament = new {
                                    name = pd.Medicament.Name,
                                    description=pd.Medicament.Description,
                                    type= pd.Medicament.Type}
                                            })
                })


            }).SingleAsync();
            
            return doctor;
        }

              public async Task<IEnumerable<Doctor>> DeleteDoctor(int idDoctor)
        {
            Doctor doctor;

            try
            {
                using (var dbContextTransaction = _db.Database.BeginTransaction())
                {
                // Getting doctor
                doctor = await _db.Doctors.SingleAsync(d => d.IdDoctor == idDoctor);
                // Getting prescriptions
                List<Prescription> prescriptions = await  _db.Prescription.Where((p=>p.IdDoctor == doctor.IdDoctor)).ToListAsync();
                // Getting PrescriptioMedicaments
                            foreach(Prescription p in prescriptions){
                                List<PrescriptionMedicament> pm = await _db.PrescriptionMedicament.Where(x=>x.IdPrescription == p.IdPrescription).ToListAsync();
                                        foreach(PrescriptionMedicament pm_to_delete in pm){
                                            _db.Remove(pm_to_delete);
                                        }
                                
                                _db.Remove(p);
                            }

                _db.Remove(doctor);
                _db.SaveChanges();

                dbContextTransaction.Commit();
                }
                   

            }
            catch
            {
                throw new Exception($"Doctor with ID = {idDoctor} does not exist");
            }

        
            return await _db.Doctors.ToListAsync();
        }
    
    
    public async void PopulateDatabase()
        {
            // Remove all data
            foreach (PrescriptionMedicament pmed in _db.PrescriptionMedicament)
            {
                _db.Remove(pmed);
            }
            foreach (Prescription pres in _db.Prescription)
            {
                _db.Remove(pres);
            }
            foreach (Doctor doc in _db.Doctors)
            {
                _db.Remove(doc);
            }
            foreach (Patient pt in _db.Patient)
            {
                _db.Remove(pt);
            }
            foreach (Medicament med in _db.Medicament)
            {
               _db.Remove(med);
            }

            await _db.SaveChangesAsync();

            // Enter new data
            Doctor doc1 = new Doctor { FirstName = "Andrzej", LastName = "Malewski", Email = "malewski@wp.pl" };
            _db.Add(doc1);
            Doctor doc2 = new Doctor { FirstName = "Marcin", LastName = "Malędowski", Email = "moleda@wp.pl" };
            _db.Add(doc2);
            Doctor doc3 = new Doctor { FirstName = "Krzysztof", LastName = "Kowalewicz", Email = "kowalewicz@wp.pl" };
            _db.Add(doc3);
            Doctor doc4 = new Doctor { FirstName = "Anna", LastName = "Kostrzewska", Email = "akostrzew@onet.pl" };
            _db.Add(doc4);
            await _db.SaveChangesAsync();

            Patient pat1 = new Patient { FirstName = "Jan", LastName = "Andrzejewski", Birthdate = new DateTime(1980,02,02) };
            Patient pat2 = new Patient { FirstName = "Krzysztof", LastName = "Kowalewicz", Birthdate = new DateTime(1991,01,10) };
            Patient pat3 = new Patient { FirstName = "Marcin", LastName = "Andrzejewicz", Birthdate = new DateTime(1995,01,02) };
            _db.Add(pat1);
            _db.Add(pat2);
            _db.Add(pat3);

            await _db.SaveChangesAsync();
            Medicament med1 = new Medicament { Name = "Xanax", Description = "Lorem ipsum...", Type = "Depression" };
            Medicament med2 = new Medicament { Name = "Abilify", Description = "Lorem.", Type = "Tabletki" };
            Medicament med3 = new Medicament { Name = "Abra", Description = "Lorem...", Type = "Żel" };
            Medicament med4 = new Medicament { Name = "Acai", Description = "Test", Type = "Kapsułki" };
            Medicament med5 = new Medicament { Name = "ACC", Description = "Lorem ipsum...", Type = "Tabletki" };
            Medicament med6 = new Medicament { Name = "Acerin", Description = "Lorem...", Type = "Płyn" };
            _db.Add(med1);
            _db.Add(med2);
            _db.Add(med3);
             _db.Add(med4);
            _db.Add(med5);
            _db.Add(med6);
            await _db.SaveChangesAsync();
            Prescription prescr1 = new Prescription { IdDoctor = doc1.IdDoctor, IdPatient = pat2.IdPatient, Date = new DateTime(2020,05,10), DueDate = new DateTime(2020,10,23) };
            Prescription prescr2 = new Prescription { IdDoctor = doc1.IdDoctor, IdPatient = pat2.IdPatient, Date = new DateTime(2020,05,20), DueDate = new DateTime(2020,06,10) };
            Prescription prescr3 = new Prescription { IdDoctor = doc2.IdDoctor, IdPatient = pat1.IdPatient, Date = new DateTime(2020,06,05), DueDate = new DateTime(2020,06,20) };
            Prescription prescr4 = new Prescription { IdDoctor = doc3.IdDoctor, IdPatient = pat2.IdPatient, Date = new DateTime(2020,03,01), DueDate = new DateTime(2020,04,25) };
           
            _db.Add(prescr1);
            _db.Add(prescr2);
            _db.Add(prescr3);
            _db.Add(prescr4);
           
            await _db.SaveChangesAsync();

            PrescriptionMedicament pr_med_1 = new PrescriptionMedicament { IdPrescription = prescr1.IdPrescription, IdMedicament = med1.IdMedicament, Dose = 4, Details = "Take every morning" };
            PrescriptionMedicament pr_med_2 = new PrescriptionMedicament { IdPrescription = prescr1.IdPrescription, IdMedicament = med2.IdMedicament, Dose = 1, Details = "Once a week" };
            PrescriptionMedicament pr_med_3 = new PrescriptionMedicament { IdPrescription = prescr1.IdPrescription, IdMedicament = med3.IdMedicament, Dose = 3, Details = "once every two weeks" };
            PrescriptionMedicament pr_med_4 = new PrescriptionMedicament { IdPrescription = prescr2.IdPrescription, IdMedicament = med1.IdMedicament, Dose = 10, Details = "Every evening" };
            PrescriptionMedicament pr_med_5 = new PrescriptionMedicament { IdPrescription = prescr3.IdPrescription, IdMedicament = med2.IdMedicament, Dose = 7, Details = "once every day after main meal" };
    
            _db.Add(pr_med_1);
            _db.Add(pr_med_2);
            _db.Add(pr_med_3);
            _db.Add(pr_med_3);
            _db.Add(pr_med_4);
            _db.Add(pr_med_5);


            await _db.SaveChangesAsync();








        }
    
    }
}