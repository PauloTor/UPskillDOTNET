using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParqueAPICentral.Data;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using ParqueAPICentral.Services;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Faturas")]
    [ApiController]
    public class FaturasController : ControllerBase
    {
        private readonly APICentralContext _context;

        //private readonly FaturaService _service;
        //private readonly IConfiguration _configure;
        //private readonly string apiBaseUrl;
        //private readonly string apiBaseUrl2;

        public FaturasController(APICentralContext context)
        {
            _context = context;
        }



        //public FaturasController(FaturaService service, IConfiguration configuration)
        //{
        //    this._service = service;
        //    _configure = configuration;
        //    apiBaseUrl2 = _configure.GetValue<string>("WebAPICentralBaseUrl");
        //    apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        //}

        //// POST Faturas by ReservaID - api/Faturas/Reserva/ReservaID
        //[EnableCors]
        //[HttpPost("Reserva/{ReservaID}")]
        //public async Task<ActionResult<Fatura>> PostFaturaByReservaID(long reservaID)
        //{
        //    return await this._service.CreateFaturaByReservaID(reservaID);
        //}

        //// GET Faturas by FaturaID - api/Faturas/5
        //[HttpGet("{FaturaID}")]
        //public IActionResult GetFatura(long FaturaID)
        //{
        //    var Fatura = this._service.FindFaturaByID(FaturaID);

        //    if (Fatura == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Fatura);

        //}

        // POST Faturas by ReservaID - api/Faturas/ReservaID - sem Services e Repositories
        [EnableCors]
        [HttpPost("{ReservaID}")]
        public async Task<ActionResult<IEnumerable<Fatura>>> PostFaturaByReserva(long ReservaID)
        {
            var reserva = await _context.Reserva.FindAsync(ReservaID);
            string BaseUrl = "https://localhost:44365/";
            string BaseUrl2 = "https://localhost:44330/";
            var ListaReservas = new List<Reserva_>();
            var ListaLugares = new List<Lugar_>();
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

                var _Reserva = await response.Content.ReadAsAsync<Reserva_>();

                var reservaIdent = _Reserva.ReservaID;

                var _lugar = _Reserva.LugarID;
                var _dataInicio = _Reserva.DataInicio;
                var _dataFim = _Reserva.DataFim;

                // Route para LugarID (_lugar)
                string endpoint2 = BaseUrl + "api/Lugares/" + _lugar;
                var response2 = await client.GetAsync(endpoint2);
                response2.EnsureSuccessStatusCode();
                var _Lugar = await response2.Content.ReadAsAsync<Lugar_>();

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

        // GET Faturas by FaturaID - api/Faturas/5 - sem services ou repositories
        [EnableCors]
        [HttpGet("{FaturaID}")]
        public async Task<ActionResult<Fatura>> GetFatura(long FaturaID)
        {
            var fatura = await _context.Fatura.FindAsync(FaturaID);

            if (fatura == null)
            {
                return NotFound();
            }

            return fatura;
        }

    }
}


