using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Repositories
{
    public class ReservaRepository : RepositoryBase<ReservaPrivateDTO>, IReservaRepository
    {
        public ReservaRepository(APICentralContext RepContext) : base(RepContext)
        {
        }

        //public async Task<ActionResult<IEnumerable<ReservaPrivateDTO>>> GetAllReservasAsync()
        //{
        //    return await RepContext.ReservaPrivateDTO.ToListAsync();
        //}

        //public async Task<ActionResult<ReservaPrivateDTO>> PostReservaByDataAsync(String DataInicio, String DataFim, long ClienteID)
        //{
        //    return await RepContext.ReservaPrivateDTO.ToListAsync();
        //}

        //public async Task<ActionResult<Reserva>> CancelarReservaAsync(long id)
        //{
        //    return await RepContext.CancelarReserva(id);
        //}
    }
}
