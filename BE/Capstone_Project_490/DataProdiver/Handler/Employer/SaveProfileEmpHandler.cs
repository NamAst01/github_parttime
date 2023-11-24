using BusinessObject.Models;
using DataProvider.Requests.Candidates;
using DataProvider.Requests.Employer;
using DataProvider.Responses.Candidates;


namespace DataProvider.Handler.Employee
{
    public class SaveProfileEmpHandler : IHandler<ProfileRequestEmp, ProfileResponse>
    {
        public ProfileResponse handler(ProfileRequestEmp request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    Employer employ = context.Employers.FirstOrDefault(x => x.AccountId == request.Accountid);
                    employ.Addressdetail = request.AddressDetail;
                    employ.Phone = request.Phone;
                    employ.Position = request.Position;
                    employ.Company = request.Company;
                    employ.City = request.City;
                    employ.Distric = request.Distric;
                    employ.Expectaddress = request.ExpectAddress;
                    employ.Dob = DateTime.Parse(request.Dob);
                    employ.Image = request.Image;
                    Account account = context.Accounts.FirstOrDefault(x => x.Id == request.Accountid);
                    account.Gender = request.Gender;
                    account.FullName = request.Fullname;
                    try
                    {
                        context.Employers.Update(employ);
                        context.Accounts.Update(account);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ProfileResponse()
                        {
                            Message = "Lỗi không update được profile emp",
                        };
                    }

                    return new ProfileResponse()
                    {
                        Message = "successfull emp",
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
