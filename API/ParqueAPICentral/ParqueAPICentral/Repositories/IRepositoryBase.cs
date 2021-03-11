using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<ActionResult<IEnumerable<T>>> FindAllAsync();
        Task<ActionResult<T>> FindById(long id);
        Task<ActionResult<T>> FindByCod(string cod);
        Task<IActionResult> UpdateById(long id, T entity);
        Task<IActionResult> UpdateByCod(string cod, T entity);
        Task<ActionResult<T>> Create(T entity);
        Task<IActionResult> DeleteById(long id);

        
        Task<IActionResult> DeleteByCod(long id);
        Task<T> AddAsync(T entity);
        Task<ActionResult<IEnumerable<T>>> GetAllAsync();
    }
}
