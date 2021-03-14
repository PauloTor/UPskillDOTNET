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
using System.Security.Claims;
using System.Text;
using System.Globalization;

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
                string endpoint = "https://localhost:44346/api/Parques";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaParques = await response.Content.ReadAsAsync<List<ParqueDTO>>();
                string endpoint2 = "https://localhost:44346/api/Moradas";
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
            // ViewData["Datainicio"] = datai;
            // ViewData["Datafim"] = dataf;
            ViewData["Dataerror"] = "";
            if (dataf != DateTime.MinValue)
            {
                if (dataf < datai || datai < DateTime.Now)
                {
                    ViewData["Dataerror"] = "Datas Inválidas";
                }
                else { 
                // var action = item.ParqueID;

                return RedirectToAction("CriarReserva", new { di = datai, df = dataf, i = id });
                      }
            }
            ParqueDTO parque;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = "https://localhost:44346/api/Parques/" + id;
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

        public IActionResult CriarReserva(DateTime di, DateTime df, long i)
        {

            //ReservaPrivateDTO reservaPrivateDTO;
            var email = Request.Cookies["email"];
            
            var dr = DateTime.Now;
            var reserva = new ReservaPrivateDTO_(dr,di, df, email, i,1);
            //            [HttpGet("post/{DataInicio}/{DataFim}/{EmailID}/{ParqueID}")]
            // string queryString = "DataInicio=" + datai + "&DataFim=" + dataf + "&EmailID=" + email + "&ParqueID=" + i;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44346/api/");
                client.DefaultRequestHeaders.Clear();
                // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync("criarreserva", reserva);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    content.Wait();
                    TempData["message"] = "Reserva efetuada com sucesso";
                }

                //long a = 1;
                //using (HttpClient client = new HttpClient())
                //{
                //    var response = client.PostAsJsonAsync("https://localhost:44346/api/criarreserva", reserva).Result;

                return RedirectToAction("Index", "Home", new { });

                //return View(reserva);
            }

        }

    }
}