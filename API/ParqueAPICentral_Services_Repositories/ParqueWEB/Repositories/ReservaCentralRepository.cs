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
    public class ReservaCentralRepository : RepositoryBase<Reserva>, IReservaCentralRepository
    {
        public ReservaCentralRepository(APICentralContext RepContext) : base(RepContext)
        {
        }

        public async Task<ActionResult<IEnumerable<Reserva>>> GetAllReservasCentralAsync()
        {
            return await RepContext.Reserva.ToListAsync();
        }
        
        public async Task<ActionResult<Reserva>> GetAllClienteByReservasCentralAsync(long parqueID, long id)
        {

            var Res = await RepContext.Reserva.Where(r => r.ParqueID == parqueID).Where(rr => rr.ReservaAPI == id).FirstOrDefaultAsync();

            return Res;
        }
         
        public async Task<ActionResult<Reserva>> GetReservaByIdAsync(long id)
        {
            return await RepContext.Reserva.Where(r => r.ReservaID == id).FirstOrDefaultAsync();
        }
        
        public async Task<ActionResult<Reserva>> ParaSubALuguer(long ReservaID, bool boleano)  // put booleano
        {
            return await RepContext.Reserva.Where(r => r.ReservaID == ReservaID).FirstOrDefaultAsync();
        }
        
        public async Task<ActionResult<Reserva>> DeleteReservaCentral(long ParqueID, long id)
        {
            var reserva = GetAll().Where(d => d.ParqueID == ParqueID).Where(d => d.ReservaAPI == id).ToList().FirstOrDefault();

            return await DeleteAsync(reserva);
        }

        public async Task<ActionResult<Reserva>> CriarReservaCentral(Reserva reserva)
        {
            return await AddAsync(reserva);
        }



    }
}
