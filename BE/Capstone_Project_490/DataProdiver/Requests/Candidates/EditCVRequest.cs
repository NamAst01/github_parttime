namespace DataProvider.Requests.Candidates
{
    public class EditCVRequest:Request
    {
        private string fullname;
        private string dob;
        private string location;
        private string phone;
        private string gender;
        private string skill;
        private int candidateId;

        public string Fullname { get => fullname; set => fullname = value; }
        public string Dob { get => dob; set => dob = value; }
        public string Location { get => location; set => location = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Skill { get => skill; set => skill = value; }
        public int CandidateId { get => candidateId; set => candidateId = value; }

        public EditCVRequest(string fullname, string dob, string location, string phone, string gender, string skill, int candidateId)
        {
            this.fullname = fullname;
            this.dob = dob;
            this.location = location;
            this.phone = phone;
            this.gender = gender;
            this.skill = skill;
            this.candidateId = candidateId;
        }

        public EditCVRequest()
        {
            fullname=String.Empty;
            dob = String.Empty;
            location = String.Empty;
            phone = String.Empty;
            gender = String.Empty;
            skill = String.Empty;
            candidateId = 0;
        }
    }
}
