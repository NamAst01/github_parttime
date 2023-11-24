using BusinessObject.Models;
using DataProvider.Requests.Interview;
using DataProvider.Responses.Interview;
using System.Security.Cryptography;

namespace DataProvider.Handler.Interviews
{
    public class CreateInterviewHandler : IHandler<InterviewRequest, InterviewResponse>
    {
        public InterviewResponse handler(InterviewRequest request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    try
                    {
                        Interview interview=new Interview();
                        interview.Start=request.Start;
                        interview.End=request.End;
                        interview.Date=request.Date;
                      
                        var emp= context.Employers.FirstOrDefault(x=>x.AccountId==request.AccountId);
                        if(emp==null) {
                            var data = context.Accounts.FirstOrDefault(a => a.Id == request.AccountId);
                            if (data == null)
                            {
                                return new InterviewResponse()
                                {
                                    Message = "Tìm account employer theo aid lỗi",
                                };
                            }
                            var account = context.Accounts.FirstOrDefault(a => a.Email == data.Email && a.RoleId == 1);
                            var employer = context.Employers.FirstOrDefault(a => a.AccountId == account.Id);
                            interview.EmployerId = employer.Id;
                        }
                        else
                        {
                            interview.EmployerId = emp.Id;
                        }                       
                        context.Interviews.Add(interview);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new InterviewResponse()
                        {
                            Message = ex.Message,
                        };
                    }
                    return new InterviewResponse()
                    {
                        Message = "successfull",
                    };

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
