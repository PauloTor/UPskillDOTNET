using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Services
{
    public class FaturaService
    {
        private readonly IFaturaRepository _repo;
        public FaturaService(IFaturaRepository repo)
        {
            this._repo = repo;
        }

        public async Task<Fatura> CreateFaturaByReservaID(long ReservaID)
        {
            var reservaComPrecoLugar = await new ParquePrivateReservaService().GetReservaLugarAsync(ReservaID);
            //Conversão para horas
            var timeSpan = reservaComPrecoLugar.DataFim.Subtract(reservaComPrecoLugar.DataInicio);
            var horas = timeSpan.Hours;
            var preco = reservaComPrecoLugar.PrecoLugar * horas;
            var fatura = new Fatura(DateTime.Now, preco, ReservaID);
            return await _repo.CreateFatura(fatura);
        }

        public Fatura FindFaturaByID(long faturaId)
        {
            return _repo.FindFatura(faturaId);
        }

    }

}

