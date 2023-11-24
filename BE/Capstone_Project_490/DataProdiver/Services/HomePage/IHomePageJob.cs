using BusinessObject.Models;
using DataProvider.Requests.DTO;

namespace DataProvider.Services.HomePage
{
    public interface IHomePageJob
    {
        public List<JobDetail> getAllJob();
        public List<JobDetail> getJobsByID(int id);
        public List<Category> getAllCateJob();
        public List<JobType> getAllJobType();
        public List<JobDetail> getJobsByType(int jobTypeId);
        public List<JobType> getJobByCateID(int cateId);

        public List<JobDetail> searchJobAll(string? title, string? location, string? jobtype);
        public List<JobDetail> searchJobAllNotSalry(string? title, string? location);
        public void jobapplycant(JobApplication application);
        
    }
}
