using Microsoft.AspNetCore.Mvc;
using TASK9.Models;
using TASK9.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace TASK9.Controllers;

[ApiController]

public class DoctorController : ControllerBase
{
    private ILogger _logger;
    private readonly IDoctorRepository _doctorRepository;
    public DoctorController(ILogger logger, IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    // Retrieve doctor by ID

    [Route("api/doctors/{idDoctor}")]
    [HttpGet()]
    public async Task<IActionResult> GetDoctorByID(int idDoctor)
    {
        try
        {
           
            var result = await _doctorRepository.GetDoctorByID(idDoctor);

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _logger.LogError(string.Format("Something went wrong while retrieving data of doctor with ID = {0}: {1}", idDoctor,ex.Message));
            return NotFound(string.Format("Something went wrong while retrieving data of doctor with ID = {0}: {1}", idDoctor,ex.Message));
        }

    }

    // Delete a doctor from db
    [Route("api/doctors/{idDoctor}")]
    [HttpDelete()]
    public async Task<IActionResult> DeleteDoctor(int idDoctor)
    {
        try
        {
            
            var result = await _doctorRepository.DeleteDoctor(idDoctor);

            return Ok(result);
        }
        catch (Exception ex)
        {   Console.WriteLine(ex);
            _logger.LogError(string.Format("Something went wrong while deleting the doctor: {0}", ex.Message));
            return NotFound(string.Format("Something went wrong while deleting the doctor: {0}", ex.Message));
        }

    }


    [Route("api/doctor")]
    [HttpGet()]
    public async Task<IActionResult> GetDoctors()
    {
        try
        {
           
            var result = await _doctorRepository.GetDoctors();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Something went wrong while retrieving data of patient"));
            return NotFound(string.Format("Something went wrong while retrieving data of patient"));
        }

    }
    [Route("api/patient")]
    [HttpGet()]
    public async Task<IActionResult> GetPatients()
    {
        try
        {
           
            var result = await _doctorRepository.GetPatients();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Something went wrong while retrieving data of patient"));
            return NotFound(string.Format("Something went wrong while retrieving data of patient"));
        }

    }
    [Route("api/prescription")]
    [HttpGet()]
    public async Task<IActionResult> GetPrescription()
    {
        try
        {
           
            var result = await _doctorRepository.GetPrescriptions();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Something went wrong while retrieving data of prescription"));
            return NotFound(string.Format("Something went wrong while retrieving data of prescription"));
        }

    }
    [Route("api/prescription_medicament")]
    [HttpGet()]
    public async Task<IActionResult> GetPrescriptionMedicament()
    {
        try
        {
           
            var result = await _doctorRepository.GetPrescriptionMedicaments();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Something went wrong while retrieving data of prescription_medicament"));
            return NotFound(string.Format("Something went wrong while retrieving data of prescription_medicament"));
        }

    }

        [Route("api/medicament")]
    [HttpGet()]
    public async Task<IActionResult> GetMedicament()
    {
        try
        {
           
            var result = await _doctorRepository.GetMedicaments();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Something went wrong while retrieving data of medicament"));
            return NotFound(string.Format("Something went wrong while retrieving data of medicament"));
        }

    }


 // Delete a doctor from db
    [Route("api/populate")]
    [HttpPost()]
    public  void PopulateDatabase(int idDoctor)
    {
        Console.WriteLine("Creating database");
        try
        {

            _doctorRepository.PopulateDatabase();

            
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("Something went wrong while deleting the doctor: {0}", ex.Message));
     
        }

    }

    

}