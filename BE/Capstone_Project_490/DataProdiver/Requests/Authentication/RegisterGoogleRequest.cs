namespace DataProvider.Requests.Authentication
{
    public class RegisterGoogleRequest : Request
    {
        private string role;
        private string email;
        private string fullname;

        public string Role { get => role; set => role = value; }
        public string Email { get => email; set => email = value; }
        public string Fullname { get => fullname; set => fullname = value; }

        public RegisterGoogleRequest(string role, string email, string fullname)
        {
            this.role = role;
            this.email = email;
            this.fullname = fullname;
        }
        public RegisterGoogleRequest()
        {
            role = String.Empty;
            email = String.Empty;
            fullname = String.Empty;
        }
    }
}
