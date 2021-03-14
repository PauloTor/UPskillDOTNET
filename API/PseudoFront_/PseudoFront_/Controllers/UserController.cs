using PseudoFront_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoFront_.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        // Construtor do controller
        public UserController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
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
               // https://localhost:44346/api/user/token
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

        // GET: Obter informação de todos os Usuários
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var listaUsers = new List<ApplicationUser>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Users";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaUsers = await response.Content.ReadAsAsync<List<ApplicationUser>>();
            }
            IQueryable<ApplicationUser> users = (from c in listaUsers select c).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(c => c.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "email_desc":
                    users = users.OrderByDescending(c => c.Email);
                    break;
                default:
                    users = users.OrderBy(c => c.FirstName);
                    break;
            }
            int pageSize = 10;
            return View(PaginatedList<ApplicationUser>.CreateAsync(users, pageNumber ?? 1, pageSize));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id) // ?????
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser applicationUser;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Users/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                applicationUser = await response.Content.ReadAsAsync<ApplicationUser>();
            }
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        //GET: Users/Delete/5
        //[Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser applicationUser;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Users/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                applicationUser = await response.Content.ReadAsAsync<ApplicationUser>();
            }
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        //POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/Users/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
