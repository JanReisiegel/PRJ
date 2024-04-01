using System.ComponentModel.DataAnnotations;

namespace OAuthServerRP.ViewModels
{
    public class LoginIM
    {
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        public bool RememberLogin { get; set; } = false;
        //public string ReturnUrl { get; set; } = "";
    }
}
