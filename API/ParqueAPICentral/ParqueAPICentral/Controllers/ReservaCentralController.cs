using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Models;
using ParqueAPICentral.Contexts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
//using ParqueAPICentral.Entities;
using Microsoft.Extensions.Configuration;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Services;

namespace ParqueAPICentral.Controllers
{
    //[Authorize(Policy = "Roles")]
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


        // GET: api/Reservas por parque
        //[Authorize(Policy = "Admin")]
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetAllReservaCentral()
        {
            return await this._service.GetAllReservasCentralAsync();
        }

        //[Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpPut("{ParqueID}")]
        public async Task<ActionResult<Reserva>> UpdateReserva(Reserva reserva)
        {
            return await _service.UpdateReserva(reserva);
        }

        //[Authorize(Policy = "Admin")]
        [EnableCors]
        [HttpGet("ClienteReserva/{id}")]
        public async Task<ActionResult<Reserva>> GetAllClienteByReservasCentralAsync(long ParqueID, long id)
        {
        return await this._service.GetAllClienteByReservasCentral(ParqueID, id);
        }

        //[Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpDelete("{ParqueID}/{id}")]
        public async Task<ActionResult<Reserva>> DeleteReservaCentral(long id)
        {
            return await _service.DeleteReservaCentral(id);
        }

        // GET: api/Reservas/id
        //[Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReservaById(long id)
        {   
            return await _service.GetReservaById(id);
        }

        [EnableCors]
        [HttpPut("sub/{id}")]
        public async Task<ActionResult<Reserva>> PutParaSubALuguer(long id)
        {
            return await _service.ParaSubALuguer(id);
        }
    }
}