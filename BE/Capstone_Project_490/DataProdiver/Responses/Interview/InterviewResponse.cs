namespace DataProvider.Responses.Interview
{
    public class InterviewResponse:Response
    {
        private string message;

        public string Message { get => message; set => message = value; }

        public InterviewResponse(string message)
        {
            this.message = message;
        }
        public InterviewResponse()
        {
            message = String.Empty;
        }
    }
}
