using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OAuthSerever.Models
{
    public class UserToken
    {
        [Key]
        public string UserId { get; set; } = "";
        [Key]
        public string TokenType {  get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = "";
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
