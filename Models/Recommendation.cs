
using System;
using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.Models
{
    public class Recommendation
    {
        [Key]
        public int RecommendationId { get; set; }

        [Required]
        public int OfficerId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Category { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Draft";

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Relationship with Agriculture Officer
        public AgricultureOfficer Officer { get; set; } = null!;
    }
}
