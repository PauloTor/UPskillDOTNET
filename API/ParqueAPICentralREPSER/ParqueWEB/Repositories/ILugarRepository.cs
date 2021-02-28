using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Repositories
{
    public interface ILugarRepository : IRepositoryBase<LugarDTO>
    {
        Task<ActionResult<IEnumerable<LugarDTO>>> GetAllLugaresAsync();
        
        Task<ActionResult<IEnumerable<LugarDTO>>> GetLugarById(long id);

        //Task<ActionResult<LugarDTO>> CancelarLugaresAsync(long id);

    }
}
