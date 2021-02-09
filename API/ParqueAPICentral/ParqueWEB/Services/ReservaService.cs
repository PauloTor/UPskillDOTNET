using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;

namespace ParqueAPICentral.Services
{
    public class ReservaService
    {
        private readonly IReservaRepository _repo;

        public ReservaService(IReservaRepository repo)
        {
            this._repo = repo;
        }

        public async Task<ActionResult<IEnumerable<Reserva>>> GetAllReservas()
        {
            return await this._repo.GetAllReservasAsync();
        }
    }
}
