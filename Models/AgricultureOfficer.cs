
using System;
using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.Models
{
    public class AgricultureOfficer
    {
        [Key]
        public int OfficerId { get; set; }

        // Link to shared User account.
        // Nullable until the User model is finalized.
        public int? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(30)]
        public string? EmployeeNumber { get; set; }

        [StringLength(100)]
        public string? Specialization { get; set; }

        [StringLength(100)]
        public string? District { get; set; }

        [StringLength(100)]
        public string? Office { get; set; }

        [Phone]
        [StringLength(20)]
        public string? ContactNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
