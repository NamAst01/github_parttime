namespace DataProvider.Responses.Accounts
{
    public class ForgetPasswordResponse : Response
    {
        private string message;

        public string Message { get => message; set => message = value; }

        public ForgetPasswordResponse(string message)
        {
            this.message = message;
        }
        public ForgetPasswordResponse()
        {
            message = "";
        }
    }
}
