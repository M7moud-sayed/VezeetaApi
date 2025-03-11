using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testV.Models;


namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        VezeetaContext db;
        public DoctorController(VezeetaContext db)
        {
            this.db = db;
        }

        [HttpGet("Doctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await db.GetAllDoctorsFullInfoAsync();

            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found.");

            return Ok(doctors);
        }




        [HttpGet("search")]
        public async Task<IActionResult> GetDoctorsBySearch(
             [FromQuery] string? speciality = null,
             [FromQuery] string? governorate = null,
             [FromQuery] string? city = null)
        {
            if (string.IsNullOrWhiteSpace(speciality) &&
                string.IsNullOrWhiteSpace(governorate) &&
                string.IsNullOrWhiteSpace(city))
            {
                var allDoctors = await db.GetAllDoctorsFullInfoAsync();
                return Ok(allDoctors);
            }

            var doctors = await db.FindDoctorsBySearchAsync(speciality, governorate, city);

            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found with the given criteria.");

            return Ok(doctors);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> FindDoctorByID(int id)
        {
            var doctor = await db.FindDoctorByIDAsync(id);

            if (doctor == null)
            {
                return NotFound($"Doctor with ID {id} not found.");
            }

            return Ok(doctor);
        }

        [HttpGet("/name={name:alpha}")]
        public async Task<IActionResult> SearchDoctorByName(string name)
        {
            var doctors = await db.FindDoctorByNameAsync(name);

            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found with this name.");

            return Ok(doctors);
        }





    }
}
