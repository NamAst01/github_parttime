using BusinessObject.Models;
using DataProvider.Requests.Feedbacks;
using DataProvider.Responses.Feedbacks;

namespace DataProvider.Handler.Feedbacks
{
    public class SentFeedbackToCandateHandler : IHandler<SentFeedbackRequest, SentFeedbackResponse>//Nhân vào from: EmployerId, to:jobApplicationId
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
                        var account = context.Employers.FirstOrDefault(a => a.Id == request.From);
                        if (account == null)
                        {
                            return new SentFeedbackResponse()
                            {
                                Message = "Không tìm thấy employer",
                            };
                        }
                        comment.FromAccountId = account.AccountId;
                        var can = context.JobApplications.FirstOrDefault(x => x.Id == request.To);
                        if (can == null)
                        {
                            return new SentFeedbackResponse()
                            {
                                Message = "Không tìm thấy Candidate",
                            };
                        }
                        comment.AccountId = can.ApplicantId;
                        comment.Status = request.Status;
                        comment.Start = request.Star;
                        comment.Time = DateTime.Now.ToString("yyyy-MM-dd");
                       
                        can.IsEmployerComment = 1;
                        context.JobApplications.Update(can);
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
