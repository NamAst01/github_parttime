namespace DataProvider.Requests.Interview
{
    public class InterviewRequest:Request
    {
        private string start;
        private string end;
        private string date;
        private int accountId;

        public string Start { get => start; set => start = value; }
        public string End { get => end; set => end = value; }
        public string Date { get => date; set => date = value; }
        public int AccountId { get => accountId; set => accountId = value; }

        public InterviewRequest(string start, string end, string date, int interviewId)
        {
            this.start = start;
            this.end = end;
            this.date = date;
            this.accountId = interviewId;
        }
        public InterviewRequest()
        {
            start=String.Empty;
            end = String.Empty;
            date = String.Empty;
            accountId = 0;
        }
    }
}
