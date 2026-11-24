
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
                return "Invalid verification decision.";
            }

            if (dto.Decision == "Rejected" &&
                string.IsNullOrWhiteSpace(dto.Remarks))
            {
                return "Remarks are required when rejecting a quotation.";
            }

            return null;
        }
    }
}
