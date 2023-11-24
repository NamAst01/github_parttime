using BusinessObject.Models;
using DataProvider.Requests.DTO;
using DataProvider.Requests.Jobs;
using DataProvider.Services.Authen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        [HttpGet("GetjobApplycationByCandidateId")]
        public IActionResult GetjobApplycationByCandidateId(int candidateId, int jobId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var account = context.JobApplications.FirstOrDefault(a => a.ApplicantId == candidateId && a.JobId == jobId);
                    if (account == null)
                    {
                        return Ok(new { status = 1 });
                    }
                    return Ok(account);

                }
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpGet("GetjobApplycationByCId")]
        public IActionResult GetjobApplycationByCId(int cid)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var account = context.JobApplications.Where(a => a.ApplicantId == cid).ToList();
                    if (account == null)
                    {
                        return NotFound();
                    }
                    return Ok(account);

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetjobApplycationById")]
        public IActionResult GetjobApplycationById(int id)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var job = context.JobApplications.Select(e => new
                    {
                        id = e.Id,
                    }).Where(a=>a.id == id).ToList();
                    if (job == null)
                    {
                        return NotFound();
                    }
                    return Ok(job);

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CareJob")]
        public IActionResult CareJob(AddJobDTO app)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    JobApplication jobx = context.JobApplications.FirstOrDefault(x => x.ApplicantId == app.ApplicantId && x.JobId == app.JobId);
                    if (jobx ==null)
                    {
                        JobApplication addJob = new JobApplication
                        {
                            JobId = app.JobId,
                            ApplicantId = app.ApplicantId,
                            CreatedId = DateTime.Now,
                            Care = 1,
                            Status = 1,
                        };
                        context.JobApplications.Add(addJob);
                        context.SaveChanges();
                        return Ok(new { Message = "Care" });
                    }
                    else 
                    {
                        jobx.Care = 1;
                        context.JobApplications.Update(jobx);
                        context.SaveChanges();
                        return Ok(new { Message = "Care" });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetJobByStatus")]
        public IActionResult GetJobByStatus(int status,int canId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.JobApplications.Select(e => new
                    {
                        id=e.Job.Id,
                        title=e.Job.Title,
                        jobAppId = e.Id,
                        candidateid=e.ApplicantId,
                        jobTime=e.Job.JobTime,
                        location=e.Job.Location,
                        company=e.Job.Company,
                        care=e.Care,
                        status=e.Status,
                        isComment=e.IsComment,
                        salary=e.Job.Salary,
                        typeSalary = e.Job.TypeSalary
                    }).Where(x=>x.status== status && x.candidateid== canId).ToList();
                    if (jobx != null)
                    {
                        return Ok(jobx);
                    }
                    return Ok(jobx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetJobCare")]
        public IActionResult GetJobCare(int CandidateId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.JobApplications.Select(e => new
                    {
                        id=e.Job.Id,
                        title=e.Job.Title,
                        jobAppId=e.Id,
                        candidateid=e.ApplicantId,
                        jobTime=e.Job.JobTime,
                        location=e.Job.Location,
                        company=e.Job.Company,
                        care=e.Care,
                        status=e.Status,
                        employerId=e.Job.EmployerId,
                        salary=e.Job.Salary,
                        typeSalary=e.Job.TypeSalary
                    }).Where(x=>x.care == 1 && x.candidateid == CandidateId).ToList();
                    if (jobx != null)
                    {
                        return Ok(jobx);
                    }
                    return Ok(jobx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("DeleteJobCare")]
        public IActionResult DeleteJobCare(int jobApplicationId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    JobApplication jobx = context.JobApplications.FirstOrDefault(x => x.Id == jobApplicationId);
                    if (jobx != null)
                    {
                        jobx.Care = 0;
                        context.JobApplications.Update(jobx);
                        context.SaveChanges();
                        return Ok(new { Message = "Delete" });
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("CanceApply")]
        public IActionResult CanceApply(CancelApply request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    if (request.ResonCancel.Trim().Length < 0)
                    {
                        return Ok(new
                        {
                            Message = "Nhập lý do hủy"
                        });
                    }
                    JobApplication jobx = context.JobApplications.FirstOrDefault(x => x.Id == request.JobId);
                    if (jobx != null)
                    {
                        jobx.Status = 1;
                        jobx.ReasonCancel = request.ResonCancel;
                        context.JobApplications.Update(jobx);
                        context.SaveChanges();
                        return Ok(new { Message = "Huy ung tuyen" });
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("Apply")]
        public IActionResult Apply(int jobApplicationId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    JobApplication jobx = context.JobApplications.FirstOrDefault(x => x.Id == jobApplicationId);
                    if (jobx != null)
                    {
                        jobx.Status = 0;
                        jobx.Care = 0;
                        context.JobApplications.Update(jobx);
                        context.SaveChanges();
                        return Ok(new { Message = "Ung tuyen" });
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
