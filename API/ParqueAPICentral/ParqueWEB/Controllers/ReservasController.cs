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

        public ReservasController(APICentralContext context)
        {
            _context = context;
        }
        /// <summary>
        /// ////////////////
        /// </summary>
        /// <param name="parqueID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET: api/reservas/parqueID/id - Reservas de um Parque por ReservaID
        [EnableCors]
        [HttpGet]
        [Route("~/reservasid/{parqueID}/{id}")]
        public async Task<ActionResult<Reserva_>> GetReservasById(long parqueID, long id)
        {
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);

            Reserva_ reserva_;

            using (var client = new HttpClient())
            {
                string endpoint = parque.Url + "reservas/" + id;

                var response = await client.GetAsync(endpoint);

                reserva_ = await response.Content.ReadAsAsync<Reserva_>();
            }
            return reserva_;
        }
        /// <summary>
        /// //////////////////////
        /// </summary>
        /// <param name="parqueID"></param>
        /// <returns></returns>
        //GET: api/reservas/parqueID - Todas as Reservas de um Parque
        [EnableCors]
        [HttpGet]
        [Route("/getreserva/{parqueID}")]
        public async Task<ActionResult<IEnumerable<Reserva_>>> GetReservasByParque(long parqueID)
        {
            var listaReservas = new List<Reserva_>();

            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);

            using (var client = new HttpClient())
            {
                string endpoint = parque.Url + "reservas/";

                var response = await client.GetAsync(endpoint);

                listaReservas = await response.Content.ReadAsAsync<List<Reserva_>>();
            }
            return listaReservas;
        }
        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="DataFim"></param>
        /// <param name="ClienteID"></param>
        /// <param name="parqueid"></param>
        /// <returns></returns>
        [EnableCors]
        [HttpGet("{DataInicio}/{DataFim}/{ClienteID}/{ParqueID}")]
        public async Task<ActionResult<Reserva_>> PostReservaByData(String DataInicio, String DataFim, long ClienteID, long parqueid)
        {
            if (DateTime.Parse(DataInicio) > DateTime.Parse(DataFim))
            {
                return NotFound();
            }
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueid);

            using (var client = new HttpClient())
            {
                var rtoken = await GetToken(parque.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                var parkingLots = await (GetLugaresDisponiveis(DataInicio, DataFim, parqueid));
                var i = parkingLots.Value.FirstOrDefault();

                if (DateTime.Parse(DataInicio) > DateTime.Parse(DataFim))
                {
                    return NotFound();
                }

                var reserva = new Reserva_(DateTime.Now, DateTime.Parse(DataInicio), DateTime.Parse(DataFim), i.LugarID);

                StringContent reserva_ = new StringContent(JsonConvert.
                    SerializeObject(reserva), Encoding.UTF8, "application/json");

                var response2 = await client.
                    PostAsync(parque.Url + "reservas/", reserva_);

                var UltimaReserva = await GetUltimaReservaPrivate(parqueid);

                try
                {
                    CriarReservaCentral(parque.ParqueID, UltimaReserva.Value.ReservaID, ClienteID);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Couldn't retrieve entities: {ex.Message}");
                }


                return CreatedAtAction(nameof(PostReservaByData),
                new { id = reserva.ReservaID }, reserva);

            }
            return Ok(reserva);
        }
        /// <summary>
        /// //////////////////////////
        /// </summary>
        /// <param name="parqueID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/reservas/cancelar/parqueID/id - Cancelar reserva
        [EnableCors]
        [HttpGet("~cancelar/{parqueID}/{id}")]
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