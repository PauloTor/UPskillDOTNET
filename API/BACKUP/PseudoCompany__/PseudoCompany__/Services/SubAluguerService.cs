using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Services
{
    public class SubAluguerService
    {
        private readonly ISubAluguerRepository _repo;

        public SubAluguerService(ISubAluguerRepository repo)
        {
            this._repo = repo;
        }
     
       public async Task<ActionResult<IEnumerable<SubAluguer>>> GetAllSubAluguerAsync()
        {
            return await this._repo.GetAllSubAluguerAsync();
        }
        public async Task<ActionResult<SubAluguer>> UpdateSubAluguer(SubAluguer subaluguer)
        {
            return await this._repo.UpdateSubAluguer(subaluguer);
        }

        public async Task<ActionResult<SubAluguer>> FindSubAluguerById(long id)
        {
            return await this._repo.FindSubAluguerById(id);
        }

        public async Task<ActionResult<SubAluguer>> FindSubAluguerByReserva(long id)
        {
            return await this._repo.FindSubAluguerByReserva(id);
        }

        public async Task<ActionResult<SubAluguer>> DeleteSubAluguer(long id)
        {
            return await _repo.DeleteSubAluguer(id);
        }

        public async Task<ActionResult<SubAluguer>> PostSubAluguer(SubAluguer subaluguer)
        {
            return await _repo.PostSubAluguer(subaluguer);
        }
    }
}

