using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PseudoFront_.Models;

namespace PseudoFront_.Controllers
{
    public class FaturasController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        public FaturasController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: Faturas
        public async Task<ActionResult> Index()
        {
            using HttpClient client = new();
            string endpoint = apiBaseUrl + "/Faturas";
            var response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var readTask = response.Content.ReadAsAsync<IEnumerable<Fatura>>();
                readTask.Wait();

                IEnumerable<Fatura> sub = readTask.Result;

                return View(sub);
            }
            else
            {
                return BadRequest("Server error. Please contact administrator.");
            }
        }

        // GET: Faturas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Fatura fatura;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Faturas/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                fatura = await response.Content.ReadAsAsync<Fatura>();
            }
            if (fatura == null)
            {
                return NotFound();
            }
            return View(fatura);
        }
    }
}
