namespace DataProvider.Requests.Accounts
{
    public class ChangePasswordRequest:Request
    {
        private int accountId;
        private string oldpassword;
        private string newpassword;
        private string repassword;

        public string Oldpassword { get => oldpassword; set => oldpassword = value; }
        public string Newpassword { get => newpassword; set => newpassword = value; }
        public string Repassword { get => repassword; set => repassword = value; }
        public int AccountId { get => accountId; set => accountId = value; }

        public ChangePasswordRequest(int accountId,string oldpassword, string newpassword, string repassword)
        {
            this.accountId = accountId;
            this.oldpassword = oldpassword;
            this.newpassword = newpassword;
            this.repassword = repassword;
        }
        public ChangePasswordRequest()
        {
            accountId= 1;
            oldpassword=String.Empty;
            newpassword = String.Empty;
            repassword = String.Empty;
        }

    }
}
