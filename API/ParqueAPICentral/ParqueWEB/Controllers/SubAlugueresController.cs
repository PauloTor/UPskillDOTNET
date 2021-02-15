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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParqueAPICentral.Data;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Cors;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/SubAlugueres")]
    [ApiController]
    public class SubAlugueresController : ControllerBase
    {
        private readonly APICentralContext _context;

        public SubAlugueresController(APICentralContext context)
        {
            _context = context;
        }
      
        // POST: api/SubAlugueres/{reservaID}/{preco}/
        [EnableCors]
        [HttpPost("{reservaID}/{preco}")]
        public async Task<ActionResult<Cliente>> PostSubAluguer(long ResevaID, float Preco)
        {
            SubAluguer subAluguer = new SubAluguer(ResevaID, Preco);
            _context.SubAluguer.Add(subAluguer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubAluguer", new { id = subAluguer.SubAluguerID }, subAluguer);
        }      
    }
}
