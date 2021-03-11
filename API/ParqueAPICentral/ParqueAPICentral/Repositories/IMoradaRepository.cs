using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public interface IMoradaRepository : IRepositoryBase<Morada>
    {
        Task<ActionResult<IEnumerable<Morada>>> GetAllMoradasAsync();
        //Task<ActionResult<Cliente>> FindClienteById(long id);
        //Task<ActionResult<Cliente>> CreateCliente(Cliente cliente); 
        //Task<ActionResult<Cliente>> UpdateCliente(Cliente cliente);
        //Task<ActionResult<Cliente>> DeleteCliente(long id);

        //Task<ActionResult<Cliente>> UpdatePagamentoCliente(Cliente cliente);
    }
}
