using BusinessObject.Models;
using DataProvider.Requests.Feedbacks;
using DataProvider.Responses.Feedbacks;
using DataProvider.Responses.Interview;

namespace DataProvider.Handler.Feedbacks
{
    public class SentFeedbackHandler : IHandler<SentFeedbackRequest, SentFeedbackResponse>//Nhận vào from: candidateId, to:jobApplicationId
    {
        public SentFeedbackResponse handler(SentFeedbackRequest request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    try
                    {
                        Comment comment = new Comment();
                        if (request.Content.Trim().Length < 0)
                        {
                            return new SentFeedbackResponse()
                            {
                                Message = "Không để trống nội dung",
                            };
                        }
                        comment.Content = request.Content;
                        var account=context.Candidates.FirstOrDefault(a=>a.Id== request.From);
                        if(account==null)
                        {
                            return new SentFeedbackResponse()
                            {
                                Message = "Không tìm thấy candidate",
                            };
                        }
                        comment.FromAccountId = account.AccountId;
                        var job=context.JobApplications.FirstOrDefault(x=>x.Id== request.To);
                        if (job == null) {
                            return new SentFeedbackResponse()
                            {
                                Message = "Không tìm thấy người đăng công việc",
                            };
                        }
                        var emp = context.JobDetails.FirstOrDefault(x => x.Id == job.JobId);
                        comment.AccountId = emp.EmployerId;
                        comment.Status = request.Status;
                        comment.Start = request.Star;
                        comment.Time = DateTime.Now.ToString("yyyy-MM-dd");
                        job.IsComment = 1;

                        context.JobApplications.Update(job);
                        context.Comments.Add(comment);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new SentFeedbackResponse()
                        {
                            Message = ex.Message,
                        };
                    }
                    return new SentFeedbackResponse()
                    {
                        Message = "successfull",
                    };

                }

            }
            catch (Exception ex)
            {
                return new SentFeedbackResponse()
                {
                    Message = ex.Message,
                };
            }
        }
    }
}
