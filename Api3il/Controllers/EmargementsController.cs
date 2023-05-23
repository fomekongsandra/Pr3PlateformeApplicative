using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api3il.Models;

namespace Api3il.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmargementsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public EmargementsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Emargements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emargement>>> Getemargement()
        {
          if (_context.emargement == null)
          {
              return NotFound();
          }
            return await _context.emargement.ToListAsync();
        }

        // GET: api/Emargements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emargement>> GetEmargement(int id)
        {
          if (_context.emargement == null)
          {
              return NotFound();
          }
            var emargement = await _context.emargement.FindAsync(id);

            if (emargement == null)
            {
                return NotFound();
            }

            return emargement;
        }

        // PUT: api/Emargements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmargement(int id, Emargement emargement)
        {
            if (id != emargement.Id)
            {
                return BadRequest();
            }

            _context.Entry(emargement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmargementExists(id))
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

        // POST: api/Emargements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emargement>> PostEmargement(Emargement emargement)
        {
          if (_context.emargement == null)
          {
              return Problem("Entity set 'ApplicationContext.emargement'  is null.");
          }
            _context.emargement.Add(emargement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmargement", new { id = emargement.Id }, emargement);
        }

        // DELETE: api/Emargements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmargement(int id)
        {
            if (_context.emargement == null)
            {
                return NotFound();
            }
            var emargement = await _context.emargement.FindAsync(id);
            if (emargement == null)
            {
                return NotFound();
            }

            _context.emargement.Remove(emargement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmargementExists(int id)
        {
            return (_context.emargement?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
    //verifier
}
