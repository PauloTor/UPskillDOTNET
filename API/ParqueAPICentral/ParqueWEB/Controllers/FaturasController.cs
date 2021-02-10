using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParqueAPICentral.Data;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using ParqueAPICentral.Services;

namespace ParqueAPICentral.Controllers
{
    [Route("api/Faturas")]
    [ApiController]
    public class FaturasController : ControllerBase
    {
        private readonly FaturaService _service;
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;
        private readonly string apiBaseUrl2;


        public FaturasController(FaturaService service, IConfiguration configuration)
        {
            this._service = service;
            _configure = configuration;
            apiBaseUrl2 = _configure.GetValue<string>("WebAPICentralBaseUrl");
            apiBaseUrl = _configure.GetValue<string>("WebAPIPrivateBaseUrl");
        }

        // POST Faturas by ReservaID - api/Faturas/ReservaID
        [EnableCors]
        [HttpGet("Reserva/{ReservaID}")]
        public async Task<ActionResult<Fatura>> PostFaturaByReservaID(long reservaID)
        {
            return await this._service.CreateFaturaByReservaID(reservaID);
        }

        // GET Faturas by FaturaID - api/Faturas/5
        [HttpGet("{FaturaID}")]
        public IActionResult GetFatura(long FaturaID)
        {
            var Fatura = this._service.FindFaturaByID(FaturaID);

            if (Fatura == null)
            {
                return NotFound();
            }

            return Ok(Fatura);

        }

    }
}

