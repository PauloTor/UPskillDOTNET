using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.Models;
using ParqueAPICentral.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Controllers
{
  //  [Authorize]
    [Route("api/Clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;
        public ClientesController(ClienteService service)
        {
            this._service = service;
        }
    // ORIGINAL    //C:\Users\paulo\Desktop\proj5\PseudoCompany\API\ParqueAPICentral\ParqueWEB\Models\Fatura.cs
        // GET: api/Clientes : Obter Informação de um Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await this._service.GetAllClientes();
        }

    }

}
