using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Authentication;
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
        public ClienteRepository(ApplicationDbContext theContext) : base(theContext)
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
       
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            return await AddAsync(cliente);
        }

        public async Task<ActionResult<Cliente>> UpdatePagamentoCliente(Cliente cliente)
        {

            return await UpdateAsync(cliente);
        }

        public async Task<ActionResult<Cliente>> DeleteCliente(long id)
        {
            var cliente = GetAll().FirstOrDefault(u => u.ClienteID == id);

            await DeleteAsync(cliente);

            return cliente;
        }

        public async Task<ActionResult<Cliente>> UpdateCliente(Cliente cliente)
        {
            await UpdateAsync(cliente);

            return cliente;
        }


        

        //public async Task<ActionResult<Cliente>> UpdateClienteById(Cliente cliente, long Id)
        //{
        //    await UpdateByIdAsync(cliente, id);

        //    return cliente;
        //}



        public async Task<bool> FindClienteAny(long id)
        {
            return await GetAll().Where(p => p.ClienteID == id).AnyAsync();
        }




    }
} 
