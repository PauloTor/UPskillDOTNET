using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repo;
        public ClienteService(IClienteRepository repo)
        {
            this._repo = repo;
        }
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllClientes()
        {
            return await this._repo.GetAllClientesAsync();
        }
        public async Task<ActionResult<Cliente>> GetClienteById(long id)
        {
            //if (await GetClienteById(id) == null)
            //{
            //    return NotFound();
            //}
            return await this._repo.FindClienteById(id);
        }
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            return await _repo.CreateCliente(cliente);
        }
        public async Task<ActionResult<Cliente>> UpdateCliente(Cliente cliente)
        {
            //if (cliente == null)
            //{
            //    return NotFound();
            //}
            return await _repo.UpdateCliente(cliente);
        }
        public async Task<ActionResult<Cliente>> DeleteCliente(long id)
        {
            return await _repo.DeleteCliente(id);
        }
        //private ActionResult<Cliente> CreatedAtAction(string v, object p, Cliente cliente)
        //{
        //    throw new NotImplementedException();
        //}

        //private ActionResult<Cliente> NotFound()
        //{
        //    throw new NotImplementedException("O que procura não existe.");
        //}
        //public async Task<ActionResult<IEnumerable<Cliente>>> UpdateClienteById(long id, Cliente cliente)
        //{

        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }

        //}
    }
}
