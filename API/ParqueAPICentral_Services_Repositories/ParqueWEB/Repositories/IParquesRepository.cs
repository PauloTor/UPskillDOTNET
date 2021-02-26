using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Repositories
{
    public interface IParquesRepository : IRepositoryBase<Parque>
    {
        
        Task<ActionResult<Parque>> GetParqueById(long id);
        Task<ActionResult<IEnumerable<Parque>>> GetAllParqueAsync();

        //Task<ActionResult<IEnumerable<Parque>>> PostParquesByDataAsync(String DataInicio, String DataFim, long ClienteID);

        //Task<ActionResult<Parque>> CancelarParquesAsync(long id);

    }
}
