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
using ParqueAPICentral.Services;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _service;
         public ReservasController(ReservaService service)
        {
            _service = service;
        }

        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _service.GetReservas();
        }


        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReservaById(long id)
        {
            return await this._service.GetReservaById(id);
        }


        // GET: api/Reservas por parque
        [EnableCors]
        [HttpGet("Parque/{id}")]
        public async Task<ActionResult<IEnumerable<ReservaPrivateDTO>>> GetReservasPorParque(long id)
        {
            return await this._service.GetAllReservasByParque(id);
        }

        [EnableCors]
        [HttpGet("Clientes")]
        public async Task<ActionResult<IEnumerable<ReservaPrivateDTO>>> GetReservasPorParqueporcliente(long id)
        {           
            return await this._service.GetAllReservasByParque(id);
        }


        //POST
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<ReservaPrivateDTO>> PostReservaByData(ReservaPrivateDTO dto)
        {           
            return await _service.PostReservaByData(dto);
        }
        

        // DELETE: api/reservaID - Cancelar reserva e devolver credito
        [EnableCors]
        [HttpDelete("{reservaID}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long reservaID)
        {
            return await _service.CancelarReserva(reservaID);
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


    }
}