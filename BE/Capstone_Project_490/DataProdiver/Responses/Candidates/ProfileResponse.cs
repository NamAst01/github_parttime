namespace DataProvider.Responses.Candidates
{
    public class ProfileResponse : Response
    {
        private string message;

        public string Message { get => message; set => message = value; }

        public ProfileResponse(string message)
        {
            this.message = message;
        }
        public ProfileResponse()
        {
            message = String.Empty;
        }
    }
}