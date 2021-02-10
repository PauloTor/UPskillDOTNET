using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParquePrivateAPI.Data;
using ParquePrivateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParquePrivateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoLugaresController : ControllerBase
    {
      
            private readonly ParquePrivateAPIContext _context;

            public PrecoLugaresController(ParquePrivateAPIContext context)
            {
                _context = context;
            }


            [Authorize]
            [EnableCors]
            [HttpPost]
            public async Task<ActionResult<PrecoLugar>> PostPrecoLugar(PrecoLugar precoLugar)
            {
                _context.PrecoLugar.Add(precoLugar);
                await _context.SaveChangesAsync();

                return NoContent();
            }




        }
    }
