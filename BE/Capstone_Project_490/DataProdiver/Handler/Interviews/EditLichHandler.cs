using BusinessObject.Models;
using DataProvider.Requests.Interview;
using DataProvider.Responses.Interview;

namespace DataProvider.Handler.Interviews
{
    public class EditLichHandler : IHandler<InterviewRequest, InterviewResponse>
    {
        public InterviewResponse handler(InterviewRequest request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    try
                    {                       
                        var emp = context.Interviews.FirstOrDefault(x => x.Id == request.AccountId);
                        if (emp == null)
                        {
                                return new InterviewResponse()
                                {
                                    Message = "Tìm account employer theo aid lỗi",
                                };
                        }
                        else
                        {
                            emp.Start = request.Start;
                            emp.End = request.End;
                            emp.Date = request.Date;
                        }
                        context.Interviews.Update(emp);
                        context.SaveChanges();
                        return new InterviewResponse()
                        {
                            Message = "successfull",
                        };
                    }
                    catch (Exception ex)
                    {
                        return new InterviewResponse()
                        {
                            Message = ex.Message,
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                return new InterviewResponse()
                {
                    Message = ex.Message,
                };
            }
        }
    }
}
