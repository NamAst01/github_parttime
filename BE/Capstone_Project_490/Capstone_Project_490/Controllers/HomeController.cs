
using BusinessObject.Models;
using DataProvider.Requests.DTO;
using DataProvider.Services.HomePage;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Utilities;



namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private List<JobDetail> lisJob = new List<JobDetail>();

        private IHomePageJob respository = new HomePageJob();
        [HttpGet("getAllJob")]
        public IActionResult GetAllJob()
        {
            try
            {
                var data = respository.getAllJob();
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
        [HttpGet("getJobByID")]
        public IActionResult GetJobById(int id)
        {
            try
            {
                var data = respository.getJobsByID(id);
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
        [HttpGet("getAllCategory")]
        public IActionResult GetALLCategory()
        {
            try
            {
                var data = respository.getAllCateJob();
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
        [HttpGet("getAllJobtype")]
        public IActionResult GetALLJobType()
        {
            try
            {
                var data = respository.getAllJobType();
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
        [HttpGet("GetAllJobByType")]
        public IActionResult GetAllJobByType(int jobid)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var tid=context.JobDetails.FirstOrDefault(x=>x.Id==jobid).JobTypeId;
                    var jobList = context.JobApplications.Select(e => new
                    {
                        jobApplicationId = e.Id,
                        title = e.Job.Title,
                        deadline = e.Job.Deadline,
                        salary = e.Job.Salary,
                        jobTime = e.Job.JobTime,
                        location = e.Job.Location,
                        company = e.Job.Company,
                        care = e.Care,
                        status = e.Status,
                        jobtypeId = e.Job.JobTypeId,
                        jobid=e.JobId,
                        typesalary=e.Job.TypeSalary
                    }).Where(x => x.jobtypeId == tid && x.status != 0).ToList();
                    if (jobList == null)
                    {
                        return NotFound();
                    }
                    return Ok(jobList);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetTypeByCate")]
        public IActionResult getTypeByCate(int cateId)
        {
            try
            {
                var data = respository.getJobByCateID(cateId);
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

        [HttpGet("searbJobAllDetail")]
        public IActionResult getJobBySearchJOb(string? title, string? location, string? jobName)
        {
           
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(location) && string.IsNullOrEmpty(jobName))
                    {
                        return Ok(new { Message = "data null" });
                    }
                    if (title == null)
                    {
                        title= string.Empty;
                    }
                    if (location == null)
                    {
                        location = string.Empty;
                    }
                    if (jobName == null)
                    {
                        jobName = string.Empty;
                    }

                    var jobList = context.JobDetails.Select(e => new
                    {
                        id = e.Id,
                        title = e.Title,
                        location = e.Location,
                        jobTime = e.JobTime,
                        company = e.Company,
                        JobtypeName=e.JobType.NameType,
                        catename=e.JobType.Category.Name,
                        status=e.Status
                    }).Where(x => (x.JobtypeName.Contains(title) && x.catename.Contains(jobName) && x.location.Contains(location)) && x.status == 1).ToList();
                    if (jobList.Count == 0)
                    {
                        return Ok(new { Message = "data null" });
                    }
                    return Ok(jobList);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost("jobapplycant")]
        public IActionResult jobapplycants(AddJobDTO app)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    JobApplication jobx = context.JobApplications.FirstOrDefault(x => x.ApplicantId == app.ApplicantId && x.JobId == app.JobId);
                    if (jobx != null && jobx.Status == 1)
                    {
                        jobx.Status = 0;
                        context.JobApplications.Update(jobx);
                        context.SaveChanges();
                        return Ok(new { Message = "Ung tuyen thanh cong" });
                    }
                    else if (jobx != null && jobx.Status == 0)
                    {
                        jobx.Status = 1;
                        context.JobApplications.Update(jobx);
                        context.SaveChanges();
                        return Ok(new { Message = "Huy ung tuyen" });
                    }
                    else if (jobx == null)
                    {
                        JobApplication addJob = new JobApplication
                        {
                            JobId = app.JobId,
                            ApplicantId = app.ApplicantId,
                            CreatedId = DateTime.Now,
                            Status = 0,
                        };

                        respository.jobapplycant(addJob);
                        return Ok(new { Message = "Ung tuyen thanh cong" });
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("countnhatd")]
        public IActionResult countnhatd()
        {
            using(ParttimeJobContext con = new ParttimeJobContext())
            {
                var countresult = con.Accounts.Where(x => x.RoleId == 2).ToList();
                if(countresult.Count < 0)
                {
                    return Ok(0);
                }
                return Ok(countresult.Count - 2);
            }
        }
        [HttpGet("congviectuyendung")]
        public IActionResult countjobtuyendung()
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                var countresult = con.JobDetails.Where(x => x.Status == 1).ToList();
                if (countresult.Count < 0)
                {
                    return Ok(0);
                }
                return Ok(countresult.Count - 2);
            }
        }

    }
}
