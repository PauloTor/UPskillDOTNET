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

        // GET: SubAlugueres
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
    }
}
