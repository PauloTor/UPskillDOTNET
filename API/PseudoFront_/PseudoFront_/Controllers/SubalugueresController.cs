using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PseudoFront_.Models;

namespace PseudoFront_.Controllers
{
    public class SubalugueresController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        public SubalugueresController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: SubAlugueres
        public async Task<ActionResult> Index()
        {
            using HttpClient client = new();
            string endpoint = apiBaseUrl + "/SubAlugueres";
            var response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var readTask = response.Content.ReadAsAsync<IEnumerable<Subaluguer>>();
                readTask.Wait();

                IEnumerable<Subaluguer> sub = readTask.Result;

                return View(sub);
            }
            else
            {
                return BadRequest("Server error. Please contact administrator.");
            }
        }

        // GET: SubAlugueres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using HttpClient client = new();
            string endpoint = apiBaseUrl + "/SubAlugueres/" + id;
            var response = await client.GetAsync(endpoint);
            var sub = await response.Content.ReadAsAsync<Subaluguer>();

            if (sub == null)
            {
                return NotFound();
            }

            return View(sub);
        }


        // GET: SubAlugueres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Subaluguer sub;
            using (HttpClient client = new())
            {
                string endpoint = apiBaseUrl + "/SubAlugueres/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                sub = await response.Content.ReadAsAsync<Subaluguer>();
            }
            if (sub == null)
            {
                return NotFound();
            }
            return View(sub);
        }


        // POST: SubAlugueres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SubAluguerID,Preco,Reservado,NovoCliente,ReservaID")] Subaluguer sub)
        {
            if (id != sub.SubAluguerID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (HttpClient client = new())
                {
                    StringContent content = new(JsonConvert.SerializeObject(sub), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/SubAlugueres/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sub);
        }

        
        // GET: SubAlugueres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Subaluguer sub;
            using (HttpClient client = new())
            {
                string endpoint = apiBaseUrl + "/SubAlugueres/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                sub = await response.Content.ReadAsAsync<Subaluguer>();
            }
            if (sub == null)
            {
                return NotFound();
            }
            return View(sub);
        }


        // POST: SubAlugueres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (HttpClient client = new())
            {
                string endpoint = apiBaseUrl + "/Subalugueres/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
