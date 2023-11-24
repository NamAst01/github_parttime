using BusinessObject.Models;

namespace DataProvider.Services.CandidatePage
{
    public interface ICandidateManager
    {
        public List<Candidate> GetCandidates(int id);
        public void updatecandidate(Candidate candidate);
    }
}
