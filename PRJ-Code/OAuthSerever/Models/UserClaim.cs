using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OAuthSerever.Models
{
    public class UserClaim
    {
        [Key]
        public string Id { get; set; } = "";
        [Required]
        public string ClaimType { get; set; } = "";
        [Required]
        public string ClaimValue { get; set; } = "";
        public string UserId { get; set; } = "";
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
