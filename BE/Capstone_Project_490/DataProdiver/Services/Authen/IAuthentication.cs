using BusinessObject.Models;
using System.Net.Mail;
using System.Net;
using System.Text;
using static IdentityServer4.Models.IdentityResources;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using DataProvider.Requests;
using DataProvider.Responses.Authentication;
using System.Security.Principal;
using System.Collections;

namespace DataProvider.Services.Authen
{
    public class IAuthentication : Authentication
    {
        private static ParttimeJobContext context = new ParttimeJobContext();
        public bool CheckAccountExist(string email)
        {
            try
            {
                Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
                if (account == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }           
        }

        public bool CheckFormatEmail(string email)
        {
           if(!email.StartsWith("@") && !email.EndsWith("@") && email.Contains("@")){ return true; }
            return false;
        }

        public bool CheckLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckPassword(string password, string HashPassword)
        {
            try
            {
                if (!String.IsNullOrEmpty(password))
                {
                    Boolean isPasswordValid = BCrypt.Net.BCrypt.Verify(password, HashPassword);
                    return isPasswordValid ? true : false;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public void CreateAccount(Account account)
        {
            try
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void CreateAccountWhenLoginFacebook(String fullname, String email)
        {
            try
            {
                Account account = new Account()
                {
                    Email = email,
                    FullName = fullname,
                    RoleId = 2,
                    CreatedAt = DateTime.Now,
                    LastLoginAt = DateTime.Now,
                    Status = 0,
                };
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void CreateAccountWhenLoginGoogle(String fullname,String email)
        {
            try
            {
                Account account = new Account()
                {
                    Email=email,
                    FullName = fullname,
                    RoleId=2,
                    CreatedAt = DateTime.Now,
                    LastLoginAt = DateTime.Now,
                    Status = 0,
                };
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            catch (Exception ex)
            {               
            }
        }

        public void CreateCandidate(Candidate candidate)
        {
            try
            {
                context.Candidates.Add(candidate);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void CreateEmployer(Employer employer)
        {
            try
            {
                context.Employers.Add(employer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public string Encode(string Encode)
        {
            try
            {
                if (!String.IsNullOrEmpty(Encode))
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Encode, BCrypt.Net.BCrypt.GenerateSalt());

                    // Lưu vào database hashedPassword

                    return hashedPassword;
                }
                return String.Empty;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public string GenerateCode(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(validChars.Length);
                password.Append(validChars[index]);
            }
            return password.ToString();
        }

        public Account GetAccountByEmail(string email)
        {
            try
            {
                Account account = context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
                if (account == null)
                {
                    return new Account();
                }
                return account;
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public Account getAccountById(int id)
        {
            try
            {
                Account account = context.Accounts.FirstOrDefault(x => x.Id==id);
                if (account == null)
                {
                    return null;
                }
                return account;
            }catch(Exception e)
            {

            }
            return null;
        }

        public Account getAccountByToken(string accessToken)
        {
            return null;
        }

        public void SentCodeToEmail(string toEmail, string code)
        {
            try
            {
                // Cấu hình thông tin máy chủ SMTP
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "huytqhe151422@fpt.edu.vn";
                string smtpPassword = "kgfegkzcegjzjcbc";


                // Tạo đối tượng SmtpClient
                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true,
                };

                // Tạo đối tượng MailMessage
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    Subject = "Code TIMTRO",
                    Body = "Ma xac thuc cua bạn là:"+ code,
                    IsBodyHtml = false, // Đặt thành true nếu bạn muốn sử dụng HTML trong nội dung email.
                };

                mailMessage.To.Add(toEmail);

                // Gửi email
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                
            }
        }

        public string Token(Account account)
        {
            try
            {
                String role = account.RoleId == 1 ? "Employer" : account.RoleId == 2 ?"Candidate":"Admin";
                var claims = new[]
                {
                    new Claim(ClaimTypes.Sid, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.FullName),
                    new Claim(ClaimTypes.Role, role)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key-with-at-least-128-bits"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "your-issuer",
                    audience: "your-audience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                return "Lỗi khi gen token: "+e.Message;
            }          
        }
        public void updatePassword(string newPassword, int accountId)
        {
            try
            {
                Account account = context.Accounts.FirstOrDefault(account=> account.Id == accountId);
                account.Password=newPassword;
                context.Accounts.Update(account);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
