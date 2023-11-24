using AutoMapper;
using BusinessObject.Models;
using Capstone_Project_490.Hubs;
using DataProvider.Requests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Capstone_Project_490
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSignalR();
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            builder.Services.AddSingleton(configuration);
            builder.Services.AddDbContext<ParttimeJobContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
            });


            builder.Services.AddControllers()
           .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // Đặt DefaultChallengeScheme cho Google
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie() // Thêm hỗ trợ xác thực cookie
    .AddGoogle("Google", googleOptions =>
    {
        //googleOptions.SignInScheme = IdedntityServerConstants.ExternalCookieAuthenticationScheme;
        // Thiết lập ClientID và ClientSecret để truy cập API google
        string clientId = configuration["Authentication:Google:ClientId"];
        string clientSecret = configuration["Authentication:Google:ClientSecret"];
        googleOptions.ClientId = clientId;
        googleOptions.ClientSecret = clientSecret;

    })
    .AddFacebook(facebookOptions =>
    {
        // Đọc cấu hình
        string appId = configuration["Authentication:Facebook:AppId"];
        string appSecret = configuration["Authentication:Facebook:AppSecret"];
        facebookOptions.AppId = appId;
        facebookOptions.AppSecret = appSecret;
        // Thiết lập đường dẫn Facebook chuyển hướng đến
        facebookOptions.CallbackPath = "/signin-facebook";
    })
    .AddJwtBearer(options =>
    {
        string issuer = configuration["Authentication:Token:Your_Issuer"];
        string audience = configuration["Authentication:Token:Your_Audience"];
        string signingKey = configuration["Authentication:Token:IssuerSigningKey"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
        };
    });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddCors();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            //end
            app.MapHub<MessageHub>("/chatHub");
            app.MapControllers();
            app.Run();
        }
    }
}