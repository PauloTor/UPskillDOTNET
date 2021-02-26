using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Repositories
{
    public class ParquesRepository : RepositoryBase<Parque>, IParquesRepository
    {
        public ParquesRepository(APICentralContext RepContext) : base(RepContext)
        {
        }

       public async Task<ActionResult<IEnumerable<Parque>>> GetAllParqueAsync()
        {
            return await RepContext.Parque.ToListAsync();
        }


        public async Task<ActionResult<Parque>> GetParqueById(long id)
        {
            return await RepContext.Parque.FindAsync(id);
        }

        //public async Task<ActionResult<IEnumerable<Parque>>> PostReservaByDataAsync(String DataInicio, String DataFim, long ClienteID)
        //{
        //    return await RepContext.Reserva.ToListAsync();
        //}

        //public async Task<ActionResult<Parque>> CancelarParqueAsync(long id)
        //{
        //    return await RepContext.CancelarParque(id);
        //}
    }
}
