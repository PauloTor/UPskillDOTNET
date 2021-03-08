using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PseudoFront_.ViewModel;
using PseudoFront_.Models;
using System.Net.Http;
using Newtonsoft.Json;
using PseudoFront_.DTO;
using System.Security.Claims;

namespace PseudoFront_.Controllers
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

        public async Task<IActionResult> Index(string myCountry)
        {
            ViewData["CurrentFilter"] = myCountry;



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


                var vmDemo = new List<ViewModel1>();
                foreach (var item in a)
                {
                    vmDemo.Add(new ViewModel1
                    {
                        CodigoPostal = item.codPostal,
                        ParqueID = item.parqueID,
                        NomeParque = item.nomeParque,
                        NIFParque = item.nifParque,
                        Lotacao = item.Lotacao,
                        Url = item.url,
                        MoradaID = item.moradaid,
                        Rua = item.rua
                    });

                }
                // string[] va;
                var i = 0;
                String[] str = new String[vmDemo.Count];
                foreach (var item in vmDemo)
                {
                    str[i] = (item.CodigoPostal + " " + item.Rua + " " + item.NomeParque);

                    if ((myCountry != null) && (myCountry == str[i]))
                    {
                        // var action = item.ParqueID;
                        return RedirectToAction("ParquesDetails", new { id = item.ParqueID });
                    }
                    i++;
                }

                //   SalesContext sc = new SalesContext();

                string json = JsonConvert.SerializeObject(str);
                //ViewData["chart"] = json;
                ViewBag.Sales = json;

                return View(vmDemo);

            }

        }


        public IActionResult ParquesDetails(int id, DateTime datai, DateTime dataf)
        {
            ViewData["Dataini"] = datai;
            ViewData["Dataini"] = dataf;

            if (datai == DateTime.MinValue && dataf != DateTime.MinValue)
            {
                // var action = item.ParqueID;

                return RedirectToAction("CriarReserva", new { i = id, di = datai, df = dataf });

            }

            ParqueDTO parque;

            using (HttpClient client = new HttpClient())
            {
                string endpoint = "https://localhost:44353/api/Parques/" + id;
                var response = client.GetAsync(endpoint);
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<ParqueDTO>();
                    read.Wait();
                    parque = read.Result;
                }
                else
                {
                    //erro
                    parque = null;
                    ModelState.AddModelError(string.Empty, "Server error occured");
                }
                return View(parque);
            }
        }


        public IActionResult CriarReserva(int parqueid, DateTime datai, DateTime dataf)
        {

            ReservaPrivateDTO reservaPrivateDTO;
            var userId = User.Identity;


            //using (HttpClient client = new HttpClient())
            //{
            //    string endpoint = "https://localhost:44353/api/Parques/" + id;
            //    var response = client.GetAsync(endpoint);
            //    response.Wait();
            //    var result = response.Result;

            //    if (result.IsSuccessStatusCode)
            //    {
            //        var read = result.Content.ReadAsAsync<ParqueDTO>();
            //        read.Wait();
            //        parque = read.Result;
            //    }
            //    else
            //    {
            //        //erro
            //        parque = null;
            //        ModelState.AddModelError(string.Empty, "Server error occured");
            //    }
            return View();
        }

    }

}


        //    // GET: ParquesController/Create
        //    public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ParquesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ParquesController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ParquesController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ParquesController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ParquesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
