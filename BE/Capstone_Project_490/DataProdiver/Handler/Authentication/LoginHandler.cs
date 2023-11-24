using BusinessObject.Models;
using DataProvider.Requests.Authentication;
using DataProvider.Responses.Authentication;
using DataProvider.Services.Authen;

namespace DataProvider.Handler.Authentication
{
    public class LoginHandler : IHandler<LoginRequest, LoginResponse>
    {
        private static IAuthentication authentication=new IAuthentication();
        public LoginResponse handler(LoginRequest request)
        {
            try
            {
                if(request == null)
                {
                    throw new ArgumentNullException();
                }
                if(request.Email.Trim().Length == 0 || request.Password.Trim().Length == 0) {
                    return new LoginResponse()
                    {
                        Message = "Không để trống email password"
                    };
                }
                if (authentication.CheckAccountExist(request.Email.Trim()))
                {
                    using (ParttimeJobContext context = new ParttimeJobContext())
                    {
                        var trueAccount = new Account();
                        var ACandidate = context.Accounts.FirstOrDefault(a=>a.Email.Equals(request.Email.Trim()) && a.RoleId==2);
                        var AEmployer = context.Accounts.FirstOrDefault(a=>a.Email.Equals(request.Email.Trim()) && a.RoleId==1);
                        var Admin = context.Accounts.FirstOrDefault(a=>a.Email.Equals(request.Email.Trim()) && a.RoleId==3);
                        var AdminAccept = context.Accounts.FirstOrDefault(a=>a.Email.Equals(request.Email.Trim()) && a.RoleId==4);
                        if (ACandidate != null)
                        {
                            trueAccount = ACandidate;
                        }
                        else if (AEmployer != null)
                        {
                            trueAccount = AEmployer;
                        }
                        if (Admin != null)
                        {
                            LoginResponse response = new LoginResponse()
                            {
                                AccessToken = authentication.Token(Admin),
                                Message = "Login thanh cong",
                                Account = Admin,
                                IsCandidate = false,
                                IsEmployer = false
                            };
                            return response;
                        }
                        if (AdminAccept != null)
                        {
                            LoginResponse response = new LoginResponse()
                            {
                                AccessToken = authentication.Token(AdminAccept),
                                Message = "Login thanh cong",
                                Account = AdminAccept,
                                IsCandidate = false,
                                IsEmployer = false
                            };
                            return response;
                        }
                        
                        if (authentication.CheckPassword(request.Password.Trim(), ACandidate.Password) || authentication.CheckPassword(request.Password.Trim(), AEmployer.Password))
                        {
                            if (ACandidate != null && AEmployer != null)
                            {
                                LoginResponse response = new LoginResponse()
                                {
                                    AccessToken = authentication.Token(trueAccount),
                                    Message = "Login thanh cong",
                                    Account = trueAccount,
                                    IsCandidate = true,
                                    IsEmployer = true
                                };
                                return response;
                            }
                            else if (ACandidate != null)
                            {
                                LoginResponse response = new LoginResponse()
                                {
                                    AccessToken = authentication.Token(trueAccount),
                                    Message = "Login thanh cong",
                                    Account = trueAccount,
                                    IsCandidate = true,
                                    IsEmployer = false
                                };
                                return response;
                            }
                            else
                            {
                                LoginResponse response = new LoginResponse()
                                {
                                    AccessToken = authentication.Token(trueAccount),
                                    Message = "Login thanh cong",
                                    Account = trueAccount,
                                    IsCandidate = false,
                                    IsEmployer = true
                                };
                                return response;
                            }
                        }
                        else
                        {
                            return new LoginResponse()
                            {
                                Message = "Password khong dung"
                            };
                        }
                    }                                        
                }
                else
                {
                    return new LoginResponse()
                    {
                        Message="Email khong dung"
                    };
                }
            }catch(Exception ex)
            {
                return new LoginResponse();
            }           
        }
    }
}
