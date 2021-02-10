﻿using System;
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
    public class ReservaService
    {
        private readonly APICentralContext _context;
        private readonly IReservaRepository _repo;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;


        public ReservaService(APICentralContext context, IReservaRepository repo, IConfiguration configuration)
        {
            this._context = context;
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

            return Eliminado();
        }
        private ActionResult<Reserva> NotFound()
        {
            throw new NotImplementedException("A reserva que procura não existe.");
        }

        private ActionResult<Reserva> Eliminado()
        {
            throw new NotImplementedException("Reserva eliminada.");
        }

        
    }
}
