using BusinessObject.Models;
using DataProvider.Requests.Accounts;
using DataProvider.Responses.Accounts;
using DataProvider.Services.Authen;

namespace DataProvider.Handler.Accounts
{
    public class CheckRegisterHandler : IHandler<RegisterAccountByEmailRequest, RegisterAccountByEmailResponse>
    {
        private static IAuthentication authentication = new IAuthentication();
        private static ParttimeJobContext context = new ParttimeJobContext();
        public RegisterAccountByEmailResponse handler(RegisterAccountByEmailRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                if (!authentication.CheckFormatEmail(request.Email.Trim()))
                {
                    return new RegisterAccountByEmailResponse()
                    {
                        Message = "Email sai định dạng"
                    };
                }
                if (!request.Password.Trim().Equals(request.Repassword.Trim()))
                {
                    return new RegisterAccountByEmailResponse() { Message = "Password không trùng khớp" };
                }
                
                if (request.Role.Trim().Equals("Candidate"))
                {
                    if (request.Phone.Trim().Length!=10)
                    {
                        return new RegisterAccountByEmailResponse() { Message = "Số điện thoại không đủ 10 số" };
                    }
                    if (String.IsNullOrEmpty(request.Email.Trim()) || String.IsNullOrEmpty(request.Password.Trim()) || String.IsNullOrEmpty(request.Repassword.Trim()) || String.IsNullOrEmpty(request.Phone.Trim())
                    || String.IsNullOrEmpty(request.Fullname.Trim()) || String.IsNullOrEmpty(request.City.Trim()) || String.IsNullOrEmpty(request.District.Trim())
                    || String.IsNullOrEmpty(request.DetailAddress.Trim()))
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Không được để trống các thông tin"
                        };
                    }
                    Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(request.Email));
                    if (account != null)
                    {
                        Candidate candidate = context.Candidates.FirstOrDefault(d => d.AccountId == account.Id);
                        if (candidate != null)
                        {
                            return new RegisterAccountByEmailResponse() { Message = "Email đã được đăng ký" };
                        }
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(request.Email.Trim()) || String.IsNullOrEmpty(request.Password.Trim()) || String.IsNullOrEmpty(request.Repassword.Trim()) || String.IsNullOrEmpty(request.Phone.Trim())
                    || String.IsNullOrEmpty(request.Fullname.Trim()) || String.IsNullOrEmpty(request.City.Trim()) || String.IsNullOrEmpty(request.District.Trim())
                    || String.IsNullOrEmpty(request.DetailAddress.Trim()) || String.IsNullOrEmpty(request.CompanyName.Trim()) || String.IsNullOrEmpty(request.Position.Trim()))
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Không được để trống các thông tin"
                        };
                    }
                    Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(request.Email));
                    if (account != null)
                    {
                        Employer employer = context.Employers.FirstOrDefault(e => e.AccountId == account.Id);
                        if (employer != null)
                        {
                            return new RegisterAccountByEmailResponse() { Message = "Email đã được đăng ký" };
                        }
                    }
                }                              
                return new RegisterAccountByEmailResponse()
                {
                    Message = "Đăng ký thành công"
                };
            }
            catch (Exception ex)
            {
                return new RegisterAccountByEmailResponse()
                {
                    Message = "Đăng ký không thành công"
                };
            }
        }
    }
}
