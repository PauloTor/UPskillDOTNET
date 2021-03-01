using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PseudoCompanyFront.Data;
using PseudoCompanyFront.Models;

namespace PseudoCompanyFront.Controllers
{

    public class ReservasController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        public ReservasController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }


        // GET: Reservas
        public async Task<ActionResult> IndexAsync()
        {
            using HttpClient client = new HttpClient();
            string endpoint = apiBaseUrl + "/ReservasCentral";
            var response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var readTask = response.Content.ReadAsAsync<IEnumerable<Reserva>>();
                readTask.Wait();

                IEnumerable<Reserva> reservas = readTask.Result;

                return View(reservas);
            }

            else
            {
                return BadRequest("Server error. Please contact administrator.");
            }
        }


        // GET: Reservas/Details/5
        public async Task<IActionResult> DetailsAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using HttpClient client = new HttpClient();
            string endpoint = apiBaseUrl + "/ReservasCentral/" + id;
            var response = await client.GetAsync(endpoint);
            var reserva = await response.Content.ReadAsAsync<Reserva>();

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservaID,ParqueID,LugarID,ClienteID")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                using HttpClient client = new HttpClient();
                string endpoint = apiBaseUrl + "/Post";
                var response = await client.GetAsync(endpoint);

                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

    
        // DELETE: Reservas/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Cancelar/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
