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
       
        public async Task<ActionResult<SubAluguer>> PostSubAluguer(SubAluguer subAluguer)
        {
            return await AddAsync(subAluguer);
        }

        public async Task<ActionResult<SubAluguer>> DeleteSubAluguer(long id)
        {
            var subAluguer = GetAll().FirstOrDefault(u => u.ReservaID == id);

            await DeleteAsync(subAluguer);

            return subAluguer;
        }

        public async Task<ActionResult<SubAluguer>> UpdateSubAluguer(SubAluguer subaluguer)
        {
            await UpdateAsync(subaluguer);

            return subaluguer;
        }
    }
} 
