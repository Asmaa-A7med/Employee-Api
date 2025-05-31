using System.ComponentModel.DataAnnotations;

namespace webApp.Dtos
{
    public class UpdateEmployeeDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
