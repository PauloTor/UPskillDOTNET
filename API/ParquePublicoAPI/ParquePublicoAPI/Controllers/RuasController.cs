using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParquePublicoAPI.Data;
using ParquePublicoAPI.Models;

namespace ParquePublicoAPI.Controllers
{
    [Authorize]
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Ruas")]
    [ApiController]
    public class RuasController : ControllerBase
    {
        private readonly ParquePublicoAPIContext _context;

        public RuasController(ParquePublicoAPIContext context)
        {
            _context = context;
        }

        // GET: api/Ruas
        [Authorize]
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rua>>> GetRua()
        {
            return await _context.Rua.ToListAsync();
        }

        // GET: api/Ruas/5
        [Authorize]
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Rua>> GetRua(long id)
        {
            var rua = await _context.Rua.FindAsync(id);

            if (rua == null)
            {
                return NotFound();
            }

            return rua;
        }

        // PUT: api/Ruas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRua(long id, Rua rua)
        {
            if (id != rua.RuaID)
            {
                return BadRequest();
            }

            _context.Entry(rua).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuaExists(id))
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

        // POST: api/Ruas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Rua>> PostRua(Rua rua)
        {
            _context.Rua.Add(rua);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRua", new { id = rua.RuaID }, rua);
        }

        // DELETE: api/Ruas/5
        [Authorize]
        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRua(long id)
        {
            var rua = await _context.Rua.FindAsync(id);
            if (rua == null)
            {
                return NotFound();
            }

            _context.Rua.Remove(rua);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuaExists(long id)
        {
            return _context.Rua.Any(e => e.RuaID == id);
        }
    }
}
