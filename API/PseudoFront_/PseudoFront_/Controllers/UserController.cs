using PseudoFront_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace PseudoFront_.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {
            var token = Request.Cookies["token"];
     
            if (token == null)
                ViewBag.Token = false;
            else
                ViewBag.Token = true;
            return View(new RegisterModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            using (var client = new HttpClient())
            {
                //StringContent reserva_ = new StringContent(JsonConvert.
                // SerializeObject(registerModel), Encoding.UTF8, "application/json");
                //var postTask =  client.
                //    PostAsync("https://localhost:44353/api/register", reserva_);


                client.BaseAddress = new Uri("https://localhost:44346/api/user/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync("register", registerModel);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    content.Wait();
                    TempData["message"] = "New user registered";
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(registerModel);
        }

        public IActionResult Login()
        {
            var token = Request.Cookies["token"];
            
            if (token == null)
                ViewBag.Token = false;
            else
                ViewBag.Token = true;
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:44346/api/");
                    client.DefaultRequestHeaders.Clear();
                   // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var postTask = client.PostAsJsonAsync("user/token", loginModel);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var content = result.Content.ReadFromJsonAsync<AuthenticationModel>();
                        content.Wait();
                        if (content.Result.Token != null)
                        {
                            CookieOptions cookieOptions = new CookieOptions();
                            cookieOptions.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Append("token", content.Result.Token, cookieOptions);
                            Response.Cookies.Append("email", content.Result.Email, cookieOptions);
             
                            //  Response.Cookies.Append("role", content.Result.Roles., cookieOptions);
                            TempData["message"] = "Login efectuado com sucesso";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["message"] = "Login falhado";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                } catch
                {
                    TempData["message"] = "Erro na Ligacao ao Servidor";
                    return RedirectToAction("Index", "Home");
                }
            }
           
            TempData["message"] = "erro.";
            return View(loginModel);
        }

        public IActionResult Logout()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Append("token", "", cookieOptions);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            
            return View();
        }
    }
}
