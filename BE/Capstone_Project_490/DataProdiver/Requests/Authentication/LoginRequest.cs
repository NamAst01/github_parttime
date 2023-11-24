namespace DataProvider.Requests.Authentication
{
    public class LoginRequest: Request
    {
        private String email;
        private String password;

        public LoginRequest()
        {
            // Khởi tạo các thuộc tính trong constructor mặc định.
            email = "";
            password = "";
        }

        public LoginRequest(String email, String password)
        {
            this.email = email;
            this.password = password;
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
