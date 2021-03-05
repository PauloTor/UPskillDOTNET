using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PseudoCompanyFront.Data;
using PseudoCompanyFront.Models;
/*
namespace PseudoCompanyFront.Controllers
{
    public class SubAlugueresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubAlugueresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubAlugueres
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubAluguer.Include(s => s.Reserva);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubAlugueres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAluguer = await _context.SubAluguer
                .Include(s => s.Reserva)
                .FirstOrDefaultAsync(m => m.SubAluguerID == id);
            if (subAluguer == null)
            {
                return NotFound();
            }

            return View(subAluguer);
        }

        // GET: SubAlugueres/Create
        public IActionResult Create()
        {
            ViewData["ReservaID"] = new SelectList(_context.Reserva, "ReservaID", "ReservaID");
            return View();
        }

        // POST: SubAlugueres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubAluguerID,Preco,Reservado,NovoCliente,ReservaID")] SubAluguer subAluguer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subAluguer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservaID"] = new SelectList(_context.Reserva, "ReservaID", "ReservaID", subAluguer.ReservaID);
            return View(subAluguer);
        }

        // GET: SubAlugueres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAluguer = await _context.SubAluguer.FindAsync(id);
            if (subAluguer == null)
            {
                return NotFound();
            }
            ViewData["ReservaID"] = new SelectList(_context.Reserva, "ReservaID", "ReservaID", subAluguer.ReservaID);
            return View(subAluguer);
        }

        // POST: SubAlugueres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SubAluguerID,Preco,Reservado,NovoCliente,ReservaID")] SubAluguer subAluguer)
        {
            if (id != subAluguer.SubAluguerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subAluguer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubAluguerExists(subAluguer.SubAluguerID))
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
            ViewData["ReservaID"] = new SelectList(_context.Reserva, "ReservaID", "ReservaID", subAluguer.ReservaID);
            return View(subAluguer);
        }

        // GET: SubAlugueres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAluguer = await _context.SubAluguer
                .Include(s => s.Reserva)
                .FirstOrDefaultAsync(m => m.SubAluguerID == id);
            if (subAluguer == null)
            {
                return NotFound();
            }

            return View(subAluguer);
        }

        // POST: SubAlugueres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var subAluguer = await _context.SubAluguer.FindAsync(id);
            _context.SubAluguer.Remove(subAluguer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubAluguerExists(long id)
        {
            return _context.SubAluguer.Any(e => e.SubAluguerID == id);
        }
    }
}
*/