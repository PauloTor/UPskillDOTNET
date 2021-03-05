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

        public async Task<ActionResult<Reserva>> UpdateReserva(Reserva reserva)
        {
            await UpdateAsync(reserva);

            return reserva;
        
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
        
        public async Task<ActionResult<Reserva>> ParaSubALuguer(long id)  // put booleano
        {
            var reserva = RepContext.Reserva.Where(r => r.ReservaID == id).FirstOrDefault();
            reserva.ParaSubAluguer = true;
            return await UpdateReserva(reserva);
        }
        
        public async Task<ActionResult<Reserva>> DeleteReservaCentral(long id)
        {
            var reserva = GetReservaByIdAsync(id).Result.Value;

            return await DeleteAsync(reserva);
        }

        public async Task<ActionResult<Reserva>> CriarReservaCentral(Reserva reserva)
        {
            return await AddAsync(reserva);
        }
    }
}
