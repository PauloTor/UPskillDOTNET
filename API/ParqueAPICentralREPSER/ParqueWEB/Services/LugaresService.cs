using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
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
    public class LugaresService
    {
        private readonly ParquesService _serviceP;
        private readonly ReservaCentralService _serviceR;
        private readonly SubAluguerService _serviceS;
        public LugaresService(ParquesService serviceP, ReservaCentralService serviceR, SubAluguerService serviceS)
        {
            this._serviceP = serviceP;
            this._serviceR = serviceR;
            this._serviceS = serviceS;
        }

        private async Task<IEnumerable<LugarDTO>> GetLugaresDisponiveis(String DataInicio, String DataFim, long parqueID)
        {
            var parque = await _serviceP.GetParqueById(parqueID);
            var ListaLugar = new List<LugarDTO>();
            using (var client = new HttpClient())
            {
                var rtoken = await GetToken(parque.Value.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
                var response = await client.GetAsync(parque.Value.Url + "Lugares/" + DataInicio + "/" + DataFim);
                response.EnsureSuccessStatusCode();
                ListaLugar = await response.Content.ReadAsAsync<List<LugarDTO>>();
            }
            return ListaLugar;
        }

        public async Task<ActionResult<IEnumerable<LugarReserva>>> GetLugaresDisponiveisComSubAlugueres(String DataInicio, String DataFim, long parqueID)
        {
            var parque = await _serviceP.GetParqueById(parqueID);

            using var client = new HttpClient();
            var rtoken = await GetToken(parque.Value.Url + "users/authenticate");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
            var response = await client.GetAsync(parque.Value.Url + "Reservas/" + DataInicio + "/" + DataFim);
            response.EnsureSuccessStatusCode();
            var listaReservas = await response.Content.ReadAsAsync<List<ReservaPrivateDTO>>();
            var reservas = await _serviceR.GetAllReservasCentralAsync();
            var subAluguer = await _serviceS.GetAllSubAluguerAsync(); //alterar para: por parqueID
            var lugaresNaoSubAlugados = new List<(long, float, long)>();

            foreach (var reserva in reservas.Value)
            {
                foreach (var reservaOriginal in listaReservas)
                {
                    if (reservaOriginal.ReservaID == reserva.ReservaAPI && reserva.ParqueID == parqueID && reserva.ParaSubAluguer)
                    {
                        var sub = subAluguer.Value.FirstOrDefault(s => s.ReservaID == reserva.ReservaID);
                        if (sub.Reservado == false)
                        {
                            lugaresNaoSubAlugados.Add((reserva.LugarID, sub.Preco, sub.SubAluguerID));
                            break;
                        }
                    }
                }
            }
            var response2 = await client.GetAsync(parque.Value.Url + "Lugares/");
            response2.EnsureSuccessStatusCode();
            var listaLugares = await response2.Content.ReadAsAsync<List<LugarDTO>>();
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
                .Select(l => new LugarReserva { Fila = l.Fila, LugarID = l.LugarID, Preço = l.Preço, Sector = l.Sector, subReservado = false, parqueId = parqueID }));

            return lugaresNaoReservados;
        }
        public async Task<string> GetToken(string apiBaseUrlPrivado)
        {
            using HttpClient client = new HttpClient();
            UserInfo user = new UserInfo();
            StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var responseLogin = await client.PostAsync(apiBaseUrlPrivado, contentUser);
            dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
            string rtoken = tokenresponsecontent.jwtToken;
            return rtoken;
        }
    }
}

