namespace DataProvider.Requests.Candidates
{
    public class ProfileRequest : Request
    {
        private int accountid;
        private string fullname;
        private string phone;
        private string gender;
        private string email;
        private string dob;
        private string city;
        private string distric;
        private string expectAddress;
        private string addressDetail;

        public string Fullname { get => fullname; set => fullname = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string Dob { get => dob; set => dob = value; }
        public string City { get => city; set => city = value; }
        public string Distric { get => distric; set => distric = value; }
        public string ExpectAddress { get => expectAddress; set => expectAddress = value; }
        public string AddressDetail { get => addressDetail; set => addressDetail = value; }
        public int Accountid { get => accountid; set => accountid = value; }

        public ProfileRequest(int accountid, string fullname, string phone, string gender, string email, string dob, string city, string distric, string expectAddress, string addressDetail)
        {
            this.accountid = accountid;
            this.fullname = fullname;
            this.phone = phone;
            this.gender = gender;
            this.email = email;
            this.dob = dob;
            this.city = city;
            this.distric = distric;
            this.expectAddress = expectAddress;
            this.addressDetail = addressDetail;
        }

        public ProfileRequest()
        {
            accountid = 1;
            fullname = String.Empty;
            phone = String.Empty;
            gender = String.Empty;
            email = String.Empty;
            dob = String.Empty;
            city = String.Empty;
            distric = String.Empty;
            expectAddress = String.Empty;
            addressDetail = String.Empty;
        }
    }
}