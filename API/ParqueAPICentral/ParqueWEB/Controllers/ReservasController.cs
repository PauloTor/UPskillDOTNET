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
using QRCoder;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace ParqueAPICentral.Controllers
{
    //[Authorize(Policy = "Roles")]
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
        //[Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservasTodas()
        {
            return await _context.Reserva.Include(r => r.Cliente).Include(r => r.Parque).ToListAsync();
        }



        // GET: api/Reservas/id
        //[Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReservaById(long id)
        {
            var reserva = await _context.Reserva.Where(r => r.ReservaID == id).Include(r => r.Cliente).Include(r => r.Parque).FirstOrDefaultAsync();

            if (reserva == null)
            {
                return NotFound();
            }
            return reserva;
        }



        //GET: api/reservas/parque/parqueID - Todas as Reservas de um Parque
        //[Authorize(Policy = "Admin")]
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



        //GET: api/reservas/parque/parqueID/id - Reservas de um Parque por ReservaID
        //[Authorize(Policy = "Admin")]
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



        //POST: Reservas by {DataInicio}/{DataFim}/{ClienteID}/{ParqueID}/{lugarId}
        [Authorize(Policy = "User")]
        [EnableCors]
        [HttpGet("{DataInicio}/{DataFim}/{ClienteID}/{ParqueID}/{lugarId}")]
        public async Task<ActionResult<Reserva_>> PostReservaByData(String DataInicio, String DataFim, long ClienteID, long parqueid, long lugarId)
        {
            if (DateTime.Parse(DataInicio) > DateTime.Parse(DataFim))
            {
                return NotFound();
            }
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueid);

            using var client = new HttpClient();
            var rtoken = await GetToken(parque.Url + "users/authenticate");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

            var parkingLots = await (GetLugaresDisponiveisComSubAlugueres(DataInicio, DataFim, parqueid));
            var i = parkingLots.Value.FirstOrDefault(p => p.LugarID == lugarId && p.ParqueId == parqueid);

            var UltimaReserva = await GetUltimaReservaPrivate(parqueid);

            var reserva = new Reserva_(DateTime.Now, DateTime.Parse(DataInicio), DateTime.Parse(DataFim), i.LugarID);

            if ((DateTime.Parse(DataInicio) > DateTime.Parse(DataFim)) || (!parkingLots.Value.Any()))
            {
                return NotFound("Data inválida");
            }

            if (i == null)
            {
                return NotFound("Lugar não disponivel para ser reservado");
            }

            if (i.SubReservado == false)
            {

                StringContent reserva_ = new StringContent(JsonConvert.
                    SerializeObject(reserva), Encoding.UTF8, "application/json");

                var response2 = await client.
                    PostAsync(parque.Url + "reservas/", reserva_);

                try
                {
                    CriarReservaCentral(parque.ParqueID, UltimaReserva.Value.ReservaID, ClienteID, lugarId);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Couldn't retrieve entities: {ex.Message}");
                }

                var qrCode = GerarQRcode(UltimaReserva.Value);

                await EnviarEmail(qrCode.Value, ClienteID, UltimaReserva.Value.ReservaID);

                return CreatedAtAction(nameof(PostReservaByData),

                new { id = reserva.ReservaID }, reserva);
            }

            else
            {
                var sub = _context.SubAluguer.FirstOrDefault(n => n.SubAluguerID == i.SubAluguerId);

                var subId = sub.ReservaID;

                var reservaC = _context.Reserva.Where(r => r.ReservaID == subId).FirstOrDefault();

                var reservaOriginalCliente = reservaC.ClienteID;

                var clienteOriginal = _context.Cliente.Where(r => r.ClienteID == reservaOriginalCliente).FirstOrDefault();

                float preco = sub.Preco;

                var clienteNovo = _context.Cliente.Where(c => c.ClienteID == ClienteID).FirstOrDefault();

                clienteNovo.Pagar(preco);

                clienteOriginal.Depositar(preco);

                sub.Reservado = true;

                sub.NovoCliente = ClienteID;

                reservaC.ParaSubAluguer = false;

                _context.SubAluguer.Update(sub);

                _context.SaveChanges();

                var qrCode = GerarQRcode(reserva);

                await EnviarEmail(qrCode.Value, ClienteID, reservaC.ReservaID);

                return CreatedAtAction(nameof(PostReservaByData),

                new { id = sub.SubAluguerID }, sub);
            }            
        }



        // DELETE: api/reservas/parqueID/reservaIdAPI - Cancelar reserva e devolver credito
        [Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpGet("{parqueID}/{reservaID}")]
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



        [EnableCors]
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


       
        private async Task<IEnumerable<Lugar_>> GetLugaresDisponiveis(String DataInicio, String DataFim, long parqueID)
        {
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);
            using var client = new HttpClient();
            var rtoken = await GetToken(parque.Url + "users/authenticate");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rtoken);

            var response = await client.GetAsync(parque.Url + "Lugares/" + DataInicio + "/" + DataFim);
            response.EnsureSuccessStatusCode();
            var ListaLugar = await response.Content.ReadAsAsync<List<Lugar_>>();

            return ListaLugar;
        }



        //GET Lugares disponíveis de ParqueID by Data1 e Data2
        [Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpGet("LugaresDisponiveis/{DataInicio}/{DataFim}/{ParqueID}")]
        public async Task<ActionResult<IEnumerable<LugarReserva>>> GetLugaresDisponiveisComSubAlugueres(String DataInicio, String DataFim, long parqueID)
        {
            var parque = await _context.Parque.FirstOrDefaultAsync(p => p.ParqueID == parqueID);
            using var client = new HttpClient();
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
                        // nao esta a encontrar apenas 1 objecto
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
                            SubReservado = true,
                            ParqueId = parqueID,
                            SubAluguerId = lugN.Item3
                        });
                    }
                }
            }

            lugaresNaoReservados.AddRange((await GetLugaresDisponiveis(DataInicio, DataFim, parqueID))
                .Select(l => new LugarReserva { Fila = l.Fila, LugarID = l.LugarID, Preço = l.Preço, Sector = l.Sector, SubReservado = false, ParqueId = parqueID }));

            return lugaresNaoReservados;
        }



        public class LugarReserva: Lugar_
        {
            public bool SubReservado { get; set; }

            public long ParqueId { get; set; }
            public long SubAluguerId { get; internal set; }
        }



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



        public ActionResult<byte[]> GerarQRcode(Reserva_ reserva)
        {
            var qrInfo = "Reserva: " + reserva.ReservaID
                   + "\nLugar: " + reserva.LugarID
                   + "\nData de Inicio: " + reserva.DataInicio
                   + "\nData de Fim: " + reserva.DataFim;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();

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



        public async Task EnviarEmail(byte[] qrCode, long ClienteID, long ReservaID)
        {
            var cliente = await _context.Cliente.Where(c => c.ClienteID == ClienteID).FirstOrDefaultAsync();

            string remetente = "pseudocompany2020@gmail.com";

            string destinatario = cliente.EmailCliente;

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
    }
}