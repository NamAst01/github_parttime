namespace DataProvider.Requests.Jobs
{
    public class CancelApply
    {
        private int jobId;
        private string resonCancel;

        public int JobId { get => jobId; set => jobId = value; }
        public string ResonCancel { get => resonCancel; set => resonCancel = value; }

        public CancelApply(int jobId, string resonCancel)
        {
            this.jobId = jobId;
            this.resonCancel = resonCancel;
        }
        public CancelApply()
        {
            jobId = 0;
            resonCancel = String.Empty;
        }
    }
}
