using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN231.Models;
using ProjectPRN231.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class Login : ControllerBase
    {
        toDoContext db;
        IConfiguration configuration;

        public Login(toDoContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult CheckLogin([FromBody] LoginDTO loginIn)
        {
            if (loginIn.UserName == "Admin" && loginIn.Password == "123")
            {
                var key = configuration["Jwt:Key"];
                var sigin_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var sigin_cred = new SigningCredentials(sigin_key, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginIn.UserName),
                    new Claim(ClaimTypes.Email,"cc"),
                    new Claim(ClaimTypes.Role,"admin"),

                };
                var token = new JwtSecurityToken
                (
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: sigin_cred,
                    claims: claims
                ) ;

                var tokenWrite = new JwtSecurityTokenHandler().WriteToken(token);
                return new JsonResult("token="+ tokenWrite );
            }
            else
            {
                return BadRequest("Invalid Password and username");
            }



        }
    }

}
