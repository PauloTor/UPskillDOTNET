using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using Microsoft.Extensions.Configuration;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Data;

namespace ParqueAPICentral.Services
{
    public class ReservaCentralService
    {
        private readonly IReservaCentralRepository _repo;

        public ReservaCentralService(IReservaCentralRepository repo)
        {
            this._repo = repo;
        }


        public async Task<ActionResult<IEnumerable<Reserva>>> GetAllReservasCentralAsync()
        {

            return await _repo.GetAllReservasCentralAsync();
        }

        public async Task<ActionResult<Reserva>> UpdateReserva(Reserva reserva)
        {

            return await _repo.UpdateReserva(reserva);
        }



        public async Task<ActionResult<Reserva>> DeleteReservaCentral(long id)
    {
        return await _repo.DeleteReservaCentral(id);
    }
        
        public async Task<ActionResult<Reserva>> GetAllClienteByReservasCentral(long ParqueID, long id)
        {
            return await _repo.GetAllClienteByReservasCentralAsync(ParqueID, id);
        }

        public async Task<ActionResult<Reserva>> ParaSubALuguer(long id)
        {
            /*
            var reserva = _repo.GetReservaByIdAsync(id);
            var r = reserva.Result.Value;

            if (r.ParaSubAluguer == false)
            {
                r.ParaSubAluguer = true;
                await _serv.CreateSubAluguer(new SubAluguer
                {
                    Preco = 11,
                    ReservaID = id,
                    Reservado = false
                });
            }
            else
            {
                r.ParaSubAluguer = false;
                await _serv.DeleteSubAluguer(id);
            }
            */
        return await _repo.ParaSubALuguer(id);
        }

        public async Task<ActionResult<Reserva>>  CriarReservaCentral(Reserva reserva)
        {
                    
            return await _repo.CriarReservaCentral(reserva);

        }

        public async Task<ActionResult<Reserva>> GetReservaById(long id)
        {
            return await _repo.GetReservaByIdAsync(id);
        }
        
    }
}

