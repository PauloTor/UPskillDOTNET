using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using Microsoft.Extensions.Configuration;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Contexts;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Drawing;
using QRCoder;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using PaqueAPICentral.Models;

namespace ParqueAPICentral.Services
{
    public class ReservaService : ControllerBase
    {
        private readonly IReservaRepository _repo;
        private readonly ParquesService _service;
        private readonly ReservaCentralService _serviceR;
        private readonly UserService _serviceC;
        private readonly LugaresService _serviceL;
        private readonly FaturaService _serviceF;
        private readonly SubAluguerService _serviceS;

        public ReservaService(IReservaRepository repo, ParquesService serviceP, ReservaCentralService serviceR, UserService serviceC, LugaresService serviceL, FaturaService serviceF, SubAluguerService serviceS)
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
            if (await _service.ParqueExist(id) == false)
            {
                return NotFound("Parque nao existe");
            }
            using var client = new HttpClient();
            UserInfo user = new UserInfo();
            StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            try
            {
                var responseLogin = await client.PostAsync(parque.Value.Url + "users/authenticate", contentUser);
                dynamic tokenresponsecontent = await responseLogin.Content.ReadAsAsync<object>();
                string rtoken = tokenresponsecontent.jwtToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
                string endpoint = parque.Value.Url + "Reservas/";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                ListaReservas = await response.Content.ReadAsAsync<List<ReservaPrivateDTO>>();
                return ListaReservas;
            }
            catch (HttpRequestException)
            {
                return NotFound("API do Parque " + parque.Value.NomeParque + " nao conectada");
            }

        }        //========================================================================>>reservaAPI
        public async Task<ActionResult<Reserva>> CancelarReserva(long reservaID)
        {
            var reserv = _serviceR.GetReservaById(reservaID);
            var reservaAPIID = reserv.Result.Value.ReservaAPI;
            var parqueID = reserv.Result.Value.ParqueID;
            var parque = await _service.GetParqueById(parqueID);

            using HttpClient client = new HttpClient();
            string endpoint = parque.Value.Url + "reservas/cancelar/" + reservaAPIID;

            try
            {
                var ReservaCentral = _serviceR.GetAllReservasCentralAsync().Result.Value.
                    Where(r => r.ParqueID == parqueID).Where(rr => rr.ReservaAPI == reservaAPIID).FirstOrDefault();

                var cliente_ = _serviceC.GetById(ReservaCentral.UserID);

                var fatura = _serviceF.GetAllFaturas().Result.Value.Where(f => f.ReservaID == ReservaCentral.ReservaID).FirstOrDefault();

                if (fatura != null)

                    if (fatura != null)
                    {
                        float precoFatura = fatura.PrecoFatura;

                        _serviceC.UpdatePagamentoCliente(cliente_.Id, precoFatura);
                    }

                var deleteTask = client.DeleteAsync(endpoint);

                var reservaCentral = await _serviceR.DeleteReservaCentral(reservaID);

                return NoContent();

            }
            catch (HttpRequestException)
            {
                return NotFound("API do Parque " + parque.Value.NomeParque + " nao conectada");
            }
        }


        public async Task<ActionResult<ReservaPrivateDTO>> PostReservaByData(ReservaPrivateDTO reserva_)
        {

            var ClienteID = _serviceC.GetIdByEmail(reserva_.UserID);

            var parque = await _service.GetParqueById(reserva_.ParqueID);
            //var DataInicio = reserva_.DataInicio.ToString();
            var DataInicio = reserva_.DataInicio.ToString("yyyy-MM-ddTHH:mm:ss");
            var DataFim = reserva_.DataFim.ToString("yyyy-MM-ddTHH:mm:ss");

            var parqueid = reserva_.ParqueID;


            using var client = new HttpClient();
            try
            {
                var rtoken = await GetToken(parque.Value.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);
                var i = _serviceL.GetLugaresDisponiveisComSubAlugueres(DataInicio, DataFim, parqueid).Result.Value.FirstOrDefault();

                
                var reserva = new ReservaPrivateDTO(DateTime.Now, DateTime.Parse(DataInicio), DateTime.Parse(DataFim), ClienteID, i.LugarID);



                //var lugarEscolhido = await _serviceL.(parkingLots); //criar lista nos lugares

                //var i = parkingLots.Value.FirstOrDefault(p => p.LugarID == lugarId && p.ParqueId == parqueid);

                //var i = parkingLots.Value.FirstOrDefault(p => p.LugarID == lugarescolhido && p.parqueId == parqueid);

                if ((DateTime.Parse(DataInicio) > DateTime.Parse(DataFim)) || DateTime.Parse(DataInicio) < DateTime.Now)
                {
                    return NotFound("Data inválida");
                }

                if (i == null)
                {
                    return NotFound("Lugar não disponivel para ser reservado");
                }

                if (i.subReservado == false)
                {
                    StringContent reserva__ = new StringContent(JsonConvert.
                        SerializeObject(reserva), Encoding.UTF8, "application/json");
                    var response2 = await client.
                        PostAsync(parque.Value.Url + "reservas/", reserva__);
                    var UltimaReservaAPI = await GetUltimaReservaPrivate(parqueid);
                    var reservaCentral = new Reserva(parqueid, UltimaReservaAPI.Value.ReservaID, reserva_.UserID, reserva_.LugarID, reserva_.DataInicio, reserva_.DataFim, reserva_.DataReserva);
                    await _serviceR.CriarReservaCentral(reservaCentral);
                    var qrCode = GerarQRcode(UltimaReservaAPI.Value);
                    await EnviarEmail(qrCode.Value, ClienteID, UltimaReservaAPI.Value.ReservaID);

                    return CreatedAtAction(nameof(PostReservaByData), new { id = reserva.ReservaID }, reserva);
                }

                else
                {
                    var sub = _serviceS.GetAllSubAluguerAsync().Result.Value.Where(n => n.SubAluguerID == i.subAluguerId).FirstOrDefault();
                    var reservaC = _serviceR.GetAllReservasCentralAsync().Result.Value.Where(r => r.ReservaID == sub.ReservaID).FirstOrDefault();
                    sub.Reservado = true;
                    sub.NovoCliente = ClienteID;
                    await _serviceS.UpdateSubAluguer(sub);

                    //pagamento do cliente que aluga
                    _serviceC.UpdatePagamentoCliente(sub.NovoCliente.ToString(), sub.Preco * -1);
                    //depositar cliente da reservacentral
                    _serviceC.UpdatePagamentoCliente(reservaC.UserID, sub.Preco);

                    reservaC.ParaSubAluguer = false;
                    await _serviceR.UpdateReserva(reservaC);

                    var qrCode = GerarQRcode(reserva);
                    await EnviarEmail(qrCode.Value, ClienteID, reservaC.ReservaID);

                    return CreatedAtAction(nameof(PostReservaByData),
                 new { id = sub.SubAluguerID }, sub);

                }
            }
            catch (HttpRequestException)
            {
                return NotFound("API do Parque " + parque.Value.NomeParque + " nao conectada");
            }
        }

