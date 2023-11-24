namespace DataProvider.Requests.Feedbacks
{
    public class SentFeedbackRequest:Request
    {
        private string content;
        private int from;
        private int to;
        private short status;
        private short star;

        public string Content { get => content; set => content = value; }
        public int From { get => from; set => from = value; }
        public int To { get => to; set => to = value; }
        public short Status { get => status; set => status = value; }
        public short Star { get => star; set => star = value; }

        public SentFeedbackRequest(string content, int from, int to, short status, short star)
        {
            this.content = content;
            this.from = from;
            this.to = to;
            this.status = status;
            this.star = star;
        }
        public SentFeedbackRequest()
        {
            content = String.Empty;
            from = 0;
            to = 0;
            status = 0;
            star = 0;
        }
    }
}
