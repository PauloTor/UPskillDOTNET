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
        private readonly ReservaCentralService _serviceR;

        public FaturaService(IFaturaRepository repo, ReservaCentralService serviceR)
        {
            this._repo = repo;
            this._serviceR = serviceR;
        }

        public FaturaService()
        { }
        //=============================================>>>>   reservacentral
        public async Task<ActionResult<Fatura>> CreateFaturaByReservaID(long ReservaID)
        {
            var reserva = _serviceR.GetReservaById(ReservaID);
            var reservaComPrecoLugar = await new ParquePrivateReservaService().GetReservaLugarAsync(reserva.Result.Value.ParqueID, reserva.Result.Value.ReservaAPI);
            //Conversão para horas
            var timeSpan = reservaComPrecoLugar.DataFim.Subtract(reservaComPrecoLugar.DataInicio);
            var horas = timeSpan.Hours;
            var preco = reservaComPrecoLugar.PrecoLugar * horas;
            var fatura = new Fatura(DateTime.Now, preco, ReservaID);
            return await _repo.CreateFatura(fatura);
        }

        public async Task<ActionResult<Fatura>> FindFaturaByID(long faturaId)
        {
            return await _repo.FindFatura(faturaId);
        }

        public async Task<ActionResult<IEnumerable<Fatura>>> GetAllFaturas()
        {
            return await _repo.GetAllFaturas();
        }

    }

}