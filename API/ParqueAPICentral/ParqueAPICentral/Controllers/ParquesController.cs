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
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Parques")]
    [ApiController]
    public class ParquesController : ControllerBase
    {
        private readonly ParquesService _service;

        public ParquesController(ParquesService service)
        {
            this._service = service;
        }

        // GET: api/Parques
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parque>>> GetParquesTodos()
        {

            return await _service.GetAllParques();
        }
       
        //// GET: api/Parques/5  - Obter Informação de um Parque por ID
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Parque>> GetParqueById(long id)
        {
            if (await ParqueExist(id) == false)
            {
                return NotFound("Parque nao existe");
            }
            return await _service.GetParqueById(id);
        }

        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Parque>> PostParque(Parque parque)
        {
            return await _service.PostParque(parque);
        }

        public async Task<bool> ParqueExist(long id)
        {
        return await _service.ParqueExist(id);
        }
    }
}