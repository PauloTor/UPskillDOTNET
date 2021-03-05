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
using ParqueAPICentral.Data;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Cors;
using ParqueAPICentral.Services;
using ParqueAPICentral.Repositories;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/SubAlugueres")]
    [ApiController]
    public class SubAlugueresController : ControllerBase
    {

        private readonly SubAluguerService _service;
        public SubAlugueresController(SubAluguerService service)
        {
            _service = service;
        }

        // GET: api/SubAlugueres
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubAluguer>>> GetAllSubAluguerAsync()
        {
            return await _service.GetAllSubAluguerAsync();
        }

        // GET: api/SubAlugueres/id
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubAluguer>> GetSubAlugueresById(long id)
        {
            return await _service.FindSubAluguerById(id);
        }

        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<SubAluguer>> UpdateSubAluguer(SubAluguer subaluguer)
        {
            return await _service.UpdateSubAluguer(subaluguer);
        }
        
        // POST: api/SubAlugueres/
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<SubAluguer>> PostSubAluguer(SubAluguer subaluguer)
        {
        return await _service.PostSubAluguer(subaluguer);
        }      
    }
}
