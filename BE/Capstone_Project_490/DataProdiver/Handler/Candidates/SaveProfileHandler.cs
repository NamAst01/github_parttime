using BusinessObject.Models;
using DataProvider.Requests.Candidates;
using DataProvider.Requests.Employer;
using DataProvider.Responses.Candidates;

namespace DataProvider.Handler.Candidates
{
    public class SaveProfileHandler : IHandler<ProfileRequest, ProfileResponse>
    {
        public ProfileResponse handler(ProfileRequest request)
        {
            try
            {
                using (ParttimeJobContext context = new ParttimeJobContext())
                {
                    Candidate candidate = context.Candidates.FirstOrDefault(x => x.AccountId == request.Accountid);
                    candidate.Address = request.AddressDetail;
                    candidate.Phone = request.Phone;
                    candidate.City = request.City;
                    candidate.Distric = request.Distric;
                    candidate.ExpectAddress = request.ExpectAddress;
                    candidate.Dob = DateTime.Parse(request.Dob);

                    Account account = context.Accounts.FirstOrDefault(x => x.Id == request.Accountid);
                    account.Gender = request.Gender;
                    account.FullName = request.Fullname;
                    try
                    {
                        context.Candidates.Update(candidate);
                        context.Accounts.Update(account);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return new ProfileResponse()
                        {
                            Message = "Lỗi không update được profile",
                        };
                    }

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
       /* public ProfileResponse handlerEmp(ProfileRequestEmp request)
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
        }*/
    }
}