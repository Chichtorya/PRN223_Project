using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
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
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            var key = Configuration["Jwt:Key"];
            builder.Services.AddDbContext<toDo2Context>(options => options.UseSqlServer(Configuration.GetConnectionString("connect")));
            builder.Services.AddAuthorization();

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    //ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidateAudience = false,
                    //ValidAudience = Configuration["Jwt:Audience"],
                    //RequireExpirationTime = true,
                    //ValidateLifetime = true,
                    IssuerSigningKey = signinKey,
                    //RequireSignedTokens = true,
                };
            });
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
            //app.Use(async (context, next) =>
            //{
            //    var token = context.Request.Cookies["AccessToken"];

            //    if (!string.IsNullOrEmpty(token) &&
            //        !context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + token);
            //    }

            //    await next();
            //});
            //app.UseCors(builder => {
            //    builder.AllowAnyOrigin()
            //    .AllowAnyHeader()
            //    .AllowAnyMethod();
            //});
            app.UseCors("AllowSpecificOrigin");

            app.Run();
        }
    }
}