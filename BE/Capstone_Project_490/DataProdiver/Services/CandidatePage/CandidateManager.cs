
using BusinessObject.DAO;
using BusinessObject.Models;

namespace DataProvider.Services.CandidatePage
{
    public class CandidateManager : ICandidateManager
    {
        public List<Candidate> GetCandidates(int id)
        {
           return CandidateDAO.Instance.getImformation(id);
        }

        public void updatecandidate(Candidate candidate)
        {
          CandidateDAO.Instance.updateImformationCandidate(candidate);
        }
    }
}
