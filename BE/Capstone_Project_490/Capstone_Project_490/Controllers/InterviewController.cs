using BusinessObject.Models;
using DataProvider.Handler.Accounts;
using DataProvider.Handler.Interviews;
using DataProvider.Requests;
using DataProvider.Requests.Interview;
using DataProvider.Responses.Accounts;
using DataProvider.Responses.Interview;
using DataProvider.Services.Authen;
using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var account = context.Interviews.ToList();
                return Ok(account);
            }
                
        }
        [HttpPost("CreateInterview")]
        public IActionResult CreateAccount(InterviewRequest request)
        {
            CreateInterviewHandler handler = new CreateInterviewHandler();
            InterviewResponse response = handler.handler(request);
            return Ok(response);
        }
        [HttpGet("GetInterViewByEmpId")]
        public IActionResult GetInterViewByEmpId(int aId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var Interview = context.Interviews.Where(x => x.Employer.Account.Id == aId).ToList();
                    if (Interview == null || Interview.Count == 0)
                    {
                        var account = context.Accounts.FirstOrDefault(a => a.Id == aId);
                        var emp = context.Accounts.FirstOrDefault(a => a.Email == account.Email && a.RoleId == 1);
                        var Interview1 = context.Interviews.Where(x => x.Employer.Account.Id == emp.Id).ToList();
                        return Ok(Interview1);
                    }
                    return Ok(Interview);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCandidateByInterviewId")]
        public IActionResult GetCandidateByInterviewId(int InterviewId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {

                    var candidate = context.JobApplications.Select(e => new
                    {
                        id = e.Id,
                        fullname = e.Applicant.Account.FullName,
                        dob = e.Applicant.Dob.ToString("yyyy-MM-dd"),
                        gender = e.Applicant.Account.Gender,
                        phone = e.Applicant.Phone,
                        email = e.Applicant.Account.Email,
                        address= e.Applicant.City+" "+ e.Applicant.Distric +" "+ e.Applicant.Address,
                        interview=e.InterviewId,
                        status=e.Status
                    }).Where(a => a.interview == InterviewId).ToList();
                    if (candidate == null)
                    {
                        return NotFound();
                    }
                    return Ok(candidate);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("SetinterviewCandidate")]
        public IActionResult SetinterviewCandidate(int cId, int InterviewId, int jobId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var candidate = context.Candidates.FirstOrDefault(c=>c.Id== cId);
                    if (candidate == null)
                    {
                        return NotFound();
                    }
                    var job = context.JobApplications.FirstOrDefault(c => c.JobId == jobId && c.ApplicantId == cId);
                    job.Status = 5;
                    job.InterviewId= InterviewId;
                    context.JobApplications.Update(job);
                    context.SaveChanges();
                    return Ok(candidate);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("EditLich")]
        public IActionResult EditLich(InterviewRequest request)
        {
            EditLichHandler handler = new EditLichHandler();
            InterviewResponse response = handler.handler(request);
            return Ok(response);
        }
        [HttpPut("DeleteCandidateInterview")]
        public IActionResult DeleteCandidateInterview(int cid)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var candidate = context.JobApplications.FirstOrDefault(c => c.Id == cid);
                    if (candidate == null)
                    {
                        return NotFound();
                    }
                    candidate.InterviewId = null;
                    candidate.Status = 4;
                    context.JobApplications.Update(candidate);
                    context.SaveChanges();
                    return Ok(candidate);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SelectOptionInterviewByEmp")]
        public IActionResult SelectOptionInterviewByEmp(int eId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var candidate = context.Interviews.Where(c => c.EmployerId == eId).ToList();
                    if (candidate == null)
                    {
                        return NotFound();
                    }
                    return Ok(candidate);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetListCandidateInterviewByEid")]
        public IActionResult GetListCandidateInterviewByEid(int eId,int status)
        {
            try
            {
                List<List<JobApplication>> list = new List<List<JobApplication>>();
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobOfEmployer = context.JobDetails.Where(x => x.EmployerId == eId).ToList();
                    if (jobOfEmployer == null)
                    {
                        return NotFound();
                    }
                    foreach (var job in jobOfEmployer)
                    {
                        List<JobApplication> jobApplications = context.JobApplications.Where(a => a.JobId == job.Id && a.Status==status).ToList();
                        foreach (var application in jobApplications)
                        {
                            Candidate can = context.Candidates.FirstOrDefault(a => a.Id == application.ApplicantId);
                            Account account = context.Accounts.FirstOrDefault(a => a.Id == can.AccountId);
                            can.Account = account;
                            application.Applicant = can;
                        }
                        list.Add(jobApplications);
                    }
                    return Ok(list);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetInterviewOfcandidate")]
        public IActionResult GetInterviewOfcandidate(int cid)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {

                    var candidate = context.JobApplications.Select(e => new
                    {
                        title = e.Job.Title,
                        nameShop = e.Job.Company,
                        start = e.Interview.Start,
                        end = e.Interview.End,
                        date = e.Interview.Date,
                        address = e.Job.Location,
                        jobApplicationId = e.Id,
                        candidateId=e.ApplicantId,
                        status=e.Applicant.Status
                    }).Where(a => a.candidateId == cid && a.status == 5).ToList();
                    if (candidate == null)
                    {
                        return NotFound();
                    }
                    return Ok(candidate);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("CandidateCancelInterview")]
        public IActionResult CandidateCancelInterview(int jobApplicationId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobApplication = context.JobApplications.FirstOrDefault(c => c.Id == jobApplicationId);
                    if (jobApplication == null)
                    {
                        return NotFound();
                    }
                    jobApplication.Status = 6;
                    context.JobApplications.Update(jobApplication);
                    context.SaveChanges();
                    return Ok(jobApplication);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
