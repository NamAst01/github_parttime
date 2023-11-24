
namespace DataProvider.Responses.Accounts
{
    public class RegisterAccountByEmailResponse : Response
    {
        private string message;

        public RegisterAccountByEmailResponse() { message = String.Empty; }

        public RegisterAccountByEmailResponse(string message)
        {
            this.message = message;
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
