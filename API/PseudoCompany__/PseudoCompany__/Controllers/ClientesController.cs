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
using Microsoft.AspNetCore.Cors;
//using ParqueApiCentral.Services.IServices;

namespace ParqueAPICentral.Controllers
{
    // [Authorize(Policy = "Admin")]
    // [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClientesController(ClienteService service)
        {
            this._service = service;
        }

        // GET: api/Clientes : Obter Informação dos Clientes
        // [Authorize(Policy = "Admin")]
        //   [EnableCors]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get_Clientes()
        {
         
            return await this._service.GetAllClientes();
        }

        //// GET: api/Clientes/5  - Obter Informação de um Cliente por ID
        //[Authorize(Policy = "Admin")]
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(long id)
        {
            return await _service.GetClienteById(id);
        }
        //[Authorize(Policy = "Admin")]
        // [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
        
            return await _service.CreateCliente(cliente);
        }

        //[Authorize(Policy = "Admin")]
        //[EnableCors]
        [HttpPut]
        public async Task<ActionResult<Cliente>> UpdateCliente(Cliente cliente)
        {

            return await _service.UpdateCliente(cliente);
        }

        // DELETE: api/Clientes/5
        //[Authorize(Policy = "Admin")]
        // [EnableCors]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(long id)
        {
            
          return  await _service.DeleteCliente(id);
                        
        }
        //[Authorize(Policy = "Admin")]
        public async Task<ActionResult<Cliente>> UpdatePagamentoCliente(long clienteID,float valor)
        {

            return await _service.UpdatePagamentoCliente(clienteID, valor);
        }     

        //public async Task<bool> ClienteExists(long id)
        //{
        //    return await _service.FindClientAny(id);
        //}
    }
}