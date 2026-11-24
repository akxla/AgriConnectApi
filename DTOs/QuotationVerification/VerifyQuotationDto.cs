
using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.DTOs.QuotationVerification
{
    public class VerifyQuotationDto
    {
        [Required]
        [RegularExpression("^(Approved|Rejected)$",
            ErrorMessage = "Decision must be Approved or Rejected.")]
        public string Decision { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Remarks { get; set; }
    }
}
