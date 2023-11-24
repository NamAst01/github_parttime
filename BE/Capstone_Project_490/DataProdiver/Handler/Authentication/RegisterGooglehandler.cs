using BusinessObject.Models;
using DataProvider.Requests.Authentication;
using DataProvider.Responses.Accounts;
using DataProvider.Responses.Authentication;
using DataProvider.Services.Authen;

namespace DataProvider.Handler.Authentication
{
    public class RegisterGooglehandler : IHandler<RegisterGoogleRequest, LoginResponse>
    {
        private static IAuthentication authentication = new IAuthentication();
        private static ParttimeJobContext context = new ParttimeJobContext();
        public LoginResponse handler(RegisterGoogleRequest request)
        {
            try
            {
                Account newAccount = context.Accounts.FirstOrDefault(a => a.Email.Equals(request.Email.Trim()));
                if(newAccount != null) {
                    LoginResponse response = new LoginResponse()
                    {
                        AccessToken = authentication.Token(newAccount),
                        Message = "Login thanh cong",
                        Account = newAccount
                    };
                    return response;
                }
                else
                {
                    Account account = new Account();
                    account.Email = request.Email.Trim();
                    account.RoleId = 2;
                    account.FullName = request.Fullname.Trim();
                    account.CreatedAt = DateTime.Now;
                    account.LastLoginAt = DateTime.Now;
                    account.Status = 1;
                    try
                    {
                        authentication.CreateAccount(account);
                    }
                    catch (Exception ex)
                    {
                        return new LoginResponse()
                        {
                            Message = "Lỗi create Account"
                        };
                    }

                    Candidate candidate = new Candidate();
                    candidate.Phone = "";
                    candidate.Dob = DateTime.Now;
                    candidate.Address = "";
                    candidate.Image = "";
                    candidate.AccountId = context.Accounts.OrderByDescending(a => a.Id).FirstOrDefault().Id;
                    candidate.Status = 1;
                    try
                    {
                        authentication.CreateCandidate(candidate);
                    }
                    catch (Exception ex)
                    {
                        return new LoginResponse()
                        {
                            Message = "Lỗi create Candidate"
                        };
                    }
                    Account Account = context.Accounts.FirstOrDefault(a => a.Email.Equals(request.Email.Trim()));
                    LoginResponse response = new LoginResponse()
                    {
                        AccessToken = authentication.Token(Account),
                        Message = "Login thanh cong",
                        Account = Account
                    };
                    return response;
                }                
                
            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    Message = "Lỗi hệ thống, xem lại code"
                };
            }
           
        }
    }
}
