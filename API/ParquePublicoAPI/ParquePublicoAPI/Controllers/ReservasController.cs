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
using System.IdentityModel.Tokens.Jwt;

namespace ParquePublicoAPI.Controllers
{
    [Authorize]
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ParquePublicoAPIContext _context;

        public ReservasController(ParquePublicoAPIContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [Authorize]
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReserva()
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return await _context.Reserva.Include(r => r.Lugar).Include(l => l.Lugar.Rua).ToListAsync();
        }

        // GET: api/Reservas/5
        [Authorize]
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(long id)
        {
            var reserva = await _context.Reserva
                         .Include(r => r.Lugar).Include(l => l.Lugar.Rua)
                         .FirstOrDefaultAsync(r => r.ReservaID == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(long id, Reserva reserva)
        {
            if (id != reserva.ReservaID)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
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

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            _context.Reserva.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.ReservaID }, reserva);
        }

        // DELETE: api/Reservas/cancelar/5
        [Authorize]
        [EnableCors]
        [HttpDelete("cancelar/{id}")]
        public async Task<IActionResult> DeleteReserva(long id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(long id)
        {
            return _context.Reserva.Any(e => e.ReservaID == id);
        }
    }
}
