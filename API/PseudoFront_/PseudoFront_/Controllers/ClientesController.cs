using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using PseudoFront_.Models;


namespace PseudoFront_.Controllers
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
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NomeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var listaClientes = new List<Cliente>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Clientes";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaClientes = await response.Content.ReadAsAsync<List<Cliente>>();
            }
            IQueryable<Cliente> clientes = (from c in listaClientes select c).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(c => c.NomeCliente.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nome_desc":
                    clientes = clientes.OrderByDescending(c => c.NomeCliente);
                    break;
                default:
                    clientes = clientes.OrderBy(c => c.NomeCliente);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Cliente>.CreateAsync(clientes, pageNumber ?? 1, pageSize));
        }


    }
}


















        //// GET: Clientes/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Cliente cliente;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string endpoint = apiBaseUrl + "/Clientes/" + id;
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        cliente = await response.Content.ReadAsAsync<Cliente>();
        //    }
        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(cliente);
        //}

        ////GET: Clientes/Create

        //public async Task<IActionResult> Create()
        //{
        //    var listaClientes = new List<Cliente>();
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string endpoint = apiBaseUrl + "/Clientes";
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        listaClientes = await response.Content.ReadAsAsync<List<Cliente>>();
        //    }
        //    return View();
        //}

        ////POST Clientes/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Funcionario,Administrador")]

        //public async Task<IActionResult> Create([Bind("ClienteID,NomeCliente,EmailCliente,NifCliente,MetodoPagamento,Credito")] Cliente cliente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
        //            string endpoint = apiBaseUrl + "/Clientes";
        //            var response = await client.PostAsync(endpoint, content);
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(cliente);
        //}

        ////GET: Clientes/Delete/5
        ////[Authorize(Roles = "Funcionario,Administrador")]
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Cliente cliente;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string endpoint = apiBaseUrl + "/Clientes/" + id;
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        cliente = await response.Content.ReadAsAsync<Cliente>();
        //    }
        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(cliente);
        //}

        ////POST: Clientes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Funcionario,Administrador")]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string endpoint = apiBaseUrl + "/Clientes/" + id;
        //        var response = await client.DeleteAsync(endpoint);
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        ////EDIT GET
        ////[Authorize(Roles = "Funcionario,Administrador")]
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Cliente cliente;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        //UserInfo user = new UserInfo();
        //        //StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        //        //var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
        //        //UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
        //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        //        string endpoint = apiBaseUrl + "/Clientes/" + id;
        //        var response = await client.GetAsync(endpoint);
        //        response.EnsureSuccessStatusCode();
        //        cliente = await response.Content.ReadAsAsync<Cliente>();
        //    }
        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(cliente);
        //}

        ////EDIT POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Funcionario,Administrador")]
        //public async Task<IActionResult> Edit(long id, [Bind("ClienteID,NomeCliente,EmailCliente,NifCliente,MetodoPagamento,Credito")] Cliente cliente)
        //{
        //    if (id != cliente.ClienteID)
        //    {
        //        return NotFound();
        //    }
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
        //            string endpoint = apiBaseUrl + "/Clientes/" + id;
        //            var response = await client.PutAsync(endpoint, content);
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(cliente);
        //}
    }
    }
}

