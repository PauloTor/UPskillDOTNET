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
using ParqueAPICentral.Entities;
using SafetyTourism.Models;
using Microsoft.Extensions.Configuration;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly APICentralContext _context;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;


        public ReservasController(APICentralContext context, IConfiguration configuration)
        {
            _context = context;
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        }
       
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> GetReservas()
        {
            var ListaReservas = new List<ReservaDto>();
            using (var client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
                // Route para Lugar por datas
                string endpoint = apiBaseUrl + "Reservas/";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                ListaReservas = await response.Content.ReadAsAsync<List<ReservaDto>>();
            }
            return ListaReservas;
        }

        [EnableCors]
        [HttpGet("{DataInicio}/{DataFim}/{Cliente}")]
        public async Task<ActionResult<IEnumerable<Reserva>>> PostReservaByData(String DataInicio, String DataFim, long ClienteID)
        {
            var dateTimeInicio = DateTime.Parse(DataInicio);
            var dateTimeFim = DateTime.Parse(DataFim);
            ReservaDto reserva;
            
            using (var client = new HttpClient())
            {
                var cliente = await _context.Cliente.FindAsync(ClienteID);
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;

                // Route para Lugar por datas
                string endpoint = apiBaseUrl + "Lugares/" + DataInicio + "/" + DataFim;
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
                //Nova reserva
                reserva = new ReservaDto(datanow, dateTimeInicio, dateTimeFim, lugar);
                //Passa a reserva para formato JSON
                StringContent reserva_ = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                string endpoint2 = apiBaseUrl + "reservas/";
                // Post de uma nova reserva 
                var response2 = await client.PostAsync(endpoint2, reserva_);
                var reserva1 = new Reserva(reserva.ReservaID, ClienteID);
                _context.Reserva.Add(reserva1);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
        // DELETE: api/reservas/id - Cancelar reserva
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            using (HttpClient client = new HttpClient())
            {             
                string endpoint = apiBaseUrl + "reservas/" + id;
                             
                var reservaRes = await client.GetAsync(endpoint);
                
                var reserva_ = await reservaRes.Content.ReadAsAsync<Reserva>();
               
                var reservaById = reserva_.ReservaID;
                
                var reservaByCliente = _context.Reserva.Find(reservaById);
               
                var fatura_ = _context.Fatura.Find(reservaById); ;
                
                var faturaPreco = fatura_.PrecoFatura;
               
                var res = _context.Cliente.Find(reservaByCliente);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

