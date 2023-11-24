using BusinessObject.Models;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("GetAllCate")]
        public IActionResult GetAllCate()
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var cate = context.Categories.ToList();
                return Ok(cate);
            }               
        }
        [HttpGet("GetAllJobType")]
        public IActionResult GetAllJobType()
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobtype = context.JobTypes.ToList();
                return Ok(jobtype);
            }
        }
        [HttpGet("GetAllJobTypeBycate")]
        public IActionResult GetAllJobTypeBycate(int cateid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var jobtype = context.JobTypes.Select(e => new
                {
                    id=e.Id,
                    NameType=e.NameType,
                    cateid=e.Category.Id

                }).Where(a=>a.cateid == cateid).ToList();
                return Ok(jobtype);
            }
        }
    }
}
