using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParqueAPICentral.Models;
using ParqueAPICentral.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParqueAPICentral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {

            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            if (result.RefreshToken != null)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            return Ok(result);
        }
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }


        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _userService.RevokeToken(token);

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }


        public ApplicationUser UpdatePagamentoCliente(string clienteID, float valor)
        {

            return _userService.UpdatePagamentoCliente(clienteID, valor);
        }

        [Authorize]
        [HttpPost("getidbymail/{email}")]
        public string GetIdbyEmail(string email)
        {

            return _userService.GetIdByEmail(email);
        }


        [Authorize]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = _userService.GetById(id);
            return Ok(user.RefreshTokens);
        }

        // GET: api/User : Obter Informação dos Users
        //[Authorize(Policy = "Admin")]
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        {
            return await this._userService.GetAllUsers();
        }

        // PUT: api/User/ - Actualizar informação de um User
        //[Authorize(Policy = "Admin")]
        //[HttpPut("{id}")]
        //public async Task<ActionResult<ApplicationUser>> UpdateUserById(ApplicationUser applicationuser)
        //{
        //    return await this._userService.UpdateUser(applicationuser);
        //}

        // DELETE: api/User/5 - Eliminar um User
        //[Authorize(Policy = "Admin")]
        //[HttpDelete("delete/{id}")] 
        //public async Task<ActionResult<ApplicationUser>> DeleteUser(string id)
        //{
        //    return await this._userService.DeleteUser(id);
        //}
    }
}