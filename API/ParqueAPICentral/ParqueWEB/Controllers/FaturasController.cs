using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParqueAPICentral.Data;
using ParqueAPICentral.Models;
using ParquePrivateAPI.Models;

namespace ParqueAPICentral.Controllers
{
    [Route("api/Faturas")]
    [ApiController]
    public class FaturasController : ControllerBase
    {
        private readonly APICentralContext _context;

        public FaturasController(APICentralContext context)
        {
            _context = context;
        }

        // POST Faturas by ReservaID - api/Faturas/ReservaID
        [EnableCors]
        [HttpGet("{ReservaID}")]
        public async Task<ActionResult<IEnumerable<Fatura>>> PostFaturaByReservaID(long ReservaID)
        {
            var reserva = await _context.Reserva.FindAsync(ReservaID);
            string BaseUrl = "https://localhost:44365/";
            string BaseUrl2 = "https://localhost:44330/";
            var ListaReservas = new List<ReservaDto>();
            var ListaLugares = new List<LugarDto>();
            Fatura fatura;
            using (var client = new HttpClient())

            {

                var pesquisaReserva = reserva.ReservaID;
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Route para Reservas
                string endpoint = BaseUrl + "api/Reservas/" + pesquisaReserva;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var _Reserva = await response.Content.ReadAsAsync<ReservaDto>();

                var reservaIdent = _Reserva.ReservaID;

                var _lugar = _Reserva.LugarID;
                var _dataInicio = _Reserva.DataInicio;
                var _dataFim = _Reserva.DataFim;

                // Route para LugarID (_lugar)
                string endpoint2 = BaseUrl + "api/Lugares/" + _lugar;
                var response2 = await client.GetAsync(endpoint2);
                response2.EnsureSuccessStatusCode();
                var _Lugar = await response2.Content.ReadAsAsync<LugarDto>();

                //Preco por hora
                var _preco = _Lugar.Preço;

                //Conversão para horas
                var timeSpan = _dataFim.Subtract(_dataInicio);
                var horas = timeSpan.Hours;

                //Preço por hora * quantidade de horas
                var PrecoFatura = horas * _preco;

                var dataFatura = DateTime.Now;

                //cria instancia para uma nova fatura
                fatura = new Fatura(dataFatura, PrecoFatura, reservaIdent);
                //Passa a fatura para formato JSON
                StringContent fatura_ = new StringContent(JsonConvert.SerializeObject(fatura), Encoding.UTF8, "application/json");
                string endpoint3 = BaseUrl2 + "API/Faturas/";
                //POST Fatura
                var response3 = await client.PostAsync(endpoint3, fatura_);

                _context.Fatura.Add(fatura);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetFatura", new { id = fatura.FaturaID }, fatura);

                return NoContent();
            }

        }

    }
}
