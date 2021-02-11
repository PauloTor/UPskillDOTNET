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
        public async Task<ActionResult<IEnumerable<Reserva_>>> GetReservas()
        {
            var ListaReservas = new List<Reserva_>();
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
                ListaReservas = await response.Content.ReadAsAsync<List<Reserva_>>();
            }
            return ListaReservas;
        }

        [EnableCors]
        [HttpPost("{DataInicio}/{DataFim}/{ClienteID}")]
        public async Task<ActionResult<IEnumerable<Reserva_>>> PostReservaByData(String DataInicio, String DataFim, long ClienteID)
        {
            var dateTimeInicio = DateTime.Parse(DataInicio);
            var dateTimeFim = DateTime.Parse(DataFim);

            if (dateTimeInicio > dateTimeFim)
            {
                return NotFound();
            }

            Reserva_ reserva;

            using (var client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
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
                if (ListaLugar.Count == 0)
                {
                    return NotFound();
                }
                // Pega no primeiro da Lista
                var Primeiro = ListaLugar.FirstOrDefault();
                lugar = Primeiro.LugarID;

                var datanow = DateTime.Now;
                //Nova reserva
                reserva = new Reserva_(datanow, dateTimeInicio, dateTimeFim, lugar);
                //Passa a reserva para formato JSON
                StringContent reserva_ = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                string endpoint2 = apiBaseUrl + "reservas/";
                string endpoint3 = apiBaseUrl + "Parques/";
                // Post de uma nova reserva 
                var response2 = await client.PostAsync(endpoint2, reserva_);

                var response3 = await client.GetAsync(endpoint2);
                List<Reserva_> ListaLugarUltimo = await response3.Content.ReadAsAsync<List<Reserva_>>();
                var reservaid_ = ListaLugarUltimo.LastOrDefault();
                long reservaid = reservaid_.ReservaID;
                //var fatura_ = _context.Fatura.Where(f => f.ReservaID == reservaById).FirstOrDefault();
                var nif = await client.GetAsync(endpoint3);
                List<Parque> ListaParques = await nif.Content.ReadAsAsync<List<Parque>>();
                var nif_ = ListaParques.FirstOrDefault();
                var nif__ = nif_.NifParque;
                var reserva1 = new Reserva(nif__, reservaid, ClienteID);
                _context.Reserva.Add(reserva1);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return NoContent();
        }
    

    // DELETE: api/reservas/id - Cancelar reserva
    [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long id,lo)
        {                   

            var reserva = _context.Reserva.Where(f => f.ReservaAPI == id).Where(f => f.NifParqueAPI == nifParque).FirstOrDefault();
            var reservaByID = reserva.ReservaID;


            if (reserva == null)
            {
                return NotFound();
            }

            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "reservas/" + id;

                var reservaRes = await client.GetAsync(endpoint);

                var reserva_ = await reservaRes.Content.ReadAsAsync<Reserva_>();

                //long reservaById = reserva_.ReservaID;

                long clienteById = reserva.ClienteID;

                var fatura_ = _context.Fatura.Where(f => f.ReservaID == reservaByID).FirstOrDefault();

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