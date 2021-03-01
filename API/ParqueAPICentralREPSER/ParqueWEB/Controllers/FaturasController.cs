using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParqueAPICentral.Data;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using ParqueAPICentral.Services;

namespace ParqueAPICentral.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Faturas")]
    [ApiController]
    public class FaturasController : ControllerBase
    {
        private readonly FaturaService _service;
        public FaturasController(FaturaService service)
        {
            this._service = service;
        }


        // POST Faturas by ReservaID - api/Faturas/Reserva/ReservaID
        [EnableCors]
        [HttpPost("Reserva/{ReservaID}")]
        public async Task<ActionResult<Fatura>> PostFaturaByReservaID(long reservaID)
        {
            return await this._service.CreateFaturaByReservaID(reservaID);
        }

        
        // GET Faturas by FaturaID - api/Faturas/5
        [HttpGet("{FaturaID}")]
        public async Task<ActionResult<Fatura>> GetFatura(long FaturaID)
        {
            //if (Fatura == null)
            //{
            //    return NotFound("Data inválida");
            //}
           return await this._service.FindFaturaByID(FaturaID);

        }

        // GET Faturas by FaturaID - api/Faturas/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fatura>>> GetAllFaturas()
        {
            var d = 1;
         return  await this._service.GetAllFaturas();

        }


    }
}
