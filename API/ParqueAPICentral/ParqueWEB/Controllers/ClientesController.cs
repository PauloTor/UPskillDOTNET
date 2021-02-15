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

namespace ParqueAPICentral.Controllers
{
    //[Authorize]
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/Clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly APICentralContext _context;

        public ClientesController(APICentralContext context)
        {
            _context = context;
        }

        // GET: api/Clientes : Obter Informação de um Cliente
        //      [Authorize]
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/Clientes/5  - Obter Informação de um Cliente por ID
        [EnableCors]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(long id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/{ClienteID}/{NomeCliente}{EmailCliente}/{NifCliente}/{MetodoPagamento}/{Credito}/{UserID} - Actualizar informação de um Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPut("{ClienteID}/{NomeCliente}/{EmailCliente}/{NifCliente}/{MetodoPagamento}/{Credito}/{UserID}")]
        public async Task<IActionResult> PutCliente(long ClienteID, string NomeCliente, string EmailCliente, int NifCliente, string MetodoPagamento, float Credito, long UserID)
        {
            var cliente = _context.Cliente.FirstOrDefault(n => n.ClienteID == ClienteID);

            if (cliente == null)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            cliente.NomeCliente = NomeCliente;
            cliente.EmailCliente = EmailCliente;
            cliente.NifCliente = NifCliente;
            cliente.MetodoPagamento = MetodoPagamento;
            cliente.Credito = Credito;
            cliente.Id = UserID;

            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        // PUT: api/Clientes/5 -  Actualizar informação de um Cliente pelo seu ID
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(long id, Cliente cliente)
        {
            var clienteDb = _context.Cliente.FirstOrDefault(n => n.ClienteID == id);

            if (id != clienteDb.ClienteID)
            {
                return BadRequest();
            }

            _context.Entry(clienteDb).State = EntityState.Modified;

            clienteDb.NomeCliente = cliente.NomeCliente;
            clienteDb.EmailCliente = cliente.EmailCliente;
            clienteDb.NifCliente = cliente.NifCliente;
            clienteDb.MetodoPagamento = cliente.MetodoPagamento;
            clienteDb.Credito = cliente.Credito;
            clienteDb.Id = cliente.Id;

            await _context.SaveChangesAsync();
        
            return Ok(clienteDb);
        }
        // POST: api/Clientes/{NomeCliente}/{EmailCliente}/{NifCliente}/{MetodoPagamento}/{Credito}/{UserID} : Criação de um Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPost("{NomeCliente}/{EmailCliente}/{NifCliente}/{MetodoPagamento}/{Credito}/{UserID}")]
        public async Task<ActionResult<Cliente>> PostCliente(string NomeCliente, string EmailCliente, int NifCliente, string MetodoPagamento, float Credito, long UserID)
        {
            Cliente cliente = new Cliente(NomeCliente, EmailCliente, NifCliente, MetodoPagamento, Credito, UserID);
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteID }, cliente);
        }

        // POST: api/Clientes : Criação de um Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostCliente", new { id = cliente.ClienteID }, cliente);
        }

        // DELETE: api/Clientes/5
        //[Authorize]
        [EnableCors]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(long id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
    /*//  [Authorize]
    [Route("api/Clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;
        public ClientesController(ClienteService service)
        {
            this._service = service;
        }

        // GET: api/Clientes : Obter Informação de um Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await this._service.GetAllClientes();
        }
        // GET: api/Clientes/5  - Obter Informação de um Cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(long id)
        {
            return await this._service.FindClienteById(id);
        }
        // POST: api/Clientes : Criação de um Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            return await this._service.CreateCliente(cliente cliente);
        }*/
