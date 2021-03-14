using PseudoFront_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PseudoFront_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            var token = Request.Cookies["token"];
            ViewBag.username= Request.Cookies["username"];
            
            //var user_=Request.Cookies[User]
            ViewBag.Token = true;
            if (token == null)
            {
                ViewBag.Token = false;
                TempData["role"] = null;
            }
            else
            {
                

                // GetUserRole
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:44346/api/");
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        //var postTask = client.GetAsync("user/role");
                        //postTask.Wait();
                        //var result = postTask.Result;
                        if (Request.Cookies["role"]!=null)
                        {
                          //  var content = result.Content.ReadAsStringAsync();
                           // content.Wait();
                         //   var role = content.Result;
                            TempData["role"] = Request.Cookies["role"];
                        }
                        else
                            TempData["role"] = null;
                    }
                    catch
                    {
                        TempData["role"] = null;
                    }
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
