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
    [Route("api/Lugares")]
    [ApiController]
    public class LugaresController : ControllerBase
    {
        private readonly ParquePublicoAPIContext _context;

        public LugaresController(ParquePublicoAPIContext context)
        {
            _context = context;
        }
        // GET: api/Lugares
        [Authorize]
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lugar>>> GetLugar()
        {
            return await _context.Lugar.Include(l => l.Rua).ToListAsync();
        }

        // GET: api/Lugares/5
        [Authorize]
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Lugar>> GetLugar(long id)
        {
            var lugar = await _context.Lugar
                       .Include(l => l.Rua)
                       .FirstOrDefaultAsync(r => r.LugarID == id);

            if (lugar == null)
            {
                return NotFound();
            }

            return lugar;
        }

        // PUT: api/Lugares/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLugar(long id, Lugar lugar)
        {
            if (id != lugar.LugarID)
            {
                return BadRequest();
            }

            _context.Entry(lugar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LugarExists(id))
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

        // POST: api/Lugares
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Lugar>> PostLugar(Lugar lugar)
        {
            _context.Lugar.Add(lugar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLugar", new { id = lugar.LugarID }, lugar);
        }

        // DELETE: api/Lugares/5
        [Authorize]
        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLugar(long id)
        {
            var lugar = await _context.Lugar.FindAsync(id);
            if (lugar == null)
            {
                return NotFound();
            }

            _context.Lugar.Remove(lugar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LugarExists(long id)
        {
            return _context.Lugar.Any(e => e.LugarID == id);
        }

        // GET: api/Lugares/data1,data2 Pesquisar lugares sem reserva
        [HttpGet("{dateInicio}/{dateFim}")]
        public async Task<ActionResult<IEnumerable<Lugar>>> GetLugaresSemReserva(string dateInicio, string dateFim)
        {
            var dateTimeInicio = DateTime.Parse(dateInicio);
            var dateTimeFim = DateTime.Parse(dateFim);

            //validar datas

            if (dateTimeInicio >= dateTimeFim)
            {
                return BadRequest();
            }

            var reservasTimeFrame = _context.Reserva.Where(n => (n.DataInicio >= dateTimeInicio && n.DataFim <= dateTimeFim)
                                                || (n.DataInicio < dateTimeInicio && n.DataInicio < dateTimeFim && dateTimeFim < n.DataFim)
                                                || (n.DataFim > dateTimeFim && n.DataInicio < dateTimeInicio && dateTimeInicio < n.DataFim))
                                                    .Select(n => n.LugarID).ToList();

            var lugaresDisponiveis = await _context.Lugar.Include(n => n.Rua).Where(n => !reservasTimeFrame.Contains(n.LugarID)).ToListAsync();

            return lugaresDisponiveis;
        }
    }
}
