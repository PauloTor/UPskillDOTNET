using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{

    public class FaturaRepository : RepositoryBase<Fatura>, IFaturaRepository
    {


        public FaturaRepository(APICentralContext theContext) : base(theContext)
        {

        }

        public async Task<Fatura> CreateFatura(Fatura fatura)
        {
            var faturaCriada = RepContext.Fatura.Add(fatura);
            await RepContext.SaveChangesAsync();
            return faturaCriada.Entity;
        }

        public Fatura FindFatura(long faturaId)
        {
            return RepContext.Fatura.FirstOrDefault(n => n.FaturaID == faturaId);
        }
    }

}