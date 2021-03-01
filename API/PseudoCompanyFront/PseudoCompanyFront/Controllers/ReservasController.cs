using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PseudoCompanyFront.Data;
using PseudoCompanyFront.Models;

namespace PseudoCompanyFront.Controllers
{

    public class ReservasController : Controller
    {

        // GET: Reservas
        public ActionResult Index()
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44330/api/ReservasParque/1")
            };

            var responseTask = client.GetAsync(client.BaseAddress);
            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<Reserva>>();
                readTask.Wait();

                IEnumerable<Reserva> reservas = readTask.Result;

                return View(reservas);
            }

            else
            {
                return BadRequest("Server error. Please contact administrator.");
            }
        }

        /*
        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.ReservaID == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservaID,ParqueID,LugarID,ClienteID")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ReservaID,ParqueID,LugarID,ClienteID")] Reserva reserva)
        {
            if (id != reserva.ReservaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.ReservaID))
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
            return View(reserva);
        }


        
        // DELETE: Reservas/Delete/5
        public ActionResult Delete()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44330/api/Reservas/Cancelar");
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Reserva>>();
                    readTask.Wait();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return NoContent();
        }*/
    }
}
