using BusinessObject.Models;
using DataProvider.Requests.Accounts;
using DataProvider.Responses.Accounts;
using DataProvider.Services.Authen;

namespace DataProvider.Handler.Accounts
{
    public class ForgetPasswordHandler : IHandler<ForgetPasswordRequest, ForgetPasswordResponse>
    {
        private static IAuthentication authentication = new IAuthentication();
        private static ParttimeJobContext context = new ParttimeJobContext();
        public ForgetPasswordResponse handler(ForgetPasswordRequest request)
        {
            try
            {
                if (String.IsNullOrEmpty(request.Email.Trim()) || String.IsNullOrEmpty(request.NewPassword.Trim()) || String.IsNullOrEmpty(request.RePassword.Trim()))
                {
                    return new ForgetPasswordResponse(){ Message = "Không được để trống các trường" };
                }
                if (request.NewPassword.Equals(request.RePassword))
                {
                    Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(request.Email));
                    if (account != null)
                    {
                        String newPass = authentication.Encode(request.RePassword);
                        account.Password = newPass;
                        context.Accounts.Update(account);
                        context.SaveChanges();
                        return new ForgetPasswordResponse() { Message = "Reset password thành công" };
                    }
                    else
                    {
                        return new ForgetPasswordResponse() { Message = "Lỗi bên trong database không update được" };
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
