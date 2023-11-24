using AutoMapper;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DAO
{
    public class CandidateDAO
    {
        private static CandidateDAO instance = null;
        private static readonly object instanceLock = new object();
        private List<Candidate> candidates = new List<Candidate>();
        private IMapper _mapper;

        private CandidateDAO(IMapper mapper) { _mapper = mapper; }
        private CandidateDAO() { }

        public static CandidateDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CandidateDAO();

                    }
                    return instance;
                }
            }
        }

        public List<Candidate> getImformation(int? id)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    candidates = context.Candidates.Include(u => u.Account).Where(x => x.Id == (int)id).ToList();
                 /*   var result = candidates.Select(x => new
                    {
                        email = x.Account.Email,
                        img = x.Image,
                        phone = x.Phone,
                        dob = x.Dob,
                        address = x.Address,
                        isReport = x.IsReport,
                        status = x.Status,
                    }).ToList();*/

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return (candidates);
        }
        public void updateImformationCandidate(Candidate candidate)
        {
            try
            {

                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    Candidate cad = context.Candidates.Include(x => x.Account).FirstOrDefault(x => x.Id == candidate.Id);
                    if (cad != null)
                    {
                        cad.Id = candidate.Id;
                        cad.Phone = candidate.Phone;
                        cad.Dob = candidate.Dob;
                        cad.Address = candidate.Address;
                        cad.Image = candidate.Image;
                        cad.Account.Email = candidate.Account.Email;
                        cad.Account.Password = candidate.Account.Password;

                        context.Candidates.Update(cad);
                        context.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
