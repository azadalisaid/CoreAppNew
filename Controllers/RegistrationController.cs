using CoreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{
    [Route("api/Registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public RegistrationController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [Route("GetUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetUsers()
        {
            return await (from c in _context.Registrations
                          join d in _context.Countries on c.CountryId equals d.CountryId
                          join e in _context.States on c.StateId equals e.StateId
                          select c).ToListAsync();
        }

        [Route("GetStates")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates(int CountryId)
        {
            return await _context.States.Where(c=>c.CountryId==CountryId).ToListAsync();
        }


        [Route("GetCountries")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetUser(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return registration;
        }

        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("UpdateRegistation")]
        [HttpPost]
        public async Task<IActionResult> UpdateRegistation([FromForm] Registration registration)
        {
            //if (paymentDetail.PaymentDetailId)
            //{
            //    return BadRequest();
            //}
            if (registration.PhotoFile != null)
            {
                IFormFile file = registration.PhotoFile;
                string fileName = file.FileName;
                long length = file.Length;
                if (length < 0)
                    return BadRequest();
                using var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[length];
                fileStream.Read(bytes, 0, (int)file.Length);
                registration.photo = bytes;
            }
            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationDetailExists(registration.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/PaymentDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("InsertUser")]
        [HttpPost]
        public async Task<ActionResult<Registration>> Registration([FromForm] Registration register)
        {
            if (register.PhotoFile != null)
            {
                IFormFile file = register.PhotoFile;
                //// string fileName = file.FileName;
                long length = file.Length;
                if (length < 0)
                    return BadRequest();
                using var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[length];
                fileStream.Read(bytes, 0, (int)file.Length);
                register.photo = bytes;
            }
            //var file = Request.Form.Files[0];
            _context.Registrations.Add(register);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsers", new { id = register.id }, register);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RegistrationDetailExists(int id)
        {
            return _context.Registrations.Any(e => e.id == id);
        }
    }
}
