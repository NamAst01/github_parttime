using BusinessObject.Models;
using DataProvider.Requests.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //with status job = 0
        [HttpGet("getAllJobDetail")]
        public IActionResult getAllJobDetail()
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var getListJobDetail = context.JobDetails
                    .Where(x=>x.Status == 0 || x.Status == 1 || x.Status == 2 || x.Status == 4).OrderByDescending(x=>x.Id).ToList();
                return Ok(getListJobDetail);
            }
        }
        [HttpPost("approve")]
        public IActionResult ApproveProducts([FromBody] List<int> jobIds)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    List<JobDetail> existingJobDetails = context.JobDetails.Where(x => jobIds.Contains(x.Id)).ToList();
                    foreach (var existingJobDetail in existingJobDetails)
                    {
                        existingJobDetail.Status = 1;
                        context.JobDetails.Update(existingJobDetail);
                    }

                    context.SaveChanges();
                }

                return Ok(new { Message = "Các công việc đã được duyệt" });
            }
            catch (Exception ex)
            {
                return BadRequest("Đã xảy ra lỗi trong quá trình duyệt công việc: " + ex.Message);
            }
        }
        [HttpPut("createReject")]
        public IActionResult createReason(ReasonCacelDTO job)
        {
            using(ParttimeJobContext con = new ParttimeJobContext())
            {
              //  var p = con.JobDetails.Find(job.Id);
                List<JobDetail> p = con.JobDetails.Where(x => job.Id.Contains(x.Id)).ToList();
                foreach (var existingJobDetail in p)
                {
                    existingJobDetail.Reasonreject = job.Reasonreject;
                    existingJobDetail.Status = 2;
                    con.JobDetails.Update(existingJobDetail);
                }
                con.SaveChanges();
                return Ok(new { Message = "Da luu ly do tu choi!" });
            }
        }
        [HttpPut("closejob")]
        public IActionResult closeJobDetail(ReasonCacelDTO job)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                //  var p = con.JobDetails.Find(job.Id);
                List<JobDetail> p = con.JobDetails.Where(x => job.Id.Contains(x.Id)).ToList();
                foreach (var existingJobDetail in p)
                {
                    existingJobDetail.Reasonreject = job.Reasonreject;
                    existingJobDetail.Status = 4;
                    con.JobDetails.Update(existingJobDetail);
                }
                con.SaveChanges();
                return Ok(new { Message = "Da luu ly do xoa!" });
            }
        }
        [HttpGet("getallcanaccount")]
        public IActionResult getallAccount()
        {
            using(ParttimeJobContext ctx = new ParttimeJobContext())
            {
                var resultac = ctx.Accounts.Where(x => x.Status == 0 || x.Status == 1 || x.Status == 2).ToList();
                return Ok(resultac);
            }
        }
        // update status khi xoa job cua admin
        [HttpPut("updatestausxoa")]
        public IActionResult updatestatusjob4(int? idjob)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                JobDetail jobDetail = context.JobDetails.FirstOrDefault(x => x.Id == idjob);
                if (jobDetail != null)
                {
                    jobDetail.Status = 4;
                    context.JobDetails.Update(jobDetail);
                    context.SaveChanges();
                    return Ok(new { Message = "Job da ko dc duyệt" });
                }
                return BadRequest();
            }
        }
        // block account 
        [HttpPut("upadateaccountstatus1")]
        public IActionResult updatestatus1([FromBody] AccountDTO idaccount)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                List<Account> existingJobDetails = context.Accounts.Where(x => idaccount.Id.Contains(x.Id)).ToList();
                foreach (var acc in existingJobDetails)
                {
                    acc.Status = 1;
                    acc.ReasonBaned = idaccount.ReasonBaned;
                    context.Accounts.Update(acc);
                }
                context.SaveChanges();
            }

            return Ok(new { Message = "da block account" });
        
        }
        // unblock account  
        [HttpPut("upadateaccountstatus0")]
        public IActionResult updatestatus0([FromBody] AccountDTO idaccount)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                List<Account> existingJobDetails = context.Accounts.Where(x => idaccount.Id.Contains(x.Id)).ToList();
                foreach (var acc in existingJobDetails)
                {
                    acc.Status = 0;
                    acc.ReasonBaned = idaccount.ReasonBaned;
                    context.Accounts.Update(acc);
                }
                context.SaveChanges();
            }

            return Ok(new { Message = "da unblock account" });
         
        }
        // xoa account  
        [HttpPut("upadateaccountstatus2")]
        public IActionResult updatestatus2(AccountDTO idaccount)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                List<Account> existingJobDetails = context.Accounts.Where(x => idaccount.Id.Contains(x.Id)).ToList();
                foreach (var acc in existingJobDetails)
                {
                    acc.Status = 2;
                    acc.ReasonBaned = idaccount.ReasonBaned;
                    context.Accounts.Update(acc);
                }
                context.SaveChanges();
            }

            return Ok(new { Message = "da xoa account" });
        }
    }
}
