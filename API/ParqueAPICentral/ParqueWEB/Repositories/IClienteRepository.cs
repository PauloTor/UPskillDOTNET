using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<ActionResult<IEnumerable<Cliente>>>  GetAllClientesAsync();
        Task<ActionResult<Cliente>> FindClienteById(long id);
    }
}
