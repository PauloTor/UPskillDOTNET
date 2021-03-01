using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public interface ISubAluguerRepository : IRepositoryBase<SubAluguer>
 
    
    {
        Task<ActionResult<IEnumerable<SubAluguer>>> GetAllSubAluguerAsync();
        Task<ActionResult<SubAluguer>> FindSubAluguerById(long id);
        Task<ActionResult<SubAluguer>> CreateSubAluguer(SubAluguer subAluguer); 
        Task<ActionResult<SubAluguer>> UpdateSubAluguer(SubAluguer subaluguer);
        Task<ActionResult<SubAluguer>> DeleteSubAluguer(long id);
        Task<ActionResult<SubAluguer>> PostSubAluguer(long reservaID, float preco,bool reservado);
    }
   
}
