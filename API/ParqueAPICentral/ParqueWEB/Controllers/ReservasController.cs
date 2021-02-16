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


        // GET: api/Reservas
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservasTodas()
        {
            return await _context.Reserva.ToListAsync();
        }


        // GET: api/Reservas/id
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReservaById(long id)
        {
            var reserva = await _context.Reserva.Where(r => r.ReservaID == id).FirstOrDefaultAsync();

            if (reserva == null)
            {
                return NotFound();
            }
            return reserva;
        }
        /// <summary>
        /// //////////////////////
        /// </summary>
        /// <param name="parqueID"></param>
        /// <returns></returns>
        //GET: api/reservas/parque/parqueID - Todas as Reservas de um Parque
        [EnableCors]
        [HttpGet]
        [Route("parque/{parqueID}")]
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
        /// ////////////////
        /// </summary>
        /// <param name="parqueID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET: api/reservas/parque/parqueID/id - Reservas de um Parque por ReservaID
        [EnableCors]
        [HttpGet]
        [Route("parque/{parqueID}/{id}")]
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
            if (reserva_ == null)
            {
                return NotFound();
            }
            return reserva_;
        }
        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="DataFim"></param>
        /// <param name="ClienteID"></param>
        /// <param name="parqueid"></param>
        /// <returns></returns>
        /// 




        //Post Reservas by {DataInicio}/{DataFim}/{ClienteID}/{ParqueID}/{lugarId}
        [EnableCors]
        [HttpPost("{DataInicio}/{DataFim}/{ClienteID}/{ParqueID}/{lugarId}")]
        public async Task<ActionResult<Reserva_>> PostReservaByData(String DataInicio, String DataFim, long ClienteID, long parqueid, long lugarId)
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

                var parkingLots = await (GetLugaresDisponiveisComSubAlugueres(DataInicio, DataFim, parqueid));
                var i = parkingLots.Value.FirstOrDefault(p => p.LugarID == lugarId && p.parqueId == parqueid);

                if ((DateTime.Parse(DataInicio) > DateTime.Parse(DataFim)) || (parkingLots.Value.Count()==0))
                {
                    return NotFound("Data inválida");
                }

                if (i == null)
                {
                    return NotFound("Lugar não disponivel para ser reservado");
                }

                if (i.subReservado == false)
                {
                    var reserva = new Reserva_(DateTime.Now, DateTime.Parse(DataInicio), DateTime.Parse(DataFim), i.LugarID);

                    StringContent reserva_ = new StringContent(JsonConvert.
                        SerializeObject(reserva), Encoding.UTF8, "application/json");

                    var response2 = await client.
                        PostAsync(parque.Url + "reservas/", reserva_);

                    var UltimaReserva = await GetUltimaReservaPrivate(parqueid);

                    try
                    {
                        CriarReservaCentral(parque.ParqueID, UltimaReserva.Value.ReservaID, ClienteID, lugarId);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Couldn't retrieve entities: {ex.Message}");
                    }


                    return CreatedAtAction(nameof(PostReservaByData),
                    new { id = reserva.ReservaID }, reserva);
                }

                else
                {
                    var sub = _context.SubAluguer.FirstOrDefault(n => n.SubAluguerID == i.subAluguerId);

                    sub.Reservado = true;
                    sub.NovoCliente = ClienteID.ToString();

                    _context.SubAluguer.Update(sub);

                    _context.SaveChanges();

                    return CreatedAtAction(nameof(PostReservaByData),
                   new { id = sub.SubAluguerID }, sub);

                }

               
            }
        }
        /// <summary>
        /// //////////////////////////
        /// </summary>
        /// <param name="parqueID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/reservas/parqueID/reservaId - Cancelar reserva e devolver credito
        [EnableCors]
        [HttpDelete("{parqueID}/{reservaID}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long parqueID, long reservaID)
        {
            var reserva = _context.Reserva.Where(r => r.ReservaAPI == reservaID).Where(r => r.ParqueID == parqueID).FirstOrDefault();

            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);

            if (reserva == null)
            {
                return NotFound();
            }
            using (HttpClient client = new HttpClient())
            {
                string endpoint = parque.Url + "reservas/" + "cancelar/" + reservaID;

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
        /// <summary>
        /// ////////////////////
        /// </summary>
        /// <param name="apiBaseUrlPrivado"></param>
        /// <returns></returns>
        [EnableCors]
        public async Task<string> GetToken(string apiBaseUrlPrivado)
        {
            using (HttpClient client = new HttpClient())
            {

                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrlPrivado, contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;

                return rtoken;
            }
        }
        /// <summary>
        /// /////////////////
        /// </summary>
        /// <param name="DataInicio"></param>
        /// <param name="DataFim"></param>
        /// <param name="parqueID"></param>
        /// <returns></returns>
       
        private async Task<IEnumerable<Lugar_>> GetLugaresDisponiveis(String DataInicio, String DataFim, long parqueID)
        {
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);
            using (var client = new HttpClient())
            {
                var rtoken = await GetToken(parque.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                var response = await client.GetAsync(parque.Url + "Lugares/" + DataInicio + "/" + DataFim);
                response.EnsureSuccessStatusCode();
                var ListaLugar = await response.Content.ReadAsAsync<List<Lugar_>>();
                
                return ListaLugar;
            }
        }

        //GET Lugares disponíveis de ParqueID by Data1 e Data2
        [EnableCors]
        [HttpGet("LugaresDisponiveis/{DataInicio}/{DataFim}/{ParqueID}")]
        public async Task<ActionResult<IEnumerable<LugarReserva>>> GetLugaresDisponiveisComSubAlugueres(String DataInicio, String DataFim, long parqueID)
        {
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);
            using (var client = new HttpClient())
            {

                var rtoken = await GetToken(parque.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                var response = await client.GetAsync(parque.Url + "Reservas/" + DataInicio + "/" + DataFim);
                response.EnsureSuccessStatusCode();
                var listaReservas = await response.Content.ReadAsAsync<List<Reserva_>>();

                var reservas = await _context.Reserva.ToListAsync();

                var subAluguer = await _context.SubAluguer.ToListAsync();

                var lugaresNaoSubAlugados = new List<(long, float, long)>();

                foreach (var reserva in reservas)
                {
                    foreach (var reservaOriginal in listaReservas)
                    {
                        if (reservaOriginal.ReservaID == reserva.ReservaAPI && reserva.ParqueID == parqueID && reserva.ParaSubAluguer)
                        {
                            var sub = subAluguer.FirstOrDefault(s => s.ReservaID == reserva.ReservaID);

                            if (sub.Reservado == false)
                            {
                                lugaresNaoSubAlugados.Add((reserva.LugarID, sub.Preco, sub.SubAluguerID));

                                break;
                            }
                        }
                    }
                }

                var response2 = await client.GetAsync(parque.Url + "Lugares/");
                response2.EnsureSuccessStatusCode();
                var listaLugares = await response2.Content.ReadAsAsync<List<Lugar_>>();

                var lugaresNaoReservados = new List<LugarReserva>();

                foreach (var lug in listaLugares)
                {
                    foreach (var lugN in lugaresNaoSubAlugados)
                    {
                        if (lug.LugarID == lugN.Item1)
                        {
                            lugaresNaoReservados.Add(new LugarReserva
                            {
                                Fila = lug.Fila,
                                LugarID = lug.LugarID,
                                Preço = lugN.Item2,
                                Sector = lug.Sector,
                                subReservado = true,
                                parqueId = parqueID,
                                subAluguerId = lugN.Item3
                            });
                        }
                    }
                }

                lugaresNaoReservados.AddRange((await GetLugaresDisponiveis(DataInicio, DataFim, parqueID))
                    .Select(l => new LugarReserva { Fila = l.Fila, LugarID = l.LugarID, Preço = l.Preço, Sector = l.Sector, subReservado = false, parqueId = parqueID}));

                return lugaresNaoReservados;

            }
        }

        public class LugarReserva: Lugar_
        {
            public bool subReservado { get; set; }

            public long parqueId { get; set; }
            public long subAluguerId { get; internal set; }
        }

        
        /// <summary>
        /// ////////////////////
        /// </summary>
        /// <param name="parqueid"></param>
        /// <returns></returns>
        [EnableCors]
        public async Task<ActionResult<Reserva_>> GetUltimaReservaPrivate(long parqueid)
        {
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueid);
            Reserva_ reserva;
            using (HttpClient client = new HttpClient())
            {
                var rtoken = await GetToken(parque.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                var response3 = await client.
                   GetAsync(parque.Url + "reservas/");

                List<Reserva_> ListaLugarUltimo = await response3.
                 Content.ReadAsAsync<List<Reserva_>>();

                reserva = ListaLugarUltimo.
                OrderByDescending(r => r.ReservaID).FirstOrDefault();

            }
            return reserva;
        }


        /// <summary>
        /// /////////////////////////
        /// </summary>
        /// <param name="reservaid"></param>
        /// <param name="parqueid"></param>
        /// <param name="clienteid"></param>
      
        public async void CriarReservaCentral(long reservaid, long parqueid, long clienteid, long lugarId)
        {
            var reserva1 = new Reserva(reservaid, parqueid, clienteid, lugarId);
            _context.Reserva.Add(reserva1);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //alterar return
            return;

        }
        /// <summary>
        /// ////////////////////////////
        /// </summary>
        /// <param name="reserva"></param>
        /// 
        public async void ApagarReservaCentral(Reserva reserva)
        {
            _context.Reserva.Remove(reserva);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            //alterar return
            return;
        }
    }
}