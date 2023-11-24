using BusinessObject.Models;
using DataProvider.Requests.Candidates;
using DataProvider.Responses.Candidates;

namespace DataProvider.Handler.Candidates
{
    public class EditCVHandler : IHandler<EditCVRequest, ProfileResponse>
    {
        public ProfileResponse handler(EditCVRequest request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                { 
                    
                    return new ProfileResponse()
                    {
                        Message = "successfull",
                    };

                }

            }
            catch (Exception ex)
            {
                return new ProfileResponse()
                {
                    Message = ex.Message,
                };
            }
        }
    }
}
