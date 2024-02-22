using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN231.Models;
using System.Text;

namespace ProjectPRN231
{
    public class Program
    {
        public static IConfiguration GetConfiguration()
        {
            // Đoạn mã để tạo IConfiguration từ appsettings.json
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration Configuration = GetConfiguration();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var key = Configuration["Jwt:Key"];

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = Configuration["Jwt:Audience"],
                RequireExpirationTime = true,
                ValidateLifetime = true,
                IssuerSigningKey = signinKey,
                RequireSignedTokens = true,
            });
            builder.Services.AddDbContext<toDoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connect")));
            builder.Services.AddAuthorization();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
    
            app.Run();
        }
    }
}