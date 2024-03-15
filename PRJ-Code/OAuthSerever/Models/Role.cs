using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace OAuthSerever.Models
{
    public class Role
    {
        [Key]
        public string Id { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
    }
}
