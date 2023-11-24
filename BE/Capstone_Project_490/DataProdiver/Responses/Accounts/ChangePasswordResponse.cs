namespace DataProvider.Responses.Accounts
{
    public class ChangePasswordResponse:Response
    {
        private string message;

        public ChangePasswordResponse(string message)
        {
            this.message = message;
        }
        public ChangePasswordResponse()
        {
            message=String.Empty;
        }

        public string Message { get => message; set => message = value; }
    }
}
