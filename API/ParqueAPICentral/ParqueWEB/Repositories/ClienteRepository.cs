/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(APICentralContext theContext) : base(theContext)
        {
        }
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllClientesAsync()
        {
            return await RepContext.Cliente.ToListAsync();
        }
        public async Task<ActionResult<Cliente>> FindClienteById(long id)
        {
            return await RepContext.Cliente.FindAsync(id);
        }
        public Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<IEnumerable<Cliente>>> UpdateClienteById(long id, Cliente cliente)
        {
            return await RepContext.Cliente.ToListAsync();
        }
    }
} */
