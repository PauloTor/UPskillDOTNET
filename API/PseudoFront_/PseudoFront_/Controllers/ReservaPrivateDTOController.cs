using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PseudoFront_.DTO;
using PseudoFront_.Models;

namespace PseudoFront_.Controllers
{
    public class ReservaPrivateDTOController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        public ReservaPrivateDTOController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }


        // GET: Reservas
        public async Task<ActionResult> Index()
        {
            using HttpClient client = new();
            string endpoint = apiBaseUrl + "/ReservasCentral";
            var response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var readTask = response.Content.ReadAsAsync<IEnumerable<ReservaPrivateDTO>>();
                readTask.Wait();

                IEnumerable<ReservaPrivateDTO> reservas = readTask.Result;

                return View(reservas);
            }
            else
            {
                return BadRequest("Server error. Please contact administrator.");
            }
        }


        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using HttpClient client = new();
            string endpoint = apiBaseUrl + "/ReservasCentral/" + id;
            var response = await client.GetAsync(endpoint);
            var reserva = await response.Content.ReadAsAsync<ReservaPrivateDTO>();

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }


        // GET: Reservas/Create
        public async Task<IActionResult> Create()
        {
            using (HttpClient client = new())
            {
                string endpoint = apiBaseUrl + "/ReservasCentral";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var reservas = await response.Content.ReadAsAsync<List<ReservaPrivateDTO>>();
            }
            return View();
        }


        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservaID,ParqueID,LugarID,ClienteID,DataInicio,DataFim,DataReserva,")] ReservaPrivateDTO reserva)
        {
            if (ModelState.IsValid)
            {
                if (reserva.DataInicio >= reserva.DataFim || reserva.DataInicio < DateTime.Now)
                {
                    throw new Exception("Data inválida");
                }
                using (HttpClient client = new())
                {
                    StringContent content = new(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/reservas";
                    var response = await client.PostAsync(endpoint, content);
                }
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
            ReservaPrivateDTO reserva;
            using (HttpClient client = new())
            {
                string endpoint = apiBaseUrl + "/ReservasCentral/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reserva = await response.Content.ReadAsAsync<ReservaPrivateDTO>();
            }
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }


        // POST: Reservas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ReservaID,ParqueID,LugarID,ClienteID,DataInicio,DataFim,ParaSubAluguer")] ReservaPrivateDTO reserva)
        {
            if (id != reserva.ReservaID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (HttpClient client = new())
                {
                    StringContent content = new(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/ReservasCentral/sub/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }


        //GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReservaPrivateDTO reserva;
            using (HttpClient client = new())
            {
                string endpoint = apiBaseUrl + "/ReservasCentral/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                reserva = await response.Content.ReadAsAsync<ReservaPrivateDTO>();
            }
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }


        // DELETE: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (HttpClient client = new())
            {
                string endpoint = apiBaseUrl + "/Cancelar/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
