using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OAuthSerever.Models
{
    public class UserRole
    {
        [Key]
        public string UserId { get; set; } = "";
        [Key]
        public string RoleId { get; set; } = "";
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}
