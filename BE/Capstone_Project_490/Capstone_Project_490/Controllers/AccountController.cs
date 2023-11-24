using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using DataProvider.Handler.Authentication;
using DataProvider.Requests;
using DataProvider.Responses.Authentication;
using DataProvider.Requests.Accounts;
using DataProvider.Handler.Accounts;
using DataProvider.Responses.Accounts;
using DataProvider.Services.Authen;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private static ParttimeJobContext context = new ParttimeJobContext();
        private static IAuthentication authentication = new IAuthentication();

        [HttpGet("GetAllAccount")]
        public IActionResult GetAllAccount()
        {
            var account = context.Accounts.ToList();
            return Ok(account);
        }
        [HttpGet("getAccountById")]
        public IActionResult getAccountById(int id)
        {
            try
            {
                var data = context.Accounts.FirstOrDefault(a => a.Id == id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(String fullName)
        {
            Account account = new Account();
            account.RoleId = 1;
            account.FullName = fullName;
            account.CreatedAt = DateTime.Now;
            account.LastLoginAt = DateTime.Now;
            account.Status = 1;
            context.Accounts.Add(account);
            context.SaveChanges();
            String accessToken=authentication.Token(account);
            return Ok(new { Token = accessToken });
        }
        [HttpGet("GetAllCandidate")]
        public IActionResult GetAllCandidate()
        {
            var account = context.Candidates.ToList();
            return Ok(account);
        }       
        [HttpGet("GetAllEmployer")]
        public IActionResult GetAllEmployer()
        {
            var account = context.Employers.ToList();
            return Ok(account);
        }
        [HttpPost("RegisterAccountByEmail")]
        public IActionResult RegisterAccountByEmail(RegisterAccountByEmailRequest request)
        {
            RegisterAccountByEmailHandler handler = new RegisterAccountByEmailHandler();
            RegisterAccountByEmailResponse response = handler.handler(request);
            return Ok(response);
        }

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ForgetPasswordRequest request)
        {
            ForgetPasswordHandler handler = new ForgetPasswordHandler();
            ForgetPasswordResponse response = handler.handler(request);
            return Ok(response);
        }
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {
            ChangePasswordHandler handler = new ChangePasswordHandler();
            ChangePasswordResponse response = handler.handler(request);
            return Ok(response);
        }

        [HttpGet("SentCodeVerify")]
        public IActionResult SentCodeVerify(String toEmail)
        {
            try
            {
                String codeVerify = authentication.GenerateCode(8);
                authentication.SentCodeToEmail(toEmail, codeVerify);
                return Ok(new { Message = codeVerify });
            }
            catch(Exception ex)
            {
                return BadRequest("Gui email that bai");
            }
        }
        [HttpPost("CheckRegister")]
        public IActionResult CheckRegister(RegisterAccountByEmailRequest request)
        {
            CheckRegisterHandler handler = new CheckRegisterHandler();
            RegisterAccountByEmailResponse response = handler.handler(request);
            return Ok(response);
        }
        [HttpPost("CheckForgetPassword")]
        public IActionResult CheckForgetPassword(ForgetPasswordRequest request)
        {
            CheckForgetPasswordHandler handler = new CheckForgetPasswordHandler();
            ForgetPasswordResponse response = handler.handler(request);
            return Ok(response);
        }
    }
}
