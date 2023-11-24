using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataProvider.Handler.Candidates;
using DataProvider.Handler.Employee;
using DataProvider.Requests.Candidates;
using DataProvider.Requests.DTO;
using DataProvider.Requests.Employer;
using DataProvider.Responses.Candidates;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Reflection;
using System.Globalization;
using static IdentityServer4.Models.IdentityResources;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private IMapper _mapper;
        public EmployerController(IMapper mapper)
        {
            _mapper = mapper;
        }
       /* [HttpGet("getAllCandidateByJobId")]
        public IActionResult getAllCandidateByApplyJob(int? idJobApp)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var allEmploy = context.JobApplications.Include(x => x.Applicant).Where(t => t.JobId == idJobApp).Select(e => new {
                    fullname = e.Applicant.Account.FullName,
                    phone = e.Applicant.Phone,
                    address = e.Applicant.Address,
                    city = e.Applicant.City,
                    distric = e.Applicant.Distric,
                    gender = e.Applicant.Account.Gender,
                    titleJob = e.Job.Title,
                }).ToList();

                if (allEmploy.Count == 0)
                {
                    return Ok(new { Message = "fail" });
                }
                return Ok(allEmploy);
            }
        }*/
       /* [HttpGet("getAllJobDetailByEmp")]
        public IActionResult getAllJobDetailByEmp(int? empid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var allEmploy = context.JobDetails.Include(x => x.Employer).Where(t => t.EmployerId == empid)
                .ToList();

                if (allEmploy.Count == 0)
                {
                    return Ok(new { Message = "fail" });
                }
                return Ok(allEmploy);
            }
        }*/
       /* [HttpGet("getAllCandidateByApply")]
        public IActionResult getAllCandidateByApply()
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var allEmploy = context.JobApplications.Include(x => x.Applicant).Select(e => new {
                    fullname = e.Applicant.Account.FullName,
                    phone = e.Applicant.Phone,
                    address = e.Applicant.Address,
                    city = e.Applicant.City,
                    distric = e.Applicant.Distric,
                    gender = e.Applicant.Account.Gender,
                    titleJob = e.Job.Title,
                }).ToList();

                if (allEmploy.Count == 0)
                {
                    return Ok(new { Message = "fail" });
                }
                return Ok(allEmploy);
            }
        }*/
      /*  [HttpGet("getAllEmployByStatus")]
        public IActionResult getAllCandidateStatus(int? idApp, int? Jobid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                JobApplication jobx = context.JobApplications.FirstOrDefault(x => x.ApplicantId == idApp && x.JobId == Jobid);
                if (jobx != null)
                {
                    jobx.Status = 1;
                    context.JobApplications.Update(jobx);
                    context.SaveChanges();
                    return Ok(new { Message = "Ung tuyen thanh cong" });
                }
                return Ok();
            }
        }*/
        [HttpGet("getEmployerByEmpId")]
        public IActionResult getEmpById(int? eid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var epmloy = context.Employers.Select(e => new
                {
                    accountid = e.Account.Id,
                    id = e.Id,
                    image = e.Image,
                    fullname = e.Account.FullName,
                    dob = e.Dob.ToString("yyyy-MM-dd") ,
                    position = e.Position,
                    companyaddress = e.CompanyAddress,
                    gender = e.Account.Gender,
                    phone = e.Phone,
                    email = e.Account.Email,
                    city = e.City,
                    company =e.CompanyAddress,
                    distric = e.Distric,
                    addressDetail = e.Addressdetail,
                    expectAddress = e.Expectaddress,
                }).Where(a => a.id == eid).ToList();
                return Ok(epmloy);
                //okee
            }
        }
        [HttpPut("SaveProfileEmploy")]
        public IActionResult SaveProfile(ProfileRequestEmp request)
        {
            SaveProfileEmpHandler handler = new SaveProfileEmpHandler();
            ProfileResponse response = handler.handler(request);
            return Ok(response);
        }

        [HttpPost("createJobDetail")]
        public IActionResult createJobDetail(CreatePostDTO job)
        {
            using(ParttimeJobContext context = new ParttimeJobContext())
            {

                JobDetail jobdetail = new JobDetail
                {
                    EmployerId = job.EmployerId,
                    Title = job.Title,

                    Description = job.Description,
                    Salary = job.Salary,

                    Location = job.Location,
                    Deadline = job.Deadline,

                    CreatedAt = DateTime.Now,
                    JobTime = job.JobTime,

                    Status = 0,
                    JobTypeId = job.JobTypeId,
                    Typename = job.Typename.ToString(),

                    Experient = job.Experient,
                    Rolecompany = job.Rolecompany,

                    NumberApply = job.NumberApply,
                    TypeJob = job.TypeJob,

                    Daywork = job.Daywork,
                    Note = job.Note,

                    Dob = job.Dob,
                    Toage = job.Toage,

                    Fromage = job.Fromage,
                    Levellearn = job.Levellearn,

                    Welfare = job.Welfare,
                    Moredesciption = job.Moredesciption,

                    Checktypejob = job.Checktypejob,
                    Agreesalary = job.Agreesalary,
                    Company=job.Company,
                    TypeSalary=job.TypeSalary,

                };
                context.JobDetails.Add(jobdetail);
                context.SaveChanges();
                return Ok(new { Message = "Create thanh cong post" });
            }
            
        }
        [HttpPost("createJobDetailtinnhap")]
        public IActionResult createJobDetailtinnhap(CreatePostDTO job)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {

                JobDetail jobdetail = new JobDetail
                {
                    EmployerId = job.EmployerId,
                    Title = job.Title,

                    Description = job.Description,
                    Salary = job.Salary,

                    Location = job.Location,
                    Deadline = job.Deadline,

                    CreatedAt = DateTime.Now,
                    JobTime = job.JobTime,

                    Status = 4,
                    JobTypeId = job.JobTypeId,
                    Typename = job.Typename.ToString(),

                    Experient = job.Experient,
                    Rolecompany = job.Rolecompany,

                    NumberApply = job.NumberApply,
                    TypeJob = job.TypeJob,

                    Daywork = job.Daywork,
                    Note = job.Note,

                    Dob = job.Dob,
                    Toage = job.Toage,

                    Fromage = job.Fromage,
                    Levellearn = job.Levellearn,

                    Welfare = job.Welfare,
                    Moredesciption = job.Moredesciption,

                    Checktypejob = job.Checktypejob,
                    Agreesalary = job.Agreesalary,
                    Company = job.Company,
                    TypeSalary = job.TypeSalary,

                };
                context.JobDetails.Add(jobdetail);
                context.SaveChanges();
                return Ok(new { Message = "luu thanh tin nhap" });
            }

        }
        [HttpGet("GetEmployerByAId")]
        public IActionResult GetEmployerByAId(int aId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var employerReal = context.Employers.FirstOrDefault(x => x.AccountId == aId);
                    if(employerReal == null)
                    {
                        var email = context.Accounts.FirstOrDefault(x => x.Id == aId).Email;
                        var account = context.Accounts.FirstOrDefault(x => x.Email == email && x.RoleId==1);
                        var employerReal1 = context.Employers.FirstOrDefault(x => x.AccountId == account.Id);
                        return Ok(employerReal1);
                    }
                    return Ok(employerReal);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllJobDetailByEid")]
        public IActionResult GetAllJobDetailByEmp(int? empid)
        {
           using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobByE = context.JobDetails.Where(x=> x.EmployerId == empid && (x.Status == 1)).ToList();
                if(jobByE.Count() < 0)
                {
                    return Ok(new { Message = "Not Job" });
                }
                return Ok(jobByE);
            }
        }
        //get job by status la 4
        [HttpGet("getstatus4")] 
        public IActionResult getAllJobStatus4(int? idemp)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobByE = context.JobDetails.Where(x => x.EmployerId == idemp && x.Status == 4).ToList();
                if (jobByE.Count() < 0)
                {
                    return Ok(new { Message = "Job nhap" });
                }
                return Ok(jobByE);
            }
        }
        [HttpPut("updatestatustinnhap")]
        public IActionResult updatestatustinnhap(int? idjob)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                JobDetail job = con.JobDetails.FirstOrDefault(x => x.Id == idjob);
                if (job != null)
                {
                    job.Status = 4;
                    con.JobDetails.Update(job);
                    con.SaveChanges();
                    return Ok(new { Message = "update stus tin nhap" });
                }
                return NoContent();
            }
        }

        // lay job da dc duyet
        [HttpGet("getAllJobDetailByEidWithstatus")]
        public IActionResult GetAllJobDetailByStatus(int? empid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobByE = context.JobDetails.Where(x => x.EmployerId == empid && x.Status == 1).ToList();
                if (jobByE.Count() < 0)
                {
                    return Ok(new { Message = "Not Job" });
                }
                return Ok(jobByE);
            }
        }
        // lay job da ko dc duyet
        [HttpGet("getJobBydWithstatus2")]
        public IActionResult GetJobByStatus(int? idemp)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobByEmp = context.JobDetails.Where(x => x.EmployerId == idemp && x.Status == 2).ToList();
                if (jobByEmp.Count() < 0)
                {
                    return Ok(new { Message = "Not Job" });
                }
                return Ok(jobByEmp);
            }
        }
        // tinh dong bai viet status la 3
        [HttpPut("closejobdetailBystatus")]
        public IActionResult closejobDetail(int idjob, int empid)
        {
            using (ParttimeJobContext ctx = new ParttimeJobContext())
            {
                var getjobclode = ctx.JobDetails.FirstOrDefault(x=>x.Id == idjob && x.EmployerId == empid);
                if (getjobclode != null)
                {
                    if (getjobclode.Deadline > DateTime.Now)
                    {
                        getjobclode.Status = 3;
                        ctx.JobDetails.Update(getjobclode);
                        ctx.SaveChanges();
                        return Ok(new { Message = "Job da dc close do qua han" });
                    }
                    else
                    {
                        getjobclode.Status = 3;
                        ctx.JobDetails.Update(getjobclode);
                        ctx.SaveChanges();
                    }
                    return Ok(new { Message = "Job da dc close" });
                }
                return BadRequest();
            }
        }
        [HttpGet("getJobBystatus0")]
        public IActionResult GetJobByStatus0(int? idemp)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobByEmp = context.JobDetails.Where(x => x.EmployerId == idemp && x.Status == 0).OrderByDescending(x=> x.Id).ToList();
                if (jobByEmp.Count() < 0)
                {
                    
                    return Ok(new { Message = "Not Job" });
                }
                return Ok(jobByEmp);
            }
        }
        [HttpGet("getJobBystatus3")]
        public IActionResult GetJobByStatus3(int? idemp)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobByEmp = context.JobDetails.Where(x => x.EmployerId == idemp && x.Status == 3).ToList();
                if (jobByEmp.Count() < 0)
                {
                    return Ok(new { Message = "Not Job" });
                }
                return Ok(jobByEmp);
            }
        }
        [HttpDelete("deleteJob")]
        public IActionResult deleteJobDetail(int? id)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobid = context.JobDetails.FirstOrDefault(x => x.Id == id);
                if (jobid == null)
                {
                    return NotFound();
                }
                var jobApplycan = context.JobApplications.Where(a => a.JobId == id).ToList();
                foreach (var jobapply in jobApplycan)
                {
                    context.JobApplications.Remove(jobapply);
                }
                context.JobDetails.Remove(jobid);
                context.SaveChanges();
                return Ok(new { Message = "delete Job success!" });
            }
        }
        [HttpPut("updatejobDetail")]
        public IActionResult updatejobDetail(JobDetaiDTO job)
        {
            using (ParttimeJobContext jobcontext = new ParttimeJobContext())
            {
                var p = jobcontext.JobDetails.Find(job.Id);
                if (p == null) return NotFound();
                p.Title = job.Title;

                p.Description = job.Description;
                    p.Salary = job.Salary;

                p.Location = job.Location;
                p.Deadline = job.Deadline;

                p.CreatedAt = DateTime.Now;
                p.JobTime = job.JobTime;

                p.Status = 0;
                p.JobTypeId = 1;
                p.Typename = job.Typename;

                p.Experient = job.Experient;
                p.Rolecompany = job.Rolecompany;

                p.NumberApply = job.NumberApply;
                p.TypeJob = job.TypeJob;

                p.Daywork = job.Daywork;
                p.Note = job.Note;

                p.Dob = job.Dob;
                p.Toage = job.Toage;

                p.Fromage = job.Fromage;
                p.Levellearn = job.Levellearn;

                p.Welfare = job.Welfare;
                 p.Moredesciption = job.Moredesciption;

                p.Checktypejob = job.Checktypejob;
                p.Agreesalary = job.Agreesalary;
                p.Company = job.Company;
                p.TypeSalary = job.TypeSalary;
                jobcontext.Update(p);
                jobcontext.SaveChanges();
                return Ok(new { Message = "update job thanh coong" });
            }
        }
        [HttpGet("getjobbyid")]
        public IActionResult getjobbyId(int id)
        {
            using(ParttimeJobContext context = new ParttimeJobContext())
            {
                var getjobyId = context.JobDetails.Where(a=>a.Id == id).Select(job => new
                {
                    Id = job.Id,
                    EmployerId = job.EmployerId,
                    Title = job.Title,

                    Description = job.Description,
                    Salary = job.Salary,

                    Location = job.Location,
                    Deadline = job.Deadline,

                    CreatedAt = DateTime.Now,
                    JobTime = job.JobTime,

                    Status = 0,
                    JobTypeId = job.JobTypeId,
                    Typename = job.Typename,

                    Experient = job.Experient,
                    Rolecompany = job.Rolecompany,

                    NumberApply = job.NumberApply,
                    TypeJob = job.TypeJob,

                    Daywork = job.Daywork,
                    Note = job.Note,

                    Dob = job.Dob,
                    Toage = job.Toage,

                    Fromage = job.Fromage,
                    Levellearn = job.Levellearn,

                    Welfare = job.Welfare,
                    Moredesciption = job.Moredesciption,

                    Checktypejob = job.Checktypejob,
                    Agreesalary = job.Agreesalary,

                    company=job.Company,
                    typeSalary=job.TypeSalary,
                }).ToList();
                return Ok(getjobyId);
            }
        }
       
        [HttpGet("tindangtuyendung")]
        public IActionResult tindangtuyendung(int? idemp)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                var resultc = con.JobDetails.Where(x => x.EmployerId == idemp && x.Status== 0).Select(x => x.Id).ToList();
                if (resultc.Count > 0)
                {
                    return Ok(resultc.Count);
                }
                else
                {
                    return Ok("Không có");
                }
            }
        }
        [HttpGet("ungviendaungtuyen")]
        public IActionResult ungviendaungtuyen(int? idemp)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                var resultc = con.JobApplications.Where(x=> x.Job.EmployerId == idemp).Select(x => x.ApplicantId).ToList();
                if (resultc.Count > 0)
                {
                    return Ok(resultc.Count);
                }
                else
                {
                    return Ok("Không có");
                }
            }
        }
        [HttpGet("tindangtrongtuan")]
        public IActionResult tindangtrongtuan(int? idemp)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                DateTime startDate = DateTime.Now.AddDays(-7); // Lấy ngày bắt đầu 7 ngày trước
                DateTime endDate = DateTime.Now; // Lấy ngày hiện tại
                var resultc = con.JobDetails.Where(x => x.EmployerId == idemp && x.CreatedAt >= startDate && x.CreatedAt <= endDate).Select(x => x.Id).ToList();
                if (resultc.Count > 0)
                {
                    return Ok(resultc.Count);
                }
                else
                {
                    return Ok("Không có");
                }
            }
        }
        [HttpGet("tindangtrongthang")]
        public IActionResult tindangtrongthang(int? idemp)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                DateTime startDate = DateTime.Now.AddMonths(-1); // Lấy ngày bắt đầu 1 tháng trước
                DateTime endDate = DateTime.Now; // Lấy ngày hiện tại

                var resultc = con.JobDetails
                    .Where(x => x.EmployerId == idemp && x.CreatedAt >= startDate && x.CreatedAt <= endDate)
                    .Select(x => x.Id)
                    .ToList();
                if (resultc.Count > 0)
                {
                    return Ok(resultc.Count);
                }
                else
                {
                    return Ok("Không có");
                }
            }

        }
        [HttpGet("ungvientrongthang")]
        public IActionResult ungvientrongthang(int? idemp)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                DateTime startDate = DateTime.Now.AddMonths(-1); // Lấy ngày bắt đầu 1 tháng trước
                DateTime endDate = DateTime.Now; // Lấy ngày hiện tại
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var resultc = context.JobApplications.Where(x => x.Job.EmployerId == idemp && x.CreatedId >= startDate && x.CreatedId <= endDate).Select(x => x.ApplicantId).ToList();
                    if (resultc.Count > 0)
                    {
                        return Ok(resultc.Count);
                    }
                    else
                    {
                        return Ok("Không có");
                    }
                }
            }

        }
        [HttpGet("ungvientrongtuan")]
        public IActionResult ungvientrongtuan(int? idemp)
        {
            using (ParttimeJobContext con = new ParttimeJobContext())
            {
                DateTime startDate = DateTime.Now.AddDays(-7); // Lấy ngày bắt đầu 1 tháng trước
                DateTime endDate = DateTime.Now; // Lấy ngày hiện tại
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var resultc = context.JobApplications.Where(x => x.Job.EmployerId == idemp && x.CreatedId >= startDate && x.CreatedId <= endDate).Select(x => x.ApplicantId).ToList();
                    if (resultc.Count > 0)
                    {
                        return Ok(resultc.Count);
                    }
                    else
                    {
                        return Ok("Không có");
                    }
                }
            }

        }
    }
}
