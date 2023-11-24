using BusinessObject.Models;
using DataProvider.Handler.Authentication;
using DataProvider.Handler.Feedbacks;
using DataProvider.Requests.Authentication;
using DataProvider.Requests.Feedbacks;
using DataProvider.Responses.Authentication;
using DataProvider.Responses.Feedbacks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var feedback = context.Comments.ToList();
                return Ok(feedback);
            }
        }
        [HttpPost("CreateFeedbackToEmployer")]
        public IActionResult CreateFeedbackE(SentFeedbackRequest request)
        {
            SentFeedbackHandler loginHandler = new SentFeedbackHandler();
            SentFeedbackResponse response = loginHandler.handler(request);
            return Ok(response);
        }
        [HttpPost("CreateFeedbackToCandidate")]
        public IActionResult CreateFeedbackC(SentFeedbackRequest request)
        {
            SentFeedbackToCandateHandler loginHandler = new SentFeedbackToCandateHandler();
            SentFeedbackResponse response = loginHandler.handler(request);
            return Ok(response);
        }
        [HttpGet("GetFeedbackForCandidate")]
        public IActionResult GetNotiForCandidate(int aid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var acid = context.Candidates.FirstOrDefault(a => a.Id == aid).AccountId;
                if (acid == null)
                {
                    return Ok(new { Message = "aid truyền vào không đúng" });
                }
                var noti = context.Comments.Include(a => a.Account).Where(n => n.AccountId == acid).ToList();
                return Ok(noti);
            }
        }
        [HttpGet("GetFeedbackForEmployer")]
        public IActionResult GetNotiForEmployer(int aid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var acid = context.Employers.FirstOrDefault(a => a.Id == aid).AccountId;
                if (acid == null)
                {
                    return Ok(new { Message = "aid truyền vào không đúng" });
                }
                var noti = context.Comments.Include(a=>a.Account).Where(n => n.AccountId == acid).ToList();
                return Ok(noti);
            }
        }
    }
}
