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
    [Route("api/ReservasCentral")]
    [ApiController]
    public class ReservaCentralController : ControllerBase
    {
        private readonly ReservaCentralService _service;
        public ReservaCentralController(ReservaCentralService service)
        {
            this._service = service;
        }

        // GET: api/ReservaCentral
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetAllReservaCentral()
        {
            return await this._service.GetAllReservasCentralAsync();
        }

        [EnableCors]
        [HttpPut]
        public async Task<ActionResult<Reserva>> UpdateReserva(Reserva reserva)
        {
            return await _service.UpdateReserva(reserva);
        }

        [EnableCors]
        [HttpGet("ClienteReserva/{id}")]
        public async Task<ActionResult<Reserva>> GetAllClienteByReservasCentralAsync(long ParqueID, long id)
        {
        return await this._service.GetAllClienteByReservasCentral(ParqueID, id);
        }


        // GET: api/ReservaCentral/id
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReservaById(long id)
        {                     
        return await _service.GetReservaById(id);
        }


        // GET: api/ReservaCentral/id/true
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<Reserva>> PutParaSubALuguer(long id)
        {
            return await _service.ParaSubALuguer(id);
        }
    }
}