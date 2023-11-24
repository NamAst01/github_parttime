using BusinessObject.Models;
using DataProvider.Handler.Feedbacks;
using DataProvider.Handler.Notifications;
using DataProvider.Requests.Feedbacks;
using DataProvider.Requests.Notifications;
using DataProvider.Responses.Feedbacks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var feedback = context.Notifications.ToList();
                return Ok(feedback);
            }
        }
        [HttpGet("GetNotiForCandidate")]
        public IActionResult GetNotiForCandidate(int aid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var email=context.Accounts.FirstOrDefault(a=>a.Id==aid).Email;
                if (email == null)
                {
                    return Ok(new {Message="aid truyền vào không đúng"});
                }
                var accountIdCandidate = context.Accounts.FirstOrDefault(a => a.Email.Equals(email) && a.RoleId == 2);
                if (accountIdCandidate == null)
                {
                    var noti = context.Notifications.Where(n => n.AccountId == aid).ToList();
                    return Ok(noti);
                }
                else
                {
                    var noti = context.Notifications.Where(n => n.AccountId == accountIdCandidate.Id).ToList();
                    return Ok(noti);
                }
            }
        }
        [HttpGet("GetNotiForEmployer")]
        public IActionResult GetNotiForEmployer(int aid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var email = context.Accounts.FirstOrDefault(a => a.Id == aid).Email;
                if (email == null)
                {
                    return Ok(new { Message = "aid truyền vào không đúng" });
                }
                var accountIdCandidate = context.Accounts.FirstOrDefault(a => a.Email.Equals(email) && a.RoleId == 1);
                if (accountIdCandidate == null)
                {
                    var noti = context.Notifications.Where(n => n.AccountId == aid).ToList();
                    return Ok(noti);
                }
                else
                {
                    var noti = context.Notifications.Where(n => n.AccountId == accountIdCandidate.Id).ToList();
                    return Ok(noti);
                }
            }
        }
        [HttpPost("CreateNotificationForCandidate")]
        public IActionResult CreateNotificationForCandidate(CreateNotificationRequest request)
        {
            CreateNotiForCandidateHandler loginHandler = new CreateNotiForCandidateHandler();
            SentFeedbackResponse response = loginHandler.handler(request);
            return Ok(response);
        }
        [HttpPost("CreateNotificationForEmployer")]
        public IActionResult CreateNotificationForEmployer(CreateNotificationRequest request)
        {
            CreateNotiForEmployerHandler loginHandler = new CreateNotiForEmployerHandler();
            SentFeedbackResponse response = loginHandler.handler(request);
            return Ok(response);
        }
    }
}
