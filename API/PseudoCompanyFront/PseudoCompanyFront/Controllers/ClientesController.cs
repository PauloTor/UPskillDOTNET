using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cliente cliente;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Clientes/" + id;
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

        // CREATE GET

        public async Task<IActionResult> Create()
        {
            var listaClientes = new List<Cliente>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Clientes";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaClientes = await response.Content.ReadAsAsync<List<Cliente>>();
            }
            return View();
        }
    }
}

        //CREATE POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Funcionario,Administrador")]

        //public async Task<IActionResult> Create([Bind("ClienteID,NomeCliente,EmailCliente,NifCliente,MetodoPagamento,Credito")] Cliente cliente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            //UserInfo user = new UserInfo();
        //            //StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        //            //var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
        //            //UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
        //            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        //            StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
        //            string endpoint = apiBaseUrl + "/Cliente";
        //            var response = await client.PostAsync(endpoint, content);
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(cliente);
//        }
//    }
//}

