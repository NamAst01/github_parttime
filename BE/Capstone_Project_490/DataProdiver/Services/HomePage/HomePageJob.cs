using BusinessObject.DAO;
using BusinessObject.Models;
using DataProvider.Requests.DTO;

namespace DataProvider.Services.HomePage
{
    public class HomePageJob : IHomePageJob
    {
        public List<Category> getAllCateJob()
        {
            return HomeJobDAO.Instance.getCategories();
        }

        public List<JobDetail> getAllJob()
        {
           return HomeJobDAO.Instance.getAllJob();
        }

        public List<JobType> getAllJobType()
        {
           return HomeJobDAO.Instance.getAllJobType();
        }

        public List<JobType> getJobByCateID(int cateId)
        {
          return HomeJobDAO.Instance.getJobTypeWithCate(cateId);
        }

        public List<JobDetail> getJobsByID(int id)
        {
            return HomeJobDAO.Instance.getJobsByID(id);
        }

        public List<JobDetail> getJobsByType(int jobTypeId)
        {
           return HomeJobDAO.Instance.getJobWithType(jobTypeId);
        }

        public void jobapplycant(JobApplication application)
        {
          HomeJobDAO.Instance.jobapply(application);
        }

        public List<JobDetail> searchJobAll(string? title, string? location, string? jobtype)
        {
          return HomeJobDAO.Instance.searchJobByString(title, location, jobtype);
        }

        public List<JobDetail> searchJobAllNotSalry(string? title, string? location)
        {
           return HomeJobDAO.Instance.searchJobByStringNotSalary(title, location);
        }
    }
}
