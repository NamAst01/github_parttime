using BusinessObject.Models;
using DataProvider.Requests.Accounts;
using DataProvider.Responses.Accounts;
using DataProvider.Services.Authen;

namespace DataProvider.Handler.Accounts
{
    public class CheckForgetPasswordHandler : IHandler<ForgetPasswordRequest, ForgetPasswordResponse>
    {
        private static IAuthentication authentication = new IAuthentication();
        private static ParttimeJobContext context = new ParttimeJobContext();
        public ForgetPasswordResponse handler(ForgetPasswordRequest request)
        {
            try
            {
                if (String.IsNullOrEmpty(request.Email.Trim()) || String.IsNullOrEmpty(request.NewPassword.Trim()) || String.IsNullOrEmpty(request.RePassword.Trim()))
                {
                    return new ForgetPasswordResponse() { Message = "Không được để trống các trường" };
                }
                if (!authentication.CheckFormatEmail(request.Email.Trim()))
                {
                    return new ForgetPasswordResponse()
                    {
                        Message = "Email sai định dạng"
                    };
                }
                if (request.NewPassword.Trim().Equals(request.RePassword.Trim()))
                {
                    Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(request.Email));
                    if (account != null)
                    {
                        return new ForgetPasswordResponse() { Message = "Successfull" };
                    }
                    else
                    {
                        return new ForgetPasswordResponse() { Message = "Email không chính xác" };
                    }
                }
                else
                {
                    return new ForgetPasswordResponse() { Message = "Password không trùng khớp" };
                }
            }
            catch (Exception ex)
            {
                return new ForgetPasswordResponse() { Message = "Lỗi ở đâu đấy trong hệ thống" };
            }
        }
    }
}
