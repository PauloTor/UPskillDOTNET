using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public interface IFaturaRepository : IRepositoryBase<Fatura>
    {
        Task<ActionResult<Fatura>> CreateFatura(Fatura fatura);
        Task<ActionResult<Fatura>> FindFatura(long faturaId);
        Task<ActionResult<IEnumerable<Fatura>>> GetAllFaturas();
    }
}
