using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using ParqueAPICentral.Repositories;
using Microsoft.Extensions.Configuration;
using ParqueAPICentral.DTO;
using ParqueAPICentral.Contexts;

namespace ParqueAPICentral.Services
{
    public class ReservaCentralService
    {
        private readonly IReservaCentralRepository _repo;
        private readonly SubAluguerService _serviceS;

        public ReservaCentralService(IReservaCentralRepository repo, SubAluguerService serviceS)
        {
            this._repo = repo;
            _serviceS = serviceS;
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
            var reserva = _repo.GetReservaByIdAsync(id).Result.Value;

            if (reserva.ParaSubAluguer == false)
            {
                reserva.ParaSubAluguer = true;
                await _serviceS.PostSubAluguer(new SubAluguer
                {
                    //Preco = 11,
                    ReservaID = id,
                    Reservado = false
                });
            }
            else
            {
                var sub = _serviceS.FindSubAluguerByReserva(id);
                if (sub.Result.Value.Reservado == false)
                {
                    reserva.ParaSubAluguer = false;
                    await _serviceS.DeleteSubAluguer(id);
                }
                else
                    throw new Exception("Este subaluguer já está reservado por outro utilizador e não é possível eliminar.");
            }
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

