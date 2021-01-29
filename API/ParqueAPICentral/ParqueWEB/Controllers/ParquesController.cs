using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParqueWEB;
using ParqueWEB.Data;

namespace ParqueWEB.Controllers
{
    public class ParquesController : Controller
    {
        private readonly ParqueWEBContext _context;

        public ParquesController(ParqueWEBContext context)
        {
            _context = context;
        }

        // GET: Parques
        public async Task<IActionResult> Index()
        {
            var parqueWEBContext = _context.Parque.Include(p => p.Morada);
            return View(await parqueWEBContext.ToListAsync());
        }

        // GET: Parques/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parque = await _context.Parque
                .Include(p => p.Morada)
                .FirstOrDefaultAsync(m => m.ParqueID == id);
            if (parque == null)
            {
                return NotFound();
            }

            return View(parque);
        }

        // GET: Parques/Create
        public IActionResult Create()
        {
            ViewData["MoradaID"] = new SelectList(_context.Morada, "MoradaID", "MoradaID");
            return View();
        }

        // POST: Parques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParqueID,NomeParque,Lotacao,MoradaID")] Parque parque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoradaID"] = new SelectList(_context.Morada, "MoradaID", "MoradaID", parque.MoradaID);
            return View(parque);
        }

        // GET: Parques/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parque = await _context.Parque.FindAsync(id);
            if (parque == null)
            {
                return NotFound();
            }
            ViewData["MoradaID"] = new SelectList(_context.Morada, "MoradaID", "MoradaID", parque.MoradaID);
            return View(parque);
        }

        // POST: Parques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ParqueID,NomeParque,Lotacao,MoradaID")] Parque parque)
        {
            if (id != parque.ParqueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParqueExists(parque.ParqueID))
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
            ViewData["MoradaID"] = new SelectList(_context.Morada, "MoradaID", "MoradaID", parque.MoradaID);
            return View(parque);
        }

        // GET: Parques/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parque = await _context.Parque
                .Include(p => p.Morada)
                .FirstOrDefaultAsync(m => m.ParqueID == id);
            if (parque == null)
            {
                return NotFound();
            }

            return View(parque);
        }

        // POST: Parques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parque = await _context.Parque.FindAsync(id);
            _context.Parque.Remove(parque);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParqueExists(long id)
        {
            return _context.Parque.Any(e => e.ParqueID == id);
        }
    }
}
