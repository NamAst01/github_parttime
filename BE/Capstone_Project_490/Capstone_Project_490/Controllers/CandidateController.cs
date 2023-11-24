using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataProvider.Handler.Accounts;
using DataProvider.Handler.Candidates;
using DataProvider.Requests;
using DataProvider.Requests.Candidates;
using DataProvider.Responses.Accounts;
using DataProvider.Responses.Candidates;
using DataProvider.Services.CandidatePage;
using DataProvider.Services.HomePage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Capstone_Project_490.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CandidateController : ControllerBase
    {
        private List<Candidate> listcandidate = new List<Candidate>();
        private ICandidateManager respository = new CandidateManager();
        private List<JobHistory> jobhistory = new List<JobHistory>();
        private List<Candidate> can = new List<Candidate>();
        private IMapper _mapper;
        public CandidateController( IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("getImformationcan")]
        public IActionResult getAllImformationCan(int id)
        {
            try
            {
                var getListCan = respository.GetCandidates(id);
                if (getListCan == null)
                {
                    return NotFound();

                }
                return Ok(_mapper.Map<List<CandidateDTO>>(getListCan));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }
        [HttpPut("updateinformationcan")]
        public IActionResult updateCan(Candidate candidate)
        {
            try
            {
               respository.updatecandidate(candidate);
                return Ok();

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("getHistoryJob")]
        public IActionResult getHistoryJob(int canid)
        {
            using(ParttimeJobContext context = new ParttimeJobContext())
            {
                jobhistory = context.JobHistories.Include(x => x.Candidate).ToList();
                var result = jobhistory.Where(x => x.CandidateId == canid).Select(x => new
                {
                    Idc = x.Id,
                    CandidateId = x.Candidate.Id,
                    JobIdc = x.JobId,
                    iscomment= x.IsComment,
                    isStatus = x.Status,
                  //  name = x.Candidate.Account.Email.ToString(),
                }).ToList();
                 return Ok(result);
                
            }
          //  return Ok(jobhistory);
            // return Ok();
        }
        [HttpGet("getCandidteByAccId")]
        public IActionResult getCanById(int aid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var candidate = context.Candidates.Select(e => new
                {
                   
                    accountid=e.Account.Id,
                    id = e.Id,
                    image =e.Image,
                    fullname = e.Account.FullName,
                    dob = e.Dob.ToString("yyyy-MM-dd"),
                    gender=e.Account.Gender,
                    phone=e.Phone,
                    email=e.Account.Email,
                    city=e.City,
                    distric=e.Distric,
                    addressDetail=e.Address,
                    expectAddress=e.ExpectAddress,
                }).Where(a=>a.accountid==aid).ToList();
                return Ok(candidate);
            }
          
        }

        [HttpPut("SaveProfile")]
        public IActionResult SaveProfile(ProfileRequest request)
        {
            SaveProfileHandler handler = new SaveProfileHandler();
            ProfileResponse response = handler.handler(request);
            return Ok(response);
        }

        [HttpGet("GetCV")]
        public IActionResult GetCV(int cid)
        {
            using (ParttimeJobContext context = new ParttimeJobContext())
            {
                var candidate = context.Candidates.Select(e => new
                {
                    fullname = e.Account.FullName,
                    candidateId = e.Id,
                    dob = e.Dob,
                    location = e.Address+"-" + e.City + "-" + e.Distric,
                    phone = e.Phone,
                    gender = e.Account.Gender,
                    skill = e.Address,
                }).Where(a => a.candidateId == cid).ToList();
                return Ok(candidate);
            }
        }
        [HttpGet("EditCV")]
        public IActionResult EditCV(EditCVRequest request)
        {
            EditCVHandler handler = new EditCVHandler();
            ProfileResponse response = handler.handler(request);
            return Ok(response);
        }
    }
}
