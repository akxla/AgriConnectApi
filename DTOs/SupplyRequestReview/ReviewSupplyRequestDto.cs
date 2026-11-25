
using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.DTOs.SupplyRequestReview
{
    public class ReviewSupplyRequestDto
    {
        [Required(ErrorMessage = "Decision is required.")]
        [RegularExpression(
            "^(Approved|Rejected)$",
            ErrorMessage = "Decision must be Approved or Rejected."
        )]
        public string Decision { get; set; } = string.Empty;

        [StringLength(
            500,
            ErrorMessage = "Review remarks cannot exceed 500 characters."
        )]
        public string? Remarks { get; set; }
    }
}
