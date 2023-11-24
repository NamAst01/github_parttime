using BusinessObject.Models;

namespace DataProvider.Responses.Authentication
{
    public class LoginResponse : Response
    {
        private string accessToken; // Biến (field) với kiểu camelCase
        private string message; // Biến (field) với kiểu camelCase
        private Account account;
        private bool isCandidate;
        private bool isEmployer;

        public string AccessToken // Property với kiểu PascalCase
        {
            get { return accessToken; }
            set { accessToken = value; }
        }
        public string Message // Property với kiểu PascalCase
        {
            get { return message; }
            set { message = value; }
        }

        public Account Account { get => account; set => account = value; }
        public bool IsCandidate { get => isCandidate; set => isCandidate = value; }
        public bool IsEmployer { get => isEmployer; set => isEmployer = value; }

        public LoginResponse()
        {
            accessToken = string.Empty;
            message = string.Empty;
            account= new Account();
            isCandidate= false;
            isEmployer= false;
        }

        public LoginResponse(string accessToken, string message,Account account, bool isCandidate, bool isEmployer)
        {
            this.accessToken = accessToken;
            this.message = message;
            this.account = account;
            this.isCandidate = isCandidate;
            this.isEmployer= isEmployer;
        }
    }
}
