using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParquePrivateAPI.Models;
using ParquePrivateAPI.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;


namespace ParquePrivateAPI.Controllers
{
    [Authorize]
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/PrecoLugares")]
    [ApiController]
        public class PrecoLugaresController : ControllerBase
        {     
            private readonly ParquePrivateAPIContext _context;

            public PrecoLugaresController(ParquePrivateAPIContext context)
            {
                _context = context;
            }

    [Authorize]
    [EnableCors("MyAllowSpecificOrigins")]
    [HttpPost]
        public async Task<ActionResult<PrecoLugar>> PostPrecoLugar(PrecoLugar precoLugar)
            {
                _context.PrecoLugar.Add(precoLugar);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
