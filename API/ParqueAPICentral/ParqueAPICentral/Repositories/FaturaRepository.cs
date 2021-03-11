using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Contexts;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{

    public class FaturaRepository : RepositoryBase<Fatura>, IFaturaRepository
    {


        public FaturaRepository(ApplicationDbContext theContext) : base(theContext)
        {

        }

        public async Task<ActionResult<Fatura>> CreateFatura(Fatura fatura)
        {
            var faturaCriada = RepContext.Fatura.Add(fatura);
            await RepContext.SaveChangesAsync();
            return faturaCriada.Entity;
        }


        public async Task<ActionResult<Fatura>> FindFatura(long faturaId)
        {
            return await RepContext.Fatura.FindAsync(faturaId);
        }
        
        public async Task<ActionResult<IEnumerable<Fatura>>> GetAllFaturas()
        {
        return await RepContext.Fatura.ToListAsync();
        }
    }

}