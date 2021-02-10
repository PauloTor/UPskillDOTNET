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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParqueAPICentral.Data;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Controllers
{
    [Route("api/SubAlugueres")]
    [ApiController]
    public class SubAlugueresController : ControllerBase
    {
        private readonly APICentralContext _context;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        public SubAlugueresController(APICentralContext context, IConfiguration configuration)
        {
            _context = context;
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        }

        //// GET: api/SubAlugueres
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<SubAluguer>>> GetSubAluguer()
        //{
        //    return await _context.SubAluguer.ToListAsync();
        //}

        // POST: api/SubAlugueres/{Clienteid}/{Reservaid}/{PrecoHoraid}
        [HttpGet("{Clienteid}/{Reservaid}/{PrecoHoraid}")]
        public async Task<ActionResult<SubAluguer>> CreateSubAluguer(long Clienteid, long Reservaid, float PrecoHoraid)
        {
            var cliente = await _context.Cliente.FindAsync(Clienteid);

            var precoLugar_ = new List<PrecoLugar>();
            PrecoLugar precoLugarNovo;
            SubAluguer subAluguer;

            using (HttpClient client = new HttpClient())
            {
                string EndpointReserva = apiBaseUrl + "Reservas/" ;
                var response = await client.GetAsync(EndpointReserva);
                response.EnsureSuccessStatusCode();
        
                List<Reserva_> ListaReservas = await response.Content.
                    ReadAsAsync<List<Reserva_>>();

                var umareserva = ListaReservas.Where(r => r.ReservaID == Reservaid).
                    FirstOrDefault();

                var datainicio = umareserva.DataInicio;
                var datafim = umareserva.DataFim;

                var LugarReserva = umareserva.ReservaID;
                // apaga reserva(API)

                precoLugarNovo = new PrecoLugar(LugarReserva, PrecoHoraid, datainicio, datafim);

                StringContent reservaJson = new StringContent(JsonConvert.
                    SerializeObject(precoLugarNovo), Encoding.UTF8, "application/json");
                
                string endpoint2 = apiBaseUrl + "PrecoLugar/";
                // Post de uma nova reserva 


                // cria novo preco para lugar
                var response2 = await client.PostAsync(endpoint2, reservaJson);

                var datanow = DateTime.Now;

                subAluguer = new SubAluguer(PrecoHoraid, datanow, datainicio, datafim, Reservaid);

                _context.SubAluguer.Add(subAluguer);
                await _context.SaveChangesAsync();


                // disponibiliza reserva
                var deleteTask = client.DeleteAsync(EndpointReserva+Reservaid);
                deleteTask.Wait();


                
                
                
                
                //return await _context.SubAluguer.ToListAsync();

                return NoContent();
            }

        }
        //    // PUT: api/SubAlugueres/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutSubAluguer(long id, SubAluguer subAluguer)
        //    {
        //        if (id != subAluguer.SubAluguerID)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(subAluguer).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SubAluguerExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/SubAlugueres
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<SubAluguer>> PostSubAluguer(SubAluguer subAluguer)
        //    {
        //        _context.SubAluguer.Add(subAluguer);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetSubAluguer", new { id = subAluguer.SubAluguerID }, subAluguer);
        //    }

        //    // DELETE: api/SubAlugueres/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteSubAluguer(long id)
        //    {
        //        var subAluguer = await _context.SubAluguer.FindAsync(id);
        //        if (subAluguer == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.SubAluguer.Remove(subAluguer);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool SubAluguerExists(long id)
        //    {
        //        return _context.SubAluguer.Any(e => e.SubAluguerID == id);
        //    }
        //}
    }
}
