using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PseudoCompanyFront.Data;
using PseudoCompanyFront.Models;

namespace PseudoCompanyFront.Controllers
{
    public class ParquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parques
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParquesDTO.ToListAsync());
        }

        // GET: Parques/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parqueDTO = await _context.ParquesDTO
                .FirstOrDefaultAsync(m => m.ParqueID == id);
            if (parqueDTO == null)
            {
                return NotFound();
            }

            return View(parqueDTO);
        }

        // GET: Parques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParqueID,NomeParque,NIFParque,Lotacao,Url,MoradaID")] ParqueDTO parqueDTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parqueDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parqueDTO);
        }

        // GET: Parques/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parqueDTO = await _context.ParquesDTO.FindAsync(id);
            if (parqueDTO == null)
            {
                return NotFound();
            }
            return View(parqueDTO);
        }

        // POST: Parques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ParqueID,NomeParque,NIFParque,Lotacao,Url,MoradaID")] ParqueDTO parqueDTO)
        {
            if (id != parqueDTO.ParqueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parqueDTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParqueDTOExists(parqueDTO.ParqueID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parqueDTO);
        }

        // GET: Parques/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parqueDTO = await _context.ParquesDTO
                .FirstOrDefaultAsync(m => m.ParqueID == id);
            if (parqueDTO == null)
            {
                return NotFound();
            }

            return View(parqueDTO);
        }

        // POST: Parques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parqueDTO = await _context.ParquesDTO.FindAsync(id);
            _context.ParquesDTO.Remove(parqueDTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParqueDTOExists(long id)
        {
            return _context.ParquesDTO.Any(e => e.ParqueID == id);
        }
    }
}
