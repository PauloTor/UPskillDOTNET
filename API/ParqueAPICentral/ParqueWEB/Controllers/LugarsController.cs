using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParqueWEB.Data;
using ParqueWEB.Models;

namespace ParqueWEB.Controllers
{
    public class LugarsController : Controller
    {
        private readonly ParqueWEBContext _context;

        public LugarsController(ParqueWEBContext context)
        {
            _context = context;
        }

        // GET: Lugars
        public async Task<IActionResult> Index()
        {
            var parqueWEBContext = _context.Lugar.Include(l => l.Parque);
            return View(await parqueWEBContext.ToListAsync());
        }

        // GET: Lugars/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lugar = await _context.Lugar
                .Include(l => l.Parque)
                .FirstOrDefaultAsync(m => m.LugarID == id);
            if (lugar == null)
            {
                return NotFound();
            }

            return View(lugar);
        }

        // GET: Lugars/Create
        public IActionResult Create()
        {
            ViewData["ParqueID"] = new SelectList(_context.Set<Parque>(), "ParqueID", "ParqueID");
            return View();
        }

        // POST: Lugars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LugarID,Fila,Sector,Preço,ParqueID")] Lugar lugar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lugar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParqueID"] = new SelectList(_context.Set<Parque>(), "ParqueID", "ParqueID", lugar.ParqueID);
            return View(lugar);
        }

        // GET: Lugars/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lugar = await _context.Lugar.FindAsync(id);
            if (lugar == null)
            {
                return NotFound();
            }
            ViewData["ParqueID"] = new SelectList(_context.Set<Parque>(), "ParqueID", "ParqueID", lugar.ParqueID);
            return View(lugar);
        }

        // POST: Lugars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("LugarID,Fila,Sector,Preço,ParqueID")] Lugar lugar)
        {
            if (id != lugar.LugarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lugar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LugarExists(lugar.LugarID))
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
            ViewData["ParqueID"] = new SelectList(_context.Set<Parque>(), "ParqueID", "ParqueID", lugar.ParqueID);
            return View(lugar);
        }

        // GET: Lugars/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lugar = await _context.Lugar
                .Include(l => l.Parque)
                .FirstOrDefaultAsync(m => m.LugarID == id);
            if (lugar == null)
            {
                return NotFound();
            }

            return View(lugar);
        }

        // POST: Lugars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var lugar = await _context.Lugar.FindAsync(id);
            _context.Lugar.Remove(lugar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LugarExists(long id)
        {
            return _context.Lugar.Any(e => e.LugarID == id);
        }
    }
}
