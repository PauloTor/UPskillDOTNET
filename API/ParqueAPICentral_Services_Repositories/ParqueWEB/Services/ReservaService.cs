using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using Microsoft.Extensions.Configuration;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Data;

namespace ParqueAPICentral.Services
{
    public class ReservaService:ControllerBase 
    {
        private readonly IReservaRepository _repo;
        private readonly ParquesService _service;
        private readonly ReservaCentralService _serviceR;
        private readonly ClienteService _serviceC;
        private readonly LugaresService _serviceL;
        private readonly FaturaService _serviceF;
        private readonly SubAluguerService _serviceS;


        public ReservaService(IReservaRepository repo, ParquesService serviceP, ReservaCentralService serviceR, ClienteService serviceC, LugaresService serviceL, FaturaService serviceF, SubAluguerService serviceS)
        {
            this._repo = repo;
            this._service = serviceP;
            this._serviceR = serviceR;
            this._serviceC = serviceC;
            this._serviceL = serviceL;
            this._serviceF = serviceF;
            this._serviceS = serviceS;
         }
       
        public async Task<ActionResult<IEnumerable<ReservaPrivateDTO>>> GetAllReservasByParque(long id)
        {
            var ListaReservas = new List<ReservaPrivateDTO>();
            var parque = await _service.GetParqueById(id);
            using (var client = new HttpClient())

            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(parque.Value.Url + "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
                string endpoint = parque.Value.Url + "Reservas/";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                ListaReservas = await response.Content.ReadAsAsync<List<ReservaPrivateDTO>>();
            }
            return ListaReservas;
        }        //========================================================================>>reservaAPI
        public async Task<ActionResult<Reserva>> CancelarReserva(long parqueID, long reservaAPIID)
        {
            //====>o que retornar da DeleteReservaCentral(repository) vai para reservaCentral
            var reservaCentral = await _serviceR.DeleteReservaCentral(parqueID, reservaAPIID);
            var parque = await _service.GetParqueById(parqueID);

            using (HttpClient client = new HttpClient())
            {
                string endpoint = parque.Value.Url + "reservas/cancelar/" + reservaAPIID;
                var reservaRes = await client.GetAsync(endpoint);
                var reserva_ = await reservaRes.Content.ReadAsAsync<ReservaPrivateDTO>();
                long reservaById = reserva_.ReservaID;
                var ReservaCentral =  _serviceR.GetAllReservasCentralAsync().Result.Value.Where(r => r.ParqueID==parqueID).Where(rr=> rr.ReservaAPI==reservaAPIID).FirstOrDefault();
                var cliente_ = await _serviceC.GetClienteById(ReservaCentral.ClienteID);

                //alterar na factura para encontrar por reserva   
                var fatura =  _serviceF.GetAllFaturas().Result.Value.Where(f => f.ReservaID == ReservaCentral.ReservaID).FirstOrDefault();
              
                if (fatura != null)
                {
                    float precoFatura = fatura.PrecoFatura;

                    //nao acrescenta valor ===>>>>
                    cliente_.Value.Depositar(precoFatura);
                }

                  var deleteTask = client.DeleteAsync(endpoint);
            }
           
            return reservaCentral;
        }
         public async Task<ActionResult<ReservaPrivateDTO>> PostReservaByData(String DataInicio, String DataFim, long ClienteID, long parqueid)
        {


            var parque = await _service.GetParqueById(parqueid);

            using (var client = new HttpClient())
            {
                var rtoken = await GetToken(parque.Value.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
                var i = _serviceL.GetLugaresDisponiveisComSubAlugueres(DataInicio, DataFim, parqueid).Result.Value.FirstOrDefault();

                //var lugarEscolhido = await _serviceL.(parkingLots); //criar lista nos lugares

                //var i = parkingLots.Value.FirstOrDefault(p => p.LugarID == lugarId && p.ParqueId == parqueid);

                //var i = parkingLots.Value.FirstOrDefault(p => p.LugarID == lugarescolhido && p.parqueId == parqueid);

                if ((DateTime.Parse(DataInicio) > DateTime.Parse(DataFim)))
                {
                  return NotFound("Data inválida");
                }

                if (i == null)
                {
                    return NotFound("Lugar não disponivel para ser reservado");
                }

                if (i.subReservado == false)
                {
                    var reserva = new ReservaPrivateDTO(DateTime.Now, DateTime.Parse(DataInicio), DateTime.Parse(DataFim), i.LugarID);
                    StringContent reserva_ = new StringContent(JsonConvert.
                        SerializeObject(reserva), Encoding.UTF8, "application/json");
                    var response2 = await client.
                        PostAsync(parque.Value.Url + "reservas/", reserva_);
                    var UltimaReserva = await GetUltimaReservaPrivate(parqueid);
                    var reservaCentral = new Reserva(parqueid, UltimaReserva.Value.ReservaID, ClienteID, i.LugarID);
                    await _serviceR.CriarReservaCentral(reservaCentral);
                    return CreatedAtAction(nameof(PostReservaByData), new { id = reserva.ReservaID }, reserva);
                }

                else
                {
                    var sub = _serviceS.GetAllSubAluguerAsync().Result.Value.Where(n => n.SubAluguerID == i.subAluguerId).FirstOrDefault();

                    sub.Reservado = true;
                    sub.NovoCliente = ClienteID.ToString();

                    await _serviceS.UpdateSubAluguer(sub);

                 return CreatedAtAction(nameof(PostReservaByData),
                 new { id = sub.SubAluguerID }, sub);

                }
            }
        }


        //GET Lugares disponíveis de ParqueID by Data1 e Data2

        public async Task<ActionResult<ReservaPrivateDTO>> GetUltimaReservaPrivate(long parqueid)
        {
            var parque = await _service.GetParqueById(parqueid);
            ReservaPrivateDTO reserva;
            using (HttpClient client = new HttpClient())
            {
                var rtoken = await GetToken(parque.Value.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                var response3 = await client.
                   GetAsync(parque.Value.Url + "reservas/");

                List<ReservaPrivateDTO> ListaLugarUltimo = await response3.
                 Content.ReadAsAsync<List<ReservaPrivateDTO>>();

                reserva = ListaLugarUltimo.
                OrderByDescending(r => r.ReservaID).FirstOrDefault();

            }
            return reserva;
        }

        public bool ValidaDatas(string DataInicio, string DataFim)
        {

            if (DateTime.Parse(DataInicio) > DateTime.Parse(DataFim))
            {
                return true; ///mensagem datas invalidas

            }


            return false;  // return action voltar a introduzir datas
        }
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


    }
}

