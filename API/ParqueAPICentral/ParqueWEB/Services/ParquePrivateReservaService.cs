using Newtonsoft.Json;
using ParqueAPICentral.Entities;
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
        private string apiBaseUrl = "https://localhost:44365/api/";

        public async Task<ReservaLugar> GetReservaLugarAsync(long reservaID)
        {
            using (var client = new HttpClient())

            {
                // Route para Reservas
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                string endpoint = apiBaseUrl + "Reservas/" + reservaID;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var reserva = await response.Content.ReadAsAsync<ParquePrivadoReserva>();

                // Route para LugarID 
                string endpoint2 = apiBaseUrl + "Lugares/" + reserva.LugarID;
                var response2 = await client.GetAsync(endpoint2);
                response2.EnsureSuccessStatusCode();
                var lugar = await response2.Content.ReadAsAsync<ParquePrivadoLugar>();

                return new ReservaLugar() { DataFim = reserva.DataFim, DataInicio = reserva.DataInicio, PrecoLugar = lugar.Preço };




            }

        }
        public class ReservaLugar
        {

            public DateTime DataInicio { get; set; }
            public DateTime DataFim { get; set; }
            public float PrecoLugar { get; set; }
        }
        public class ParquePrivadoReserva
        {
            public DateTime DataInicio { get; set; }
            public DateTime DataFim { get; set; }
            public long ReservaID { get; set; }
            public long LugarID { get; set; }
        }
        public class ParquePrivadoLugar
        {
            public long LugarID { get; set; }
            public float Preço { get; set; }
        }

    }
}
