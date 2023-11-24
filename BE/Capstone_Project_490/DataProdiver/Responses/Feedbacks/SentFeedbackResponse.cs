namespace DataProvider.Responses.Feedbacks
{
    public class SentFeedbackResponse:Response
    {
        private string message;

        public string Message { get => message; set => message = value; }

        public SentFeedbackResponse(string message)
        {
            this.message = message;
        }
        public SentFeedbackResponse()
        {
            message = String.Empty;
        }
    }
}
