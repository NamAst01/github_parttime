namespace DataProvider.Requests.Accounts
{
    public class ForgetPasswordRequest: Request
    {
        private string email;
        private string newPassword;
        private string rePassword;

        public ForgetPasswordRequest(string email, string newPassword, string rePassword)
        {
            this.email = email;
            this.newPassword = newPassword;
            this.rePassword = rePassword;
        }
        public ForgetPasswordRequest()
        {
            email = string.Empty;
            newPassword = string.Empty;
            rePassword = string.Empty;
        }

        public string Email { get => email; set => email = value; }
        public string NewPassword { get => newPassword; set => newPassword = value; }
        public string RePassword { get => rePassword; set => rePassword = value; }
    }
}
