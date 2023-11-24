using BusinessObject.Models;

namespace DataProvider.Services.Authen
{
    public interface Authentication
    {
        String Encode(String Encode);
        Boolean CheckPassword(String password, String HashPassword);
        void updatePassword(String newPassword, int accountId);
        String Token(Account account);
        void SentCodeToEmail(String toEmail, String code);
        String GenerateCode(int length);
        Account getAccountByToken(String accessToken);
        void CreateAccount(Account account);
        Account GetAccountByEmail(String email);
        Boolean CheckAccountExist(String email);
        Boolean CheckLogin(String email,String password);
        Account getAccountById(int id);
        void CreateAccountWhenLoginGoogle(String fullname, String email);
        void CreateAccountWhenLoginFacebook(String fullname, String email);
        void CreateCandidate(Candidate candidate);
        void CreateEmployer(Employer employer);
        Boolean CheckFormatEmail(String email);
    }
}
