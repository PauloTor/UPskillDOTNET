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
                List<LugarDto> ListaLugar = await response.Content.ReadAsAsync<List<LugarDto>>();
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

/*        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
           // public async Task<ActionResult<Reserva>> CancelarReserva(long id)
        {
            string endpoint = BaseUrl + "api/reservas/" + id;
            var reservaRes = await client.GetAsync(endpoint);
            List<Reserva> ListaReserva = await reservaRes.Content.ReadAsAsync<List<Reserva>>();
            var reserva_ = await reservaRes.Content.ReadAsAsync<Reserva>();

            var reserva_ = ListaReserva.FirstOrDefault();
            var reservaById = reserva_.ReservaID;
            var reservaByCliente = reserva_.ClienteID;



            var reservaByCliente = _context.Reserva.Find(reservaById);
            //var reservaDeCliente = reservaByCliente.ClienteID

            endpoint = BaseUrl + "api/faturas/" + reservaById;
            var faturaRes = await client.GetAsync(endpoint);
            List<Fatura> ListaFaturas = await faturaRes.Content.ReadAsAsync<List<Fatura>>();
            var fatura_ = _context.Fatura.Find(reservaById); ;

            var fatura_ = ListaFaturas.FirstOrDefault();
            var faturaPreco = fatura_.PrecoFatura;

            var res = _context.Cliente.FindAsync(reservaByCliente);
            var res = _context.Cliente.Find(reservaByCliente);

            var response3 = await client.GetAsync(endpoint);
            List<Cliente> ListaClientes = await response3.Content.ReadAsAsync<List<Cliente>>();
            var reserva4 = ListaClientes.FirstOrDefault();
            var cliente_ = reserva4.ClienteID;

            reservaRes = await client.DeleteAsync(endpoint);
        }

        await _context.SaveChangesAsync();
            return NoContent();
        }
    
        */
        
        }
}

