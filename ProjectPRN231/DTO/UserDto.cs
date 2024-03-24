using ProjectPRN231.Models;

namespace ProjectPRN231.DTO
{
    public class UserDto
    {
        private User user;

        public UserDto(User user)
        {
            Username = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Mobile = user.Mobile;
            DarkMode = user.UserSettings?.FirstOrDefault().DarkMode;
            EmailPopup = user.UserSettings?.FirstOrDefault().EmailPopup;
            Popup = user.UserSettings?.FirstOrDefault().Popup;
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool? DarkMode { get; set; }
        public bool? EmailPopup { get; set; }
        public bool? Popup { get; set; }
    }
}