        //GET Lugares disponíveis de ParqueID by Data1 e Data2

        public async Task<ActionResult<ReservaPrivateDTO>> GetUltimaReservaPrivate(long parqueid)
        {
            var parque = await _service.GetParqueById(parqueid);
            ReservaPrivateDTO reserva;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var rtoken = await GetToken(parque.Value.Url + "users/authenticate");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                    var response3 = await client.
                       GetAsync(parque.Value.Url + "reservas/");

                    List<ReservaPrivateDTO> ListaLugarUltimo = await response3.
                     Content.ReadAsAsync<List<ReservaPrivateDTO>>();

                    reserva = ListaLugarUltimo.
                    OrderByDescending(r => r.ReservaID).FirstOrDefault();


                    return reserva;
                }
                catch (HttpRequestException)
                {
                    return NotFound("API do Parque " + parque.Value.NomeParque + " nao conectada");
                }
            }
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

        public ActionResult<byte[]> GerarQRcode(ReservaPrivateDTO reserva)
        {
            var qrInfo = "Reserva: " + reserva.ReservaID
                   + "\nLugar: " + reserva.LugarID
                   + "\nData de Inicio: " + reserva.DataInicio
                   + "\nData de Fim: " + reserva.DataFim;

            QRCodeGenerator qrGenerator = new();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrInfo, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return BitmapToBytes(qrCodeImage);
        }


        private static byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new MemoryStream();

            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            return stream.ToArray();
        }

        public async Task EnviarEmail(byte[] qrCode, string ClienteID, long ReservaID)
        {
            var cliente = _serviceC.GetById(ClienteID);

            string remetente = "pseudocompany2020@gmail.com";

            string destinatario = cliente.Email;

            var qrcode = new Attachment(new MemoryStream(qrCode), "QRCode", "image/png");

            using MailMessage mail = new MailMessage(remetente, destinatario)
            {
                Subject = "Confirmação da reserva nº " + ReservaID,
                Body = "A sua reserva está confirmada.\n\nO código QR relativo à sua reserva encontra-se em anexo."
            };

            mail.Attachments.Add(qrcode);
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            NetworkCredential networkCredential = new NetworkCredential(remetente, "PseudoPark255");
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mail);
        }

        public async Task<ActionResult<ReservaPrivateDTO>> PostReserva(ReservaPrivateDTO dto)
        {
            var parque = await _service.GetParqueById(2);
            using var client = new HttpClient();
            try
            {
                var rtoken = await GetToken(parque.Value.Url + "users/authenticate");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

                if (dto.DataInicio >= dto.DataFim || dto.DataInicio < DateTime.Now)
                {
                    throw new Exception("Data inválida");
                }
                StringContent reserva_ = new(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
                var response2 = await client.PostAsync(parque.Value.Url + "reservas/", reserva_);
                var UltimaReservaAPI = await GetUltimaReservaPrivate(dto.ParqueID);
                var reservaCentral = new Reserva(dto.ParqueID, dto.ReservaID, dto.UserID, dto.LugarID, dto.DataInicio, dto.DataFim, dto.DataReserva);
                await _serviceR.CriarReservaCentral(reservaCentral);
                var qrCode = GerarQRcode(UltimaReservaAPI.Value);
                await EnviarEmail(qrCode.Value, dto.UserID, UltimaReservaAPI.Value.ReservaID);

                return CreatedAtAction(nameof(PostReserva), new { id = dto.ReservaID }, dto);
            }
            catch (HttpRequestException)
            {
                return NotFound("API do Parque " + parque.Value.NomeParque + " não conectada.");
            }
        }

        public async Task<ActionResult<Reserva>> PostSubReserva(Reserva reserva)
        {

            var reservaCentral = new Reserva(reserva.ParqueID, reserva.ReservaAPI, reserva.UserID, reserva.LugarID, reserva.DataInicio, reserva.DataFim, reserva.DataReserva);
            await _serviceR.CriarReservaCentral(reservaCentral);
            var UltimaReservaAPI = await GetUltimaReservaPrivate(reserva.ParqueID);
            var qrCode = GerarQRcode(UltimaReservaAPI.Value);
            EnviarEmail(qrCode.Value, reserva.UserID, UltimaReservaAPI.Value.ReservaID);
            _serviceR.DeleteReservaCentral(reserva.ReservaID);

            return CreatedAtAction(nameof(PostReserva), new { id = reserva.ReservaID }, reserva);
        }
    }
}

