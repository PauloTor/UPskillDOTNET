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
    [Route("api")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _service;
         public ReservasController(ReservaService service)
        {
            this._service = service;
        }

        // GET: api/Reservas por parque
        [EnableCors]
        [HttpGet("ReservasParque/{id}")]
        public async Task<ActionResult<IEnumerable<ReservaPrivateDTO>>> GetReservasPorParque(long id)
        {
           

            return await this._service.GetAllReservasByParque(id);
        }

        [EnableCors]
        [HttpGet("Reservas")]
        public async Task<ActionResult<IEnumerable<ReservaPrivateDTO>>> GetReservasPorParqueporcliente(long id)
        {
           

            return await this._service.GetAllReservasByParque(id);
        }


        //Post Reservas by {DataInicio}/{DataFim}/{ClienteID}/{ParqueID}/{lugarId}
        [EnableCors]
        [HttpGet("post/{DataInicio}/{DataFim}/{ClienteID}/{ParqueID}")]
        public async Task<ActionResult<ReservaPrivateDTO>> PostReservaByData(String DataInicio, String DataFim, long ClienteID, long parqueid)
        {
            
            return await _service.PostReservaByData(DataInicio, DataFim, ClienteID, parqueid);

        }
        

        // DELETE: api/reservas/parqueID/reservaId - Cancelar reserva e devolver credito

        [EnableCors]
        [HttpGet("apagar/{parqueID}/{reservaID}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long parqueID, long reservaID)
        {
            return await _service.CancelarReserva(parqueID, reservaID);
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