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

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Reservas")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly APICentralContext _context;
        private readonly ReservaService _service;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;


        public ReservasController(APICentralContext context, IConfiguration configuration, ReservaService service)
        {
            this._context = context;
            this._service = service;
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        }
       
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva_>>> GetReservas()
        {
            return await this._service.GetAllReservas();
        }

        [EnableCors]
        [HttpGet("{DataInicio}/{DataFim}/{Cliente}")]
        public async Task<ActionResult<IEnumerable<Reserva>>> PostReservaByData(String DataInicio, String DataFim, long ClienteID)
        {
            return await this._service.PostReservaByData(DataInicio, DataFim, ClienteID);
        }

        // DELETE: api/reservas/id - Cancelar reserva
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> CancelarReserva(long id)
        {
            return await this._service.CancelarReserva(id);
        }
    }
}

