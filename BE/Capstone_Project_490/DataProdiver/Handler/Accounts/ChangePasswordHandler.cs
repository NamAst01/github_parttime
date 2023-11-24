using BusinessObject.Models;
using DataProvider.Requests.Accounts;
using DataProvider.Responses.Accounts;
using DataProvider.Services.Authen;

namespace DataProvider.Handler.Accounts
{
    public class ChangePasswordHandler : IHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private static IAuthentication authentication = new IAuthentication();
        private static ParttimeJobContext context = new ParttimeJobContext();
        public ChangePasswordResponse handler(ChangePasswordRequest request)
        {
            try
            {
                if (String.IsNullOrEmpty(request.Newpassword.Trim()) || String.IsNullOrEmpty(request.Repassword.Trim()) || String.IsNullOrEmpty(request.Oldpassword.Trim()))
                {
                    return new ChangePasswordResponse()
                    {
                        Message = "Không để trống thông tin"
                    };
                }
                if (!request.Newpassword.Trim().Equals(request.Repassword.Trim()))
                {
                    return new ChangePasswordResponse(){
                        Message="Mật khẩu mới không trùng khớp với nhau"
                    };
                }
                Account account = context.Accounts.FirstOrDefault(a => a.Id == request.AccountId);
                if (authentication.CheckPassword(request.Oldpassword.Trim(), account.Password))
                {
                    String EncodePass = authentication.Encode(request.Newpassword.Trim());
                    authentication.updatePassword(EncodePass, account.Id);
                    return new ChangePasswordResponse()
                    {
                        Message = "successfull"
                    };
                }
                else
                {
                    return new ChangePasswordResponse()
                    {
                        Message = "Mật khẩu cũ không chính xác"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ChangePasswordResponse() { Message = "Lỗi ở đâu đấy trong hệ thống" };
            }
        }
    }
}
