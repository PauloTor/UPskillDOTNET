using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Services
{
    public class MoradaService
    {
        private readonly IMoradaRepository _repo;
        public MoradaService(IMoradaRepository repo)
        {
            this._repo = repo;
        }


        public async Task<ActionResult<IEnumerable<Morada>>> GetAllMoradas()
        {
            return await this._repo.GetAllMoradasAsync();
        }

        //public async Task<ActionResult<Cliente>> GetClienteById(long id)
        //{

        //    return await this._repo.FindClienteById(id);
        //}
        //public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        //{
        //    return await _repo.CreateCliente(cliente);
        //}

        //public async Task<ActionResult<Cliente>> DeleteCliente(long id)
        //{
        //    return await _repo.DeleteCliente(id);
        //}

        //public async Task<ActionResult<Cliente>> UpdatePagamentoCliente(long clienteid, float valor)
        //{
        //    var cliente = await _repo.FindById(clienteid);
        //    if (valor < 0)
        //    {
        //        throw new Exception("A quantia a pagar deve ter um valor positivo.");
        //    }
        //    else
        //    {
        //        cliente.Value.Credito -= valor;
        //        // Cofre.Entrada(valor);
        //    }
        //    if (cliente.Value.Credito < 0)
        //    {
        //        // Cofre.Saida(valor);
        //        cliente.Value.Credito += valor;
        //        throw new Exception("O crédito não permite efetuar a operação.");
        //    }
        //    return await _repo.UpdatePagamentoCliente(cliente.Value);
        //}


        //public async Task<ActionResult<Cliente>> UpdateCliente(Cliente cliente)
        //{

        //    return await _repo.UpdateCliente(cliente);
        //}
    }
}

