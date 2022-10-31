using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Controllers
{
    
    [Route("api/Menus")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public MenusController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [Route("GetMenus")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menus>>> GetMenuss()
        {
            return await _context.Menuss.ToListAsync();
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menus>> GetMenus(int id)
        {
            var menus = await _context.Menuss.FindAsync(id);
            if (menus == null)
            {
                return NotFound();
            }
            return menus;
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenus(int id, Menus menus)
        {
            if (id != menus.MenuId)
            {
                return BadRequest();
            }
            _context.Entry(menus).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenusExists(id))
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

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menus>> PostMenus(Menus menus)
        {
            _context.Menuss.Add(menus);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMenus", new { id = menus.MenuId }, menus);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenus(int id)
        {
            var menus = await _context.Menuss.FindAsync(id);
            if (menus == null)
            {
                return NotFound();
            }

            _context.Menuss.Remove(menus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenusExistsss(int id)
        {
            return _context.Menuss.Any(e => e.MenuId == id);
        }
    }
}
