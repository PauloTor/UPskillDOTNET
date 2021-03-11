using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PseudoCompanyFront.ViewModel;
using PseudoCompanyFront.Models;
using System.Net.Http;

namespace PseudoCompanyFront.Controllers
{


    public class ParquesController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;


        public ParquesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // Construtor do controller

        // GET: Obter informação de todos os Clientes
        public async Task<IActionResult> Index(string searchString)
        {
            // ViewData["CurrentFilter"] = searchString;
           // ViewModel1 vmDemo = new ViewModel1();
            var listaParques = new List<ParqueDTO>();
            var listaMoradas = new List<MoradaDTO>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = "https://localhost:44353/api/Parques";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaParques = await response.Content.ReadAsAsync<List<ParqueDTO>>();
                string endpoint2 = "https://localhost:44353/api/Moradas";
                var response2 = await client.GetAsync(endpoint2);
                response2.EnsureSuccessStatusCode();
                listaMoradas = await response2.Content.ReadAsAsync<List<MoradaDTO>>();
                var a = from e1 in listaMoradas
                            join e2 in listaParques
                                on e1.MoradaID equals e2.MoradaID
                            select new
                            {
                                parqueID = e2.ParqueID,
                                nomeParque = e2.NomeParque,
                                nifParque = e2.NIFParque,
                                Lotacao = e2.Lotacao,
                                url = e2.Url,
                                moradaid = e2.MoradaID,
                                rua = e1.Rua,
                                codPostal = e1.CodigoPostal
                            };
                var Lista = (from N in a
                                where N.parqueID != null
                                select new { N.codPostal });
                ViewBag.Name = Lista.ToList();

                var vmDemo = new List<ViewModel1>();
                foreach (var item in a)
                {
                    vmDemo.Add(new ViewModel1
                    {
                        CodigoPostal =item.codPostal,
                        ParqueID = item.parqueID,
                        NomeParque = item.nomeParque,
                        NIFParque = item.nifParque,
                        Lotacao = item.Lotacao,
                        Url = item.url,
                        MoradaID = item.moradaid,
                        Rua = item.rua
                    });

                }
               
                return View(vmDemo);
            }

        }







        // GET: ParquesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParquesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParquesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParquesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParquesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParquesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParquesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
