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
//using ParqueAPICentral.Entities;
using Microsoft.Extensions.Configuration;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Services;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api")]
    [ApiController]
    public class LugaresController : ControllerBase
    {
        private readonly LugaresService _service;

        public LugaresController(LugaresService service)
        {
            _service = service;
        }
       

        //GET Lugares disponíveis de ParqueID by Data1 e Data2
        [EnableCors]
        [HttpGet("LugaresDisponiveis/{DataInicio}/{DataFim}/{ParqueID}")]
        public async Task<ActionResult<IEnumerable<LugarReserva>>> GetLugaresDisponiveisComSubAlugueres(String DataInicio, String DataFim, long parqueID)
        {
           
            return await _service.GetLugaresDisponiveisComSubAlugueres(DataInicio, DataFim, parqueID);
        }

        //[HttpGet]
        //[Route("centralapi/parque/{id}/lugareslivres/{startDate}/{endDate}")]
        //public async Task<ActionResult<IEnumerable<LugarDTO>>> GetLugaresLivresByDate(DateTime startDate, DateTime endDate, int id)
        //{
        //    //if (await ParkingLotExists(id) == false)
        //    //{
        //    //    return NotFound("Parking Lot was not found");
        //    //}
        //    //if (startDate > endDate)
        //    //{
        //    //    return BadRequest("Dates are not correct");
        //    //}
        //    return await _service.GetLugaresLivresByDate(startDate, endDate, id);
        //}


    }
}