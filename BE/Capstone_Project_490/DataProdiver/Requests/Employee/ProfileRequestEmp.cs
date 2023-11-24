namespace DataProvider.Requests.Employer
{
    public class ProfileRequestEmp : Request
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
        private string position;
        private string image;
        private string company;


        public string Fullname { get => fullname; set => fullname = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string Dob { get => dob; set => dob = value; }
        public string City { get => city; set => city = value; }
        public string Distric { get => distric; set => distric = value; }
        public string ExpectAddress { get => expectAddress; set => expectAddress = value; }
        public string Position { get => position; set => position = value; }
        public string AddressDetail { get => addressDetail; set => addressDetail = value; }
        public string Image { get => image; set => image = value; }
        public string Company { get => company; set => company = value; }
        public int Accountid { get => accountid; set => accountid = value; }

        public ProfileRequestEmp(int accountid,string position,string company, string image, string fullname, string phone, string gender, string email, string dob, string city, string distric, string expectAddress, string addressDetail)
        {
            this.accountid = accountid;
            this.fullname = fullname;
            this.phone = phone;
            this.gender = gender;
            this.email = email;
            this.dob = dob;
            this.city = city;
            this.position = position;
            this.distric = distric;
            this.expectAddress = expectAddress;
            this.addressDetail = addressDetail;
            this.image = image;
            this.company = company;
        }

        public ProfileRequestEmp()
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
            position = String.Empty;
            image = String.Empty;
            company = String.Empty;
        }
    }
}
