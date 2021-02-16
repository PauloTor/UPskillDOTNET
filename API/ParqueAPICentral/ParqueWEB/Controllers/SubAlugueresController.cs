using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Cors;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/SubAlugueres")]
    [ApiController]
    public class SubAlugueresController : ControllerBase
    {
        private readonly APICentralContext _context;

        public SubAlugueresController(APICentralContext context)
        {
            _context = context;
        }


        // GET: api/SubAlugueres
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubAluguer>>> GetSubAlugueresTodos()
        {
            return await _context.SubAluguer.
                ToListAsync();
        }


        // GET: api/SubAlugueres/id
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubAluguer>> GetSubAlugueresById(long id)
        {
            var subAluguer = await _context.SubAluguer.Where(r => r.SubAluguerID == id).FirstOrDefaultAsync();

            if (subAluguer == null)
            {
                return NotFound();
            }
            return subAluguer;
        }


        // POST: api/SubAlugueres/{reservaID}/{preco}/
        [EnableCors]
        [HttpPost("{reservaID}/{preco}")]
        public async Task<ActionResult<SubAluguer>> PostSubAluguer(long reservaID, float preco)
        {
            SubAluguer subAluguer = new SubAluguer() { ReservaID = reservaID, Preco = preco };

            var reserva = _context.Reserva.Where(r => r.ReservaID == reservaID).FirstOrDefault();

            reserva.ParaSubAlugar(true);

            _context.Reserva.Update(reserva);

            _context.SubAluguer.Add(subAluguer);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubAluguer", new { id = subAluguer.SubAluguerID }, subAluguer);
        }      
    }
}
