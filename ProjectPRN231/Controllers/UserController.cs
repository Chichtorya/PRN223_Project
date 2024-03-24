using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN231.DTO;
using ProjectPRN231.Models;

namespace ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly toDo2Context _service;

        public UserController(toDo2Context service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<ActionResult> CreateUser(AccountDto userData)
        {

            if (userData.UserName.IsNullOrEmpty() || userData.Password.IsNullOrEmpty()) return BadRequest("UserName and PassWord have not empty");
            if (userData.FirstName.IsNullOrEmpty() || userData.LastName.IsNullOrEmpty()) return BadRequest("Name have not empty");

            if (await _service.Users.AnyAsync(u => u.UserName == userData.UserName || u.Email == userData.Email))
            {
                return BadRequest("UserName or Email already exists.");
            }

            // Tạo mới người dùng
            var user = new User
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Mobile = userData.Mobile,
                Email = userData.Email,
                UserName = userData.UserName,
                Password = userData.Password,
                Address = userData.Address,
                Dob = userData.Dob,
                RoleId = 3,
                HoatDong = true,
                Dflag = false,
            };

            _service.Users.Add(user);
            await _service.SaveChangesAsync();

            // Tạo mới cài đặt người dùng
            var userSetting = new UserSetting
            {
                UserId = user.Id,
                DarkMode = false,
                EmailPopup = false,
                Popup = false
            };

            _service.UserSettings.Add(userSetting);
            await _service.SaveChangesAsync();

            // Tạo mới kế hoạch hàng ngày
            var dailyPlan = new Plant
            {
                Name = "Daily",
                UserId = user.Id,
                HoatDong = true,
                Dflag = false
            };

            _service.Plants.Add(dailyPlan);
            await _service.SaveChangesAsync();

            return Ok("User created successfully.");
        }

        [HttpGet("{userId}")]

        public IActionResult UserProfile(int userId)
        {
            try
            {
                var user = _service.Users.Include(u => u.UserSettings)
                                         .FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    return NotFound();
                }
                var userProfileDTO = new UserDto(user);

                return Ok(userProfileDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
