using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParquePrivateAPI.Models;
using ParquePrivateAPI.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace ParquePrivateAPI.Controllers
{
    [Authorize]
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Lugares")]
    [ApiController]
    public class LugaresController : ControllerBase
    {
        private readonly ParquePrivateAPIContext _context;

        public LugaresController(ParquePrivateAPIContext context)
        {
            _context = context;
        }
        
        // GET: api/Lugares
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lugar>>> GetLugar()
        {
            return await _context.Lugar.Include(l => l.Parque).Include(p => p.Parque.Morada).ToListAsync();
        }

        // GET: api/Lugares/5
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Lugar>> GetLugar(long id)
        {
            var lugar = await _context.Lugar
                        .Include(l => l.Parque).Include(p => p.Parque.Morada)
                        .FirstOrDefaultAsync(l => l.LugarID == id);

            if (lugar == null)
            {
                return NotFound();
            }

            return lugar;
        }

        // PUT: api/Lugares/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Lugar>> PostLugar(Lugar lugar)
        {
            _context.Lugar.Add(lugar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLugar", new { id = lugar.LugarID }, lugar);
        }

        // DELETE: api/Lugares/5
        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lugar>> DeleteLugar(long id)
        {
            var lugar = await _context.Lugar.FindAsync(id);
            if (lugar == null)
            {
                return NotFound();
            }

            _context.Lugar.Remove(lugar);
            await _context.SaveChangesAsync();

            return lugar;
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

            var reservasTimeFrame =  _context.Reserva.Where(n => (n.DataInicio >= dateTimeInicio && n.DataFim <= dateTimeFim)
                                                || (n.DataInicio < dateTimeInicio && n.DataInicio < dateTimeFim && dateTimeFim < n.DataFim)
                                                || (n.DataFim > dateTimeFim && n.DataInicio < dateTimeInicio && dateTimeInicio < n.DataFim))
                                                    .Select(n => n.LugarID).ToList();

            var lugaresDisponiveis = await _context.Lugar.Include(n => n.Parque).Where(n => !reservasTimeFrame.Contains(n.LugarID)).ToListAsync();

            return lugaresDisponiveis;
        }
    }
}