using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PseudoCompanyFront.Data;
using PseudoCompanyFront.Models;

namespace PseudoCompanyFront.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        // Construtor do controller
        public ClientesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
        // GET: Obter informação de todos os Clientes
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var listaClientes = new List<Cliente>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Clientes";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaClientes = await response.Content.ReadAsAsync<List<Cliente>>();
            }
            return View(listaClientes);
        }
        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cliente cliente;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/cliente/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                cliente = await response.Content.ReadAsAsync<Cliente>();
            }
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
    }
}

