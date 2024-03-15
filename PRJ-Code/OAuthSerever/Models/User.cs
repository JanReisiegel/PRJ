using System.ComponentModel.DataAnnotations;

namespace OAuthSerever.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Surname { get; set; } = "";
        [Required]
        public string PhoneNumber { get; set; } = "";
        [Required]
        public DateOnly BirthDate { get; set; }
        [Required]
        public string PersonIdentityNumber { get; set; } = "";
        public bool IsAccountConfirmed { get; set; } = false;
    }
}
