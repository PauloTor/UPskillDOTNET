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
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;


namespace ParqueAPICentral.Controllers
{
    [Route("api/Faturas")]
    [ApiController]
    public class FaturasController : ControllerBase
    {
        private readonly APICentralContext _context;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;
        private readonly string apiBaseUrl2;


        public FaturasController(APICentralContext context, IConfiguration configuration)
        {
            _context = context;
            _configure = configuration;
            apiBaseUrl2 = _configure.GetValue<string>("WebAPICentralBaseUrl");
            apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        }


        // POST Faturas by ReservaID - api/Faturas/ReservaID
        [EnableCors]
        [HttpGet("{ReservaID}")]
        public async Task<ActionResult<IEnumerable<Fatura>>> PostFaturaByReservaID(long ReservaID)
        {
            var reserva = await _context.Reserva.FindAsync(ReservaID);
            var ListaReservas = new List<Reserva_>();
            var ListaLugares = new List<Lugar>();
            Fatura fatura;
            using (var client = new HttpClient())

            {

                var pesquisaReserva = reserva.ReservaID;
                //  client.BaseAddress = new Uri(BaseUrl);
                //  client.DefaultRequestHeaders.Clear();
                //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Route para Reservas
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                string endpoint = apiBaseUrl + "Reservas/" + pesquisaReserva;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var _Reserva = await response.Content.ReadAsAsync<Reserva_>();

                var reservaIdent = _Reserva.ReservaID;

                var _lugar = _Reserva.LugarID;
                var _dataInicio = _Reserva.DataInicio;
                var _dataFim = _Reserva.DataFim;

                // Route para LugarID (_lugar)
                string endpoint2 = apiBaseUrl + "Lugares/" + _lugar;
                var response2 = await client.GetAsync(endpoint2);
                response2.EnsureSuccessStatusCode();
                var _Lugar = await response2.Content.ReadAsAsync<Lugar>();

                //Preco por hora
                float _preco = _Lugar.Preço;

                //Conversão para horas
                var timeSpan = _dataFim.Subtract(_dataInicio);
                var horas = timeSpan.Hours;

                //Preço por hora * quantidade de horas
                float PrecoFatura = horas * _preco;

                var dataFatura = DateTime.Now;

                //cria instancia para uma nova fatura
                fatura = new Fatura(dataFatura, PrecoFatura, reservaIdent);
                //Passa a fatura para formato JSON
                //StringContent fatura_ = new StringContent(JsonConvert.SerializeObject(fatura), Encoding.UTF8, "application/json");
                //string endpoint3 = apiBaseUrl2 + "Faturas/";
                ////POST Fatura
               // var response3 = await client.PostAsync(endpoint3, fatura_);

                _context.Fatura.Add(fatura);
                await _context.SaveChangesAsync();

                //return CreatedAtAction("GetFatura", new { id = fatura.FaturaID }, fatura);


            }
            return NoContent();
        }

    }
}
