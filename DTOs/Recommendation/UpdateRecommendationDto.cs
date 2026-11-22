
using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.DTOs.Recommendation
{
    public class UpdateRecommendationDto
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Category { get; set; }

        [Required]
        [RegularExpression("^(Draft|Published|Archived)$",
            ErrorMessage =
                "Status must be Draft, Published or Archived.")]
        public string Status { get; set; } = "Draft";
    }
}
