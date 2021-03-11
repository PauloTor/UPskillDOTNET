using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using NPOI.SS.Formula.Functions;

using ParqueAPICentral.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext RepContext { get; set; }

        public RepositoryBase(ApplicationDbContext context)
        {
            this.RepContext = context;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                RepContext.Update(entity);
                await RepContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
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

        public IQueryable<T> GetAll()
        {
            try
            {
                return this.RepContext.Set<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public Task<IActionResult> UpdateByCod(string cod, T entity)
        {
            throw new NotImplementedException();
        }
        
        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await this.RepContext.AddAsync(entity);
                await this.RepContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public Task<IActionResult> UpdateById(long id, T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                this.RepContext.Remove(entity);
                await this.RepContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

    }

}
