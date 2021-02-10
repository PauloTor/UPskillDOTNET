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


namespace ParqueAPICentral.Services
{
    public class ReservaService
    {
        private readonly IReservaRepository _repo;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;


        public ReservaService(IReservaRepository repo, IConfiguration configuration)
        {
            this._repo = repo;
            _configure = configuration;

            apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        }

        public async Task<ActionResult<IEnumerable<Reserva_>>> GetAllReservas()
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
    }
}
