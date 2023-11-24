namespace DataProvider.Requests.Accounts
{
    public class RegisterAccountByEmailRequest : Request
    {
        private string role; 
        private string email; 
        private string password; 
        private string repassword; 
        private string fullname; 
        private string phone; 
        private DateTime dob;
        private string image;        
        private string gender;        
        private string companyName;        
        private string position;        
        private string city; 
        private string distric; 
        private string detailAddress;

        public RegisterAccountByEmailRequest(string role, string email, string password, string repassword, string fullname, string phone, DateTime dob, string image, string gender, string companyName, string position, string city, string distric, string detailAddress)
        {
            this.role = role;
            this.email = email;
            this.password = password;
            this.repassword = repassword;
            this.fullname = fullname;
            this.phone = phone;
            this.dob = dob;
            this.image = image;
            this.gender = gender;
            this.companyName = companyName;
            this.position = position;
            this.city = city;
            this.distric = distric;
            this.detailAddress = detailAddress;
        }

        public RegisterAccountByEmailRequest()
        {
            role = String.Empty;
            email = String.Empty; 
            password=String.Empty; 
            repassword=String.Empty;
            fullname = String.Empty;
            phone = String.Empty;
            dob = DateTime.Now;
            image = String.Empty;
            gender = String.Empty;
            companyName = String.Empty;
            position = String.Empty;
            city = String.Empty;
            distric = String.Empty;
            detailAddress = String.Empty;
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Repassword
        {
            get { return repassword; }
            set { repassword = value; }
        }

        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string District
        {
            get { return distric; }
            set { distric = value; }
        }

        public string DetailAddress
        {
            get { return detailAddress; }
            set { detailAddress = value; }
        }
    }
}
