
using AgriConnectApi.DTOs.QuotationVerification;
using AgriConnectApi.IServices;

namespace AgriConnectApi.Services
{
    public class QuotationVerificationService
        : IQuotationVerificationService
    {
        public string? ValidateDecision(VerifyQuotationDto dto)
        {
            if (dto == null)
            {
                return "Verification details are required.";
            }

            if (dto.Decision != "Approved" &&
                dto.Decision != "Rejected")
            {
                return "Decision must be Approved or Rejected.";
            }

            if (dto.Decision == "Rejected" &&
                string.IsNullOrWhiteSpace(dto.Remarks))
            {
                return "A rejection reason is required.";
            }

            return null;
        }
    }
}
