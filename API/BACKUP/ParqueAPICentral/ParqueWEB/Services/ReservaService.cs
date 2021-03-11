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
/*
namespace ParqueAPICentral.Services
{
    public class ReservaService
    {
        //private readonly APICentralContext _context;
        private readonly IReservaRepository _ReservaRepo;
        private readonly IClienteRepository _ClienteRepo;
        //private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;


        public ReservaService(IReservaRepository reserva, IClienteRepository cliente)
        {
            // this._context = context;
            this._ReservaRepo = reserva;
            this._ClienteRepo = cliente;
            //_configure = configuration;

            //apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
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

        public async Task<ActionResult<IEnumerable<Reserva>>> PostReservaByData(String DataInicio, String DataFim, long ClienteID)
        {
            var dateTimeInicio = DateTime.Parse(DataInicio);
            var dateTimeFim = DateTime.Parse(DataFim);
            Reserva_ reserva;

            using (var client = new HttpClient())
            {
                var cliente = await _ClienteRepo.FindClienteById(ClienteID);
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
                var parqueID = _context.Parque.FirstOrDefault().ParqueID;
                var reserva1 = new Reserva(ClienteID, parqueID);
                _context.Reserva.Add(reserva1);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private ActionResult<IEnumerable<Reserva>> NoContent()
        {
            throw new NotImplementedException();
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
*/