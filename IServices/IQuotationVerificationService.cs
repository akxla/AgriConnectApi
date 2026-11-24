
using AgriConnectApi.DTOs.QuotationVerification;

namespace AgriConnectApi.IServices
{
    public interface IQuotationVerificationService
    {
        string? ValidateDecision(VerifyQuotationDto dto);
    }
}
