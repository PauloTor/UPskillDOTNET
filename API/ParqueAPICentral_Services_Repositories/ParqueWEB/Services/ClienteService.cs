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

            return await this._repo.FindClienteById(id);
        }
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            return await _repo.CreateCliente(cliente);
        }

        public async Task<ActionResult<Cliente>> DeleteCliente(long id)
        {
            return await _repo.DeleteCliente(id);
        }
        
        public async Task<ActionResult<Cliente>> UpdateCliente(Cliente cliente)
        {

            return await _repo.UpdateCliente(cliente);
        }
    }
}

