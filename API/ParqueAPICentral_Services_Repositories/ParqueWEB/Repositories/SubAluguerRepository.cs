﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public class SubAluguerRepository : RepositoryBase<SubAluguer>, ISubAluguerRepository
    {
        public SubAluguerRepository(APICentralContext theContext) : base(theContext)
        {
        }
        public async Task<ActionResult<IEnumerable<SubAluguer>>> GetAllSubAluguerAsync()
        {
            return await RepContext.SubAluguer.ToListAsync();
        }
        public async Task<ActionResult<SubAluguer>> FindSubAluguerById(long id)
        {
            return await RepContext.SubAluguer.FindAsync(id);
        }
       
        public async Task<ActionResult<SubAluguer>> PostSubAluguer(long reservaID, float preco)
        {
            var sub = new SubAluguer(reservaID, preco);
            return await AddAsync(sub);
        }

        public async Task<ActionResult<SubAluguer>> CreateSubAluguer(SubAluguer subaluguer)
        {
           
            return await AddAsync(subaluguer);
        }

        public async Task<ActionResult<SubAluguer>> DeleteSubAluguer(long id)
        {
            var subAluguer = GetAll().FirstOrDefault(u => u.SubAluguerID == id);

            await DeleteAsync(subAluguer);

            return subAluguer;
        }

        public async Task<ActionResult<SubAluguer>> UpdateSubAluguer(SubAluguer subaluguer)
        {
            await UpdateAsync(subaluguer);

            return subaluguer;
        }
              
        public async Task<bool> FindClienteAny(long id)
        {
            return await GetAll().Where(p => p.SubAluguerID == id).AnyAsync();
        }




    }
} 
