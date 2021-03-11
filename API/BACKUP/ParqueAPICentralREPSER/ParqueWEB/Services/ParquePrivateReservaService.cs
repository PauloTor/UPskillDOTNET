using Newtonsoft.Json;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ParqueAPICentral.Services
{
    public class ParquePrivateReservaService

    {
        private readonly ParquesService _service;

        public ParquePrivateReservaService(ParquesService service)
        {
            this._service = service;
        }

        public ParquePrivateReservaService()
        { 
         
        }


        public async Task<ReservaLugarDTO> GetReservaLugarAsync(long ParqueID, long reservaID)
        {
            var parque = await _service.GetParqueById(ParqueID);
            using (var client = new HttpClient())
            //    var parque = await _service.GetParqueById(id);

            {
                // Route para Reservas
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(parque.Value.Url+ "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
                string endpoint = parque.Value.Url + "Reservas/" + reservaID;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                // reservaAPI
                var reserva = await response.Content.ReadAsAsync<ParquePrivadoReservaDTO>();
                string endpoint2 = parque.Value.Url + "Lugares/" + reserva.LugarID;
                var response2 = await client.GetAsync(endpoint2);
                response2.EnsureSuccessStatusCode();
                var lugar = await response2.Content.ReadAsAsync<ParquePrivadoLugarDTO>();

                return new ReservaLugarDTO() { DataFim = reserva.DataFim, DataInicio = reserva.DataInicio, PrecoLugar = lugar.Preço };
            }
        }
    }
}
