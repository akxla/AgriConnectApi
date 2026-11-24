
using System;

namespace AgriConnectApi.DTOs.QuotationVerification
{
    public class QuotationVerificationResponseDto
    {
        public int QuotationId { get; set; }

        public string VerificationStatus { get; set; }
            = string.Empty;

        public int? VerifiedByOfficerId { get; set; }

        public string? VerificationRemarks { get; set; }

        public DateTime? VerifiedAt { get; set; }
    }
}
