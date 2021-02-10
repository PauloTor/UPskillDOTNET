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
using Newtonsoft.Json;
using System.Text;
using ParqueAPICentral.Entities;
using Microsoft.Extensions.Configuration;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Services;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly APICentralContext _context;
        private readonly ReservaService _service;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;


        public ReservasController(APICentralContext context, IConfiguration configuration, ReservaService service)
        {
            _context = context;
            this._service = service;
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        }
       
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva_>>> GetReservas()
        {
            return await this._service.GetAllReservas();
        }

        [EnableCors]
        [HttpGet("{DataInicio}/{DataFim}/{Cliente}")]
        public async Task<ActionResult<IEnumerable<Reserva>>> PostReservaByData(String DataInicio, String DataFim, long ClienteID)
        {
            var dateTimeInicio = DateTime.Parse(DataInicio);
            var dateTimeFim = DateTime.Parse(DataFim);
            Reserva_ reserva;
            
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
                List<Lugar_> ListaLugar = await response.Content.ReadAsAsync<List<Lugar_>>();
                long lugar = 0;
                if (ListaLugar.Count != 0)
                {
                 // Pega no primeiro da Lista
                    var Primeiro = ListaLugar.FirstOrDefault();
                    lugar = Primeiro.LugarID;
                }
                var datanow = DateTime.Now;
                //Nova reserva
                reserva = new Reserva_(datanow, dateTimeInicio, dateTimeFim, lugar);
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
                
                var reserva_ = await reservaRes.Content.ReadAsAsync<Reserva_>();
               
                long reservaById = reserva_.ReservaID;

                long clienteById = reserva.ClienteID;
               
                var fatura_ = _context.Fatura.Where(f => f.ReservaID == reservaById).FirstOrDefault();
                
                var cliente_ = _context.Cliente.Where(c => c.ClienteID == clienteById).FirstOrDefault();
               
                float precoFatura = fatura_.PrecoFatura;

                cliente_.Depositar(precoFatura);

                _context.Reserva.Remove(reserva);

                var deleteTask = client.DeleteAsync(endpoint);

            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

