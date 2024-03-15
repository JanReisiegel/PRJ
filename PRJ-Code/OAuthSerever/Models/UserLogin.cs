using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OAuthSerever.Models
{
    public class UserLogin
    {
        [Key]
        public string LoginProvider { get; set; } = "";
        [Key]
        public string ProviderKey { get; set; } = "";
        public string UserId { get; set; } = "";
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
