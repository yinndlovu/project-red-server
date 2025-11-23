using System.ComponentModel.DataAnnotations;

namespace ProjectRed.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; } = null!;
        [MaxLength(30)]
        public string? Surname { get; set; }
        [Range(1000, 9999)]
        public int BirthYear { get; set; }
        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; } = null!;
        public string? PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool IsVerified { get; set; } = false;
        public string AuthProvider { get; set; } = "local";
        [Required, MaxLength(2)]
        public string CountryCode { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
