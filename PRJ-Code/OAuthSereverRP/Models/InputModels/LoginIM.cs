using System.ComponentModel.DataAnnotations;

namespace OAuthSereverRP.Models.InputModels
{
    public class LoginIM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
