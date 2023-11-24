namespace DataProvider.Requests.Notifications
{
    public class CreateNotificationRequest:Request
    {
        private int accountId;
        private int fromAccountId;
        private string title;
        private string content;

        public int AccountId { get => accountId; set => accountId = value; }
        public int FromAccountId { get => fromAccountId; set => fromAccountId = value; }
        public string Title { get => title; set => title = value; }
        public string Content { get => content; set => content = value; }

        public CreateNotificationRequest(int accountId, int fromAccountId, string title, string content)
        {
            this.accountId = accountId;
            this.fromAccountId = fromAccountId;
            this.title = title;
            this.content = content;
        }
        public CreateNotificationRequest()
        {
            accountId = 0;
            fromAccountId = 0;
            title = String.Empty;
            content = String.Empty;
        }
    }
}
