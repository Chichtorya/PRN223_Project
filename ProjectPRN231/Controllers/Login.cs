using DataAccess.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN231.Models;
using ProjectPRN231.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LoginController : ControllerBase
    {
        private readonly toDoContext _service;
        private readonly IConfiguration _configuration;

   

        public LoginController(
            IConfiguration configuration,
           toDoContext service)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            var user =  _service
            .Users.Where(x => x.UserName == login.UserName && x.Password == login.Password).Include(x=>x.Role).FirstOrDefault();

            if (user == null)
            {
                return Unauthorized();
            }

            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name.ToString().ToLower()),
        };
                
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.UtcNow.AddYears(10),
                 signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new TokenRequest(jwt, user.Role.Name.ToString().ToLower() == "admin"?1:0 ));
        }

    }
}
