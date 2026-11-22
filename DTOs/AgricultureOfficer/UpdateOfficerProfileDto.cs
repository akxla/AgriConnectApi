using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.DTOs.AgricultureOfficer
{
    public class UpdateOfficerProfileDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Specialization { get; set; }

        [StringLength(100)]
        public string? District { get; set; }

        [StringLength(100)]
        public string? Office { get; set; }

        [Phone]
        [StringLength(20)]
        public string? ContactNumber { get; set; }
    }
}