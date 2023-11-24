using BusinessObject.Models;
using DataProvider.Requests.Notifications;
using DataProvider.Responses.Feedbacks;

namespace DataProvider.Handler.Notifications
{
    public class CreateNotiForCandidateHandler : IHandler<CreateNotificationRequest, SentFeedbackResponse>
    {
        public SentFeedbackResponse handler(CreateNotificationRequest request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    try
                    {
                        Notification comment = new Notification();
                        if (request.Content.Trim().Length < 0)
                        {
                            return new SentFeedbackResponse()
                            {
                                Message = "Không để trống nội dung",
                            };
                        }
                        comment.Content = request.Content;
                        comment.NotifyFrom = request.FromAccountId;
                        var job = context.JobDetails.FirstOrDefault(x => x.Id == request.AccountId);
                        if (job == null)
                        {
                            return new SentFeedbackResponse()
                            {
                                Message = "Không tìm thấy người đăng công việc",
                            };
                        }
                        var emp = context.Employers.FirstOrDefault(x => x.Id == job.EmployerId);
                        comment.AccountId = emp.AccountId;
                        comment.Title = request.Title;
                        comment.CreatedAt= DateTime.Now;
                        context.Notifications.Add(comment);
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
