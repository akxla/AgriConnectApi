
using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.DTOs.Admin
{
    public class RegisterOfficerRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string EmployeeNumber { get; set; } = string.Empty;

        public string? Specialization { get; set; }

        public string? District { get; set; }

        public string? Office { get; set; }

        public string? ContactNumber { get; set; }
    }
}
