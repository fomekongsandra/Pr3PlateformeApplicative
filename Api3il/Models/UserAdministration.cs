using System.ComponentModel.DataAnnotations;

namespace Api3il.Models
{
    public class UserAdministration
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
