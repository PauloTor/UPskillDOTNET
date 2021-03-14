using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Models;
namespace ParqueAPICentral.Repositories
{
    public interface IReservaCentralRepository : IRepositoryBase<Reserva>
    {

        Task<ActionResult<IEnumerable<Reserva>>> GetAllReservasCentralAsync();
        Task<ActionResult<Reserva>> DeleteReservaCentral(long id);
        Task<ActionResult<Reserva>> GetAllClienteByReservasCentralAsync(long ParqueID, long id);
        Task<ActionResult<Reserva>> GetReservaByIdAsync(long id);
        Task<ActionResult<Reserva>> ParaSubALuguer(long id);
        Task<ActionResult<Reserva>> CriarReservaCentral(Reserva reserva);
        Task<ActionResult<Reserva>> UpdateReserva(Reserva reserva);
        Task<ActionResult<IEnumerable<Reserva>>> GetAllReservaByUserAsync(string id);
    }
}
