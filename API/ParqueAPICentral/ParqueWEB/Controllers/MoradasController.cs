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
    public class MoradasController : Controller
    {
        private readonly ParqueWEBContext _context;

        public MoradasController(ParqueWEBContext context)
        {
            _context = context;
        }

        // GET: Moradas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Morada.ToListAsync());
        }

        // GET: Moradas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var morada = await _context.Morada
                .FirstOrDefaultAsync(m => m.MoradaID == id);
            if (morada == null)
            {
                return NotFound();
            }

            return View(morada);
        }

        // GET: Moradas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoradaID,Rua,CodigoPostal")] Morada morada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(morada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(morada);
        }

        // GET: Moradas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var morada = await _context.Morada.FindAsync(id);
            if (morada == null)
            {
                return NotFound();
            }
            return View(morada);
        }

        // POST: Moradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MoradaID,Rua,CodigoPostal")] Morada morada)
        {
            if (id != morada.MoradaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(morada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoradaExists(morada.MoradaID))
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
            return View(morada);
        }

        // GET: Moradas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var morada = await _context.Morada
                .FirstOrDefaultAsync(m => m.MoradaID == id);
            if (morada == null)
            {
                return NotFound();
            }

            return View(morada);
        }

        // POST: Moradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var morada = await _context.Morada.FindAsync(id);
            _context.Morada.Remove(morada);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoradaExists(long id)
        {
            return _context.Morada.Any(e => e.MoradaID == id);
        }
    }
}
