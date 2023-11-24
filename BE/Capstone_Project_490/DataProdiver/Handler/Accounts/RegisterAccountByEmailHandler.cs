using BusinessObject.Models;
using DataProvider.Requests;
using DataProvider.Requests.Accounts;
using DataProvider.Responses.Accounts;
using DataProvider.Services.Authen;
using IdentityServer4.Extensions;
using static IdentityServer4.Models.IdentityResources;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace DataProvider.Handler.Accounts
{
    public class RegisterAccountByEmailHandler : IHandler<RegisterAccountByEmailRequest, RegisterAccountByEmailResponse>
    {
        private static IAuthentication authentication = new IAuthentication();
        private static ParttimeJobContext context = new ParttimeJobContext();
        public RegisterAccountByEmailResponse handler(RegisterAccountByEmailRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                if (!authentication.CheckFormatEmail(request.Email.Trim()))
                {
                    return new RegisterAccountByEmailResponse()
                    {
                        Message = "Email sai định dạng"
                    };
                }
                if (!request.Password.Trim().Equals(request.Repassword.Trim()))
                {
                    return new RegisterAccountByEmailResponse() { Message = "Password không trùng khớp" };

                }
                if (request.Role.Trim().Equals("Candidate"))
                {
                    if (String.IsNullOrEmpty(request.Email.Trim()) || String.IsNullOrEmpty(request.Password.Trim()) || String.IsNullOrEmpty(request.Repassword.Trim()) || String.IsNullOrEmpty(request.Phone.Trim())
                    || String.IsNullOrEmpty(request.Fullname.Trim()) || String.IsNullOrEmpty(request.City.Trim()) || String.IsNullOrEmpty(request.District.Trim())
                    || String.IsNullOrEmpty(request.DetailAddress.Trim()))
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Không được để trống các thông tin"
                        };
                    }
                    var checkAccount = context.Accounts.FirstOrDefault(a => a.Email.Equals(request.Email.Trim()) && a.RoleId == 2);
                    if (checkAccount != null)
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Email đã được đăng ký"
                        };
                    }
                    String encodePassword = authentication.Encode(request.Password.Trim());
                    Account account = new Account();
                    account.Email = request.Email.Trim();
                    account.Password = encodePassword;
                    account.RoleId = 2;
                    account.Gender= request.Gender.Trim();
                    account.FullName = request.Fullname.Trim() == null ? String.Empty : request.Fullname.Trim();
                    account.CreatedAt = DateTime.Now;
                    account.LastLoginAt = DateTime.Now;
                    account.Status = 1;
                    try
                    {
                        authentication.CreateAccount(account);
                    }
                    catch (Exception ex)
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Lỗi create Account"
                        };
                    }

                    Candidate candidate = new Candidate();
                    candidate.Phone = request.Phone.Trim() == null ? String.Empty : request.Phone.Trim();
                    candidate.Dob = request.Dob;
                    candidate.Address =request.DetailAddress.Trim();
                    candidate.Image = request.Image.Trim() == null ? String.Empty : request.Image.Trim();
                    candidate.AccountId = context.Accounts.OrderByDescending(a => a.Id).FirstOrDefault().Id;
                    candidate.Status = 1;
                    candidate.City = request.City.Trim();
                    candidate.Distric = request.District.Trim();
                    try
                    {
                        authentication.CreateCandidate(candidate);
                    }
                    catch (Exception ex)
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Lỗi create Candidate"
                        };
                    }

                }
                else{
                    if (String.IsNullOrEmpty(request.Email.Trim()) || String.IsNullOrEmpty(request.Password.Trim()) || String.IsNullOrEmpty(request.Repassword.Trim()) || String.IsNullOrEmpty(request.Phone.Trim())
                    || String.IsNullOrEmpty(request.Fullname.Trim()) || String.IsNullOrEmpty(request.City.Trim()) || String.IsNullOrEmpty(request.District.Trim())
                    || String.IsNullOrEmpty(request.DetailAddress.Trim()) || String.IsNullOrEmpty(request.CompanyName.Trim()) || String.IsNullOrEmpty(request.Position.Trim()))
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Không được để trống các thông tin"
                        };
                    }
                    var checkAccount = context.Accounts.FirstOrDefault(a => a.Email.Equals(request.Email.Trim()) && a.RoleId == 1);
                    if (checkAccount != null)
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Email đã được đăng ký"
                        };
                    }
                    String encodePassword = authentication.Encode(request.Password.Trim());
                    Account account = new Account();
                    account.Email = request.Email.Trim();
                    account.Password = encodePassword;
                    account.RoleId = 1;
                    account.Gender = request.Gender.Trim();
                    account.FullName = request.Fullname.Trim() == null ? String.Empty : request.Fullname.Trim();
                    account.CreatedAt = DateTime.Now;
                    account.LastLoginAt = DateTime.Now;
                    account.Status = 1;
                    try
                    {
                        authentication.CreateAccount(account);
                    }
                    catch (Exception ex)
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Lỗi create Account"
                        };
                    }
                    Employer employer = new Employer();
                    employer.Phone = request.Phone.Trim();
                    employer.CompanyAddress = request.City.Trim() + request.District.Trim() + request.DetailAddress.Trim();
                    employer.Image = request.Image.Trim();
                    employer.Position = request.Position.Trim();
                    employer.Company = request.CompanyName.Trim();
                    employer.AccountId = context.Accounts.OrderByDescending(a => a.Id).FirstOrDefault().Id;
                    employer.Status = 1;
                    employer.Dob=request.Dob;
                    try
                    {
                        authentication.CreateEmployer(employer);
                    }
                    catch (Exception ex)
                    {
                        return new RegisterAccountByEmailResponse()
                        {
                            Message = "Lỗi create Employer"
                        };
                    }
                }
                return new RegisterAccountByEmailResponse()
                {
                    Message = "Đăng ký thành công"
                };               
            }catch(Exception ex) {
                return new RegisterAccountByEmailResponse()
                {
                    Message = "Đăng ký không thành công"
                };
            }
        }
    }
}
