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
        private readonly ReservaCentralService _serviceR;
        public SubAluguerService(ISubAluguerRepository repo, ReservaCentralService serviceR)
        {
            this._repo = repo;
            this._serviceR = serviceR;
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

        public async Task<ActionResult<SubAluguer>> CreateCliente(SubAluguer subAluguer)
        {
            return await _repo.CreateSubAluguer(subAluguer);
        }

        public async Task<ActionResult<SubAluguer>> DeleteSubAluguer(long id)
        {
            return await _repo.DeleteSubAluguer(id);
        }
        
        public async Task<ActionResult<SubAluguer>> PostSubAluguer(long reservaID, float preco)
        {
            await _serviceR.ParaSubALuguer(reservaID, true); // alterar reservacentral para booleano true
                        
            return await _repo.PostSubAluguer(reservaID,preco);
        }
    }
}

