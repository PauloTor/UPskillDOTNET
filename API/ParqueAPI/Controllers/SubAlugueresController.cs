using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPI.Data;
using ParqueAPI.Models;

namespace ParqueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAlugueresController : ControllerBase
    {
        private readonly ParqueAPIContext _context;

        public SubAlugueresController(ParqueAPIContext context)
        {
            _context = context;
        }

        // GET: api/SubAlugueres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubAluguer>>> GetSubAluguer()
        {
            return await _context.SubAluguer.ToListAsync();
        }

        // GET: api/SubAlugueres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubAluguer>> GetSubAluguer(long id)
        {
            var subAluguer = await _context.SubAluguer.FindAsync(id);

            if (subAluguer == null)
            {
                return NotFound();
            }

            return subAluguer;
        }

        // PUT: api/SubAlugueres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubAluguer(long id, SubAluguer subAluguer)
        {
            if (id != subAluguer.SubAluguerID)
            {
                return BadRequest();
            }

            _context.Entry(subAluguer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubAluguerExists(id))
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

        // POST: api/SubAlugueres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubAluguer>> PostSubAluguer(SubAluguer subAluguer)
        {
            _context.SubAluguer.Add(subAluguer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubAluguer", new { id = subAluguer.SubAluguerID }, subAluguer);
        }

        // DELETE: api/SubAlugueres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubAluguer(long id)
        {
            var subAluguer = await _context.SubAluguer.FindAsync(id);
            if (subAluguer == null)
            {
                return NotFound();
            }

            _context.SubAluguer.Remove(subAluguer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubAluguerExists(long id)
        {
            return _context.SubAluguer.Any(e => e.SubAluguerID == id);
        }
    }
}
