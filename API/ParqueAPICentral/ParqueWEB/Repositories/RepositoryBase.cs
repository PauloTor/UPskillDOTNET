using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using ParqueAPICentral.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected APICentralContext RepContext { get; set; }

        public RepositoryBase(APICentralContext context)
        {
            this.RepContext = context;
        }

        public Task<ActionResult<T>> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteByCod(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<T>>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<T>> FindByCod(string cod)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<T>> FindById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            return await this.RepContext.Set<T>().ToListAsync();
        }

        public Task<IActionResult> UpdateByCod(string cod, T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateById(long id, T entity)
        {
            throw new NotImplementedException();
        }
    }

}
