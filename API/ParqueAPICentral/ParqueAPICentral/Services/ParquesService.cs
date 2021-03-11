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
    public class ParquesService 
    {
        private readonly IParquesRepository _repo;
        public ParquesService(IParquesRepository repo)
        {
            this._repo = repo;
        }
        public async Task<ActionResult<IEnumerable<Parque>>> GetAllParques()
        {
            return await this._repo.GetAllParqueAsync();
        }

        public async Task<ActionResult<Parque>> GetParqueById(long id)
        {
           

            return await this._repo.GetParqueById(id);
        }


        public async Task<bool> ParqueExist(long id)
        {
            return await this._repo.ParqueExist(id);
        }
    }
}