//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using PseudoCompanyFront.Data;
//using PseudoCompanyFront.Models;

//namespace PseudoCompanyFront.Controllers
//{
//    public class ClientesController : Controller
//    {
//        private readonly IConfiguration _configure;
//        private readonly string apiBaseUrl;

//        // Construtor do controller
//        public ClientesController(IConfiguration configuration)
//        {
//            _configure = configuration;
//            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
//        }
//        // GET: api/Clientes : Obter Informação dos Clientes
//        [Authorize(Policy = "Admin")]
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Cliente>>> Get_Clientes()
//        {
//            using (HttpClient client = new HttpClient())
//            {
//                string endpoint = apiBaseUrl + "/Clientes";
//                var response = await client.GetAsync(endpoint);
//                response.EnsureSuccessStatusCode();
//                Clientes = await response.Content.ReadAsAsync<Cliente>();
//                return Clientes;
//            }


//        }

//    }
//}
