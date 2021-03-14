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
            if (sub.Reservado == false)
            { 
                using HttpClient client1 = new();
                string endpoint1 = apiBaseUrl + "/SubAlugueres/" + id;
                var response1 = await client1.GetAsync(endpoint1);
                var suba = await response1.Content.ReadAsAsync<Subaluguer>();

                sub.SubAluguerID = suba.SubAluguerID;
                sub.ReservaID = suba.ReservaID;
                sub.Reservado = suba.Reservado;
                sub.NovoCliente = suba.NovoCliente;

                using HttpClient client = new();
                StringContent content = new(JsonConvert.SerializeObject(sub), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/SubAlugueres/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            else
                throw new Exception("O subaluguer já se encontra reservado e não pode ser modificado.");

            return RedirectToAction(nameof(Index));
        }


        // GET: SubAlugueres/Reservar/5
        public async Task<IActionResult> Reservar(long? id)
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


        // POST: SubAlugueres/Reservar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservar(long id, [Bind("SubAluguerID,Preco,Reservado,NovoCliente,ReservaID")] Subaluguer sub)
        {
            var email = Request.Cookies["email"];
            using HttpClient client2 = new();
            string endpoint2 = apiBaseUrl + "/user/getidbymail/" + email;
            var response2 = await client2.GetAsync(endpoint2);
            var userId = await response2.Content.ReadAsStringAsync();

            using HttpClient client1 = new();
            string endpoint1 = apiBaseUrl + "/SubAlugueres/" + id;
            var response1 = await client1.GetAsync(endpoint1);
            var suba = await response1.Content.ReadAsAsync<Subaluguer>();

            sub.SubAluguerID = suba.SubAluguerID;
            sub.ReservaID = suba.ReservaID;
            sub.Preco = suba.Preco;
            sub.NovoCliente = userId;

            using HttpClient client = new();
            StringContent content = new(JsonConvert.SerializeObject(sub), Encoding.UTF8, "application/json");
            string endpoint = apiBaseUrl + "/SubAlugueres/post/";
            var response = await client.PostAsync(endpoint, content);

            return RedirectToAction(nameof(Index));
        }
    }
}
