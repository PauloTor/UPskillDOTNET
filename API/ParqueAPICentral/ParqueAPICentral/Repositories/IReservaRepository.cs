using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Models;
namespace ParqueAPICentral.Repositories
{
    public interface IReservaRepository : IRepositoryBase<ReservaPrivateDTO>
    {
        //Task<ActionResult<IEnumerable<ReservaPrivateDTO>>> GetAllReservasAsync();

        //Task<ActionResult<ReservaPrivateDTO>> PostReservaByDataAsync(String DataInicio, String DataFim, long ClienteID);
        
       // Task<ActionResult<ReservaPrivateDTO>> CancelarReservaAsync(long id);

    }
    
}
