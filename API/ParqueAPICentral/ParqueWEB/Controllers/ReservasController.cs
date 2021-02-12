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
        private readonly string apiBaseUrlPrivado;
        private readonly string apiBaseUrlPublico;
        private string UrlToUse;


        public ReservasController(APICentralContext context, IConfiguration configuration)
        {
            _context = context;
            _configure = configuration;
            apiBaseUrlPrivado = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
            apiBaseUrlPublico = _configure.GetValue<string>("WebAPIPublicBaseUrl");
        }

        //GET: api/reservas/parqueID/id - Reservas de um Parque por ReservaID
        [EnableCors]
        [HttpGet]
        [Route("{parqueID}/{id}")]
        public async Task<ActionResult<Reserva_>> GetReservasById(long parqueID, long id)
        {
            //var listaReservas = new List<Reserva_>();

            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);

            Reserva_ reserva_;

            using (var client = new HttpClient())
            {
                string endpoint = parque.Url + "reservas/" + id;

                var response = await client.GetAsync(endpoint);

                dynamic tokenresponsecontent = await response.Content.ReadAsAsync<object>();

                string rtoken = tokenresponsecontent.jwtToken;

                reserva_ = await response.Content.ReadAsAsync<Reserva_>();
            }
            return reserva_;
        }

        //GET: api/reservas/parqueID - Todas as Reservas de um Parque
        [EnableCors]
        [HttpGet]
        [Route("{parqueID}")]
        public async Task<ActionResult<IEnumerable<Reserva_>>> GetReservasByParque(long parqueID)
        {
            var listaReservas = new List<Reserva_>();

            var parque =  await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);

            using (var client = new HttpClient())
            {
                string endpoint = parque.Url + "reservas/";

                var response = await client.GetAsync(endpoint);

                dynamic tokenresponsecontent = await response.Content.ReadAsAsync<object>();

                string rtoken = tokenresponsecontent.jwtToken;

                listaReservas = await response.Content.ReadAsAsync<List<Reserva_>>();
            }
            return listaReservas;
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
                var responseLogin = await client.PostAsync(apiBaseUrlPrivado + "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;

                // Route para Lugar por datas
                string endpoint = apiBaseUrlPrivado + "Lugares/" + DataInicio + "/" + DataFim;
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
                string endpoint2 = apiBaseUrlPrivado + "reservas/";
                string endpoint3 = apiBaseUrlPrivado + "Parques/";
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
                var nif__ = nif_.NIFParque;
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

        // DELETE: api/reservas/cancelar/parqueID/id - Cancelar reserva
        [EnableCors]
        [HttpGet("{parqueID}/{id}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long parqueID, long id)
        {
            
            var reserva = _context.Reserva.Where(r => r.ReservaAPI == id).Where(r => r.ParqueID == parqueID).FirstOrDefault();

            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);

            if (reserva == null)
            {
                return NotFound();
            }

            using (HttpClient client = new HttpClient())
            {
                var parquePublico = reserva.Publico;

                string endpoint = parque.Url + "reservas/" + "cancelar/" + id;

                var reservaRes = await client.GetAsync(endpoint);

                var reserva_ = await reservaRes.Content.ReadAsAsync<Reserva_>();

                long reservaById = reserva.ReservaID;

                long clienteById = reserva.ClienteID;

                var cliente_ = _context.Cliente.Where(c => c.ClienteID == clienteById).FirstOrDefault();

                var fatura_ = _context.Fatura.Where(f => f.ReservaID == reservaById).FirstOrDefault();

                if (fatura_ != null)
                {
                    float precoFatura = fatura_.PrecoFatura;

                    cliente_.Depositar(precoFatura);
                }                

                _context.Reserva.Remove(reserva);

                var deleteTask = client.DeleteAsync(endpoint);

            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}