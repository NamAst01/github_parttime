using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataProvider.Handler.Authentication;
using DataProvider.Requests.Authentication;
using DataProvider.Responses.Authentication;
using BusinessObject.Models;
using System.Security.Principal;
using System.Net.NetworkInformation;
using DataProvider.Services.Authen;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using DataProvider.Handler.Accounts;
using DataProvider.Requests;
using DataProvider.Responses.Accounts;
using DataProvider.Requests.Accounts;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller 
    {
        private static ParttimeJobContext context = new ParttimeJobContext();
        private static IAuthentication authentication = new IAuthentication();
        [HttpPost("LoginWithAccount")]
        public IActionResult LoginWithAccount(LoginRequest request)
        {
            LoginHandler loginHandler = new LoginHandler();
            LoginResponse response = loginHandler.handler(request);
            return Ok(response);
        }
        [HttpGet("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
        [HttpPost("RegisterGoogle")]
        public async Task<IActionResult> RegisterGoogle(RegisterGoogleRequest request)
        {
            RegisterGooglehandler handler = new RegisterGooglehandler();
            LoginResponse response = handler.handler(request);
            return Ok(response);
        }
        [HttpGet("GetAccountByToken")]
        public async Task<IActionResult> GetAccountByToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenRead = tokenHandler.ReadJwtToken(token);
                var claims = tokenRead.Claims;

                var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
                var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (idClaim != null  && nameClaim != null)
                {
                    if (int.TryParse(idClaim.Value, out int id))
                    {
                        var account = authentication.getAccountById(id);
                        return Ok(account);
                    }
                }
            }

            return BadRequest("Không tìm thấy thông tin tài khoản hoặc thông tin không hợp lệ.");
        }

    }
}