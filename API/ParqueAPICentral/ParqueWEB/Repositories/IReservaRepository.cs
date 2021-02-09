using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Repositories
{
    public interface IReservaRepository : IRepositoryBase<Reserva>
    {
        Task<ActionResult<IEnumerable<Reserva>>> GetAllReservasAsync();
    }
}