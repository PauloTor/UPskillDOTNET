using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Models;
using ParqueAPICentral.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using ParquePrivateAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace ParqueAPICentral.Controllers
{



    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly APICentralContext _context;

        public ReservasController(APICentralContext context)
        {
            _context = context;
        }

        [EnableCors]
        [HttpGet("{DataInicio}/{DataFim}")]
        public async Task<ActionResult<IEnumerable<Reserva>>> PostReservaByData(String DataInicio, String DataFim)
        {
            var dateTimeInicio = DateTime.Parse(DataInicio);
            var dateTimeFim = DateTime.Parse(DataFim);
            string BaseUrl = "https://localhost:44365/";
            ReservaDto reserva;
            var ListaLugar = new List<LugarDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Route para Lugar por datas
                string endpoint = BaseUrl + "api/Lugares/" + DataInicio + "/" + DataFim;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                // Lugares disponiveis para criar Reserva
                ListaLugar = await response.Content.ReadAsAsync<List<LugarDto>>();
                long lugar = 0;

                if (ListaLugar.Count != 0)
                {
                    // Pega no primeiro da Lista
                    var Primeiro = ListaLugar.FirstOrDefault();
                    lugar = Primeiro.LugarID;
                }
                var datanow = DateTime.Now;
                //cria instancia para uma nova reserva
                reserva = new ReservaDto(datanow, dateTimeInicio, dateTimeFim, lugar);
                //Passa a reserva para formato JSON
                StringContent reserva_ = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                string endpoint2 = BaseUrl + "API/reservas/";
                // Post de uma nova reserva 
                var response2 = await client.PostAsync(endpoint2, reserva_);
            }
            return NoContent();
        }

        // DELETE: api/reservas/id - Cancelar reserva

        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            string BaseUrl = "https://localhost:44365/";
            var ListaReserva = new List<Reserva>();
            var ListaFaturas = new List<Fatura>();
            var ListaClientes = new List<Cliente>();
            using (HttpClient client = new HttpClient())

            {
                string endpoint = BaseUrl + "api/reservas/" + id;
                var response = await client.GetAsync(endpoint);
                ListaReserva = await response.Content.ReadAsAsync<List<Reserva>>();


                var reserva_ = ListaReserva.FirstOrDefault();
                var temp = reserva_.ReservaID;
                var temp2 = reserva_.ClienteID;
                // alteral modelo tirar reservaID clienteID para a reserva
                //var temp2 = reserva_.ClienteID;
                


                endpoint = BaseUrl + "api/faturas/" + temp;
                var response2 = await client.GetAsync(endpoint);
                ListaFaturas = await response2.Content.ReadAsAsync<List<Fatura>>();

                var reserva2 = ListaFaturas.FirstOrDefault();
                var temp_ = reserva2.PrecoFatura;



                var p = _context.Cliente.FindAsync(temp2);



                var response3 = await client.GetAsync(endpoint);
                ListaClientes = await response3.Content.ReadAsAsync<List<Cliente>>();
                var reserva4 = ListaClientes.FirstOrDefault();
                var cliente_ = reserva4.ClienteID

            }



            if (reserva == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}







