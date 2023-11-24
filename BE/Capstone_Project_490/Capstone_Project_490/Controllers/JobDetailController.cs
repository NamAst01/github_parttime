using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDetailController : ControllerBase
    {
        [HttpGet("GetJobCareFromJobDetailByTypeId")]
        public IActionResult GetJobCareFromJobDetail(int canId, int typeId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.JobDetails.Select(e => new
                    {
                        id = e.Id,
                        title = e.Title,
                        jobAppId = e.Id,
                        jobTime = e.JobTime,
                        location = e.Location,
                        company = e.Rolecompany,
                        care = e.JobApplications.FirstOrDefault(a=>a.ApplicantId==canId).Care,
                        typeId=e.JobTypeId
                    }).Where(x => x.typeId == typeId).ToList();
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

        [HttpGet("GetJobByCate")]
        public IActionResult GetJobByCate()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.Categories.Include(x=>x.JobTypes).ThenInclude(a=>a.JobDetails).ToList();
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
        [HttpGet("ViecLamTotNhat")]
        public IActionResult ViecLamTotNhat()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.JobDetails.Where(a=>a.JobApplications.Count()>0 && a.Status==1).OrderByDescending(a=>a.Id).ToList();
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
        [HttpGet("ViecLamSieuCap")]
        public IActionResult ViecLamSieuCap()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.JobDetails.Where(a => (a.TypeSalary.Equals("Ngày") || a.TypeJob==0) && a.Status == 1).OrderByDescending(a => a.Id).ToList();
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
        [HttpGet("ViecLamSinhVien")]
        public IActionResult ViecLamSinhVien()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.JobDetails.Where(a => ((a.Toage>=16 && a.Fromage <= 22) 
                    || a.Levellearn.Equals("Tốt nghiệp THPT") 
                    || a.Experience.Equals("Không yêu cầu")
                    || a.TypeSalary.Equals("Giờ")) && a.Status == 1).OrderByDescending(a => a.Id).ToList();
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
        [HttpGet("ViecLamGanBanNhat")]
        public IActionResult ViecLamGanBanNhat(int cId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var candidate = context.Candidates.FirstOrDefault(a => a.Id == cId);
                    String ExpectAddress = candidate.ExpectAddress;
                    String Distric = candidate.Distric;
                    var jobx = context.JobDetails.Where(a => (a.Location.Contains(ExpectAddress) 
                    || a.Location.Contains(Distric)) && a.Status == 1).OrderByDescending(a => a.Id)
                    .ToList();
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
        [HttpGet("ViecLamMoiNhat")]
        public IActionResult ViecLamMoiNhat()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    DateTime twoDaysAgo = DateTime.Now.AddDays(-2);

                    var jobx = context.JobDetails
                        .Where(a => a.CreatedAt.Date >= twoDaysAgo.Date && a.Status == 1).OrderByDescending(a => a.Id)
                        .ToList();
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
        [HttpGet("NhaTuyenDungHangDau")]
        public IActionResult NhaTuyenDungHangDau()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    var jobx = context.JobDetails.Where(a => a.JobApplications.Count() > 20 && a.Status == 1).OrderByDescending(a => a.Id).ToList();
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
    }
}
