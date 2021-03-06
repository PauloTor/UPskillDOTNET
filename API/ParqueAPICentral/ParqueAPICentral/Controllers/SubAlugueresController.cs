using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Contexts;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Cors;
using ParqueAPICentral.Services;
using ParqueAPICentral.Repositories;

namespace ParqueAPICentral.Controllers
{
    //[Authorize(Policy = "Roles")]
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/SubAlugueres")]
    [ApiController]
    public class SubAlugueresController : ControllerBase
    {
        private readonly SubAluguerService _service;
        private readonly ReservaService _serviceR;
        private readonly ReservaCentralService _serviceRC;

        public SubAlugueresController(SubAluguerService service, ReservaService serviceR, ReservaCentralService serviceRC)
        {
            _service = service;
            _serviceR = serviceR;
            _serviceRC = serviceRC;
        }

        // GET: api/SubAlugueres
        //[Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubAluguer>>> GetAllSubAluguerAsync()
        {
            return await _service.GetAllSubAluguerAsync();
        }

        // GET: api/SubAlugueres/id
        //[Authorize(Policy = "Roles")]
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubAluguer>> GetSubAlugueresById(long id)
        {
            return await _service.FindSubAluguerById(id);
        }

        //[Authorize(Policy = "User")]
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<SubAluguer>> UpdateSubAluguer(SubAluguer subaluguer)
        {
            if (subaluguer.Reservado == false)
                return await _service.UpdateSubAluguer(subaluguer);
            else
                throw new Exception("O subaluguer já se encontra reservado e não pode ser modificado.");
        }

        // POST: api/SubAlugueres/
        //[Authorize(Policy = "Roles")]
        [EnableCors]
        public async Task<ActionResult<SubAluguer>> PostSubAluguer(SubAluguer subaluguer)
        {
            return await _service.PostSubAluguer(subaluguer);
        }

        [EnableCors]
        [HttpPost("post")]
        public async Task<ActionResult<Reserva>> PostSubReserva(SubAluguer subaluguer)
        {
            var id = subaluguer.ReservaID;
            var reserva = _serviceRC.GetReservaById(id).Result.Value;
            reserva.UserID = subaluguer.NovoCliente;
            return await _serviceR.PostSubReserva(reserva);
            //return await _serviceRC.DeleteReservaCentral(id);
        }
    }
}
