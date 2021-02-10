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
        Task<Fatura> CreateFatura(Fatura fatura);

        Fatura FindFatura(long faturaId);
    }
}
