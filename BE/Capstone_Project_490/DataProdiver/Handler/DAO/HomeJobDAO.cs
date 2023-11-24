using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;


namespace BusinessObject.DAO
{
    public class HomeJobDAO
    {
        private static HomeJobDAO instance = null;
        private static readonly object instanceLock = new object();
        private HomeJobDAO() { }
        private List<JobDetail> jobList = new List<JobDetail>();
        private List<Category> categoryList = new List<Category>();
        private List<JobType> jobType = new List<JobType>();
        public static HomeJobDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new HomeJobDAO();
                    }
                }
                return instance;
            }

        }
        public List<JobDetail> getAllJob()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    jobList = context.JobDetails.Where(x=>x.Status==1).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobList;
        }
        public List<JobDetail> getJobsByID(int id)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    jobList = context.JobDetails.Include(a=>a.Employer).ThenInclude(a=>a.Account).Where(x => x.Id == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobList;
        }
        public List<Category> getCategories()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    categoryList = context.Categories.ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categoryList;
        }
        public List<JobType> getAllJobType()
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    jobType = context.JobTypes.Include(x=>x.JobDetails).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobType;
        }
        public List<JobDetail> getJobWithType(int typeId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    jobList = context.JobDetails.Where(x => x.JobTypeId == typeId).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobList;
        }
        public List<JobType> getJobTypeWithCate(int cateId)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    jobType = context.JobTypes.Where(x => x.CategoryId == cateId).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobType;
        }
        public List<JobDetail> searchJobByString(string? title, string? location, string? typeJob)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
             jobList = context.JobDetails.Include(a=>a.JobType).Where(a =>
           (string.IsNullOrEmpty(title) || a.Title.Contains(title)) &&
           (string.IsNullOrEmpty(location) || a.Location.Contains(location)) &&
           (string.IsNullOrEmpty(typeJob) || a.JobType.NameType.Contains(typeJob))).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobList;
        }
        public List<JobDetail> searchJobByStringNotSalary(string? title, string? location)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    jobList = context.JobDetails.Where(a =>
           (string.IsNullOrEmpty(title) || a.Title.Contains(title)) &&
           (string.IsNullOrEmpty(location) || a.Location.Contains(location)))
            .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobList;
        }
        public void jobapply(JobApplication jobApplication)
        {
            try
            {
               
                using (ParttimeJobContext jobContext = new ParttimeJobContext())
                {
                    
                        jobContext.JobApplications.Add(jobApplication);
                        jobContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
