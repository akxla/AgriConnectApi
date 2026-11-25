
using AgriConnectApi.DTOs.SupplyRequestReview;

namespace AgriConnectApi.IServices
{
    public interface ISupplyRequestReviewService
    {
        string? ValidateReviewDecision(
            ReviewSupplyRequestDto dto);
    }
}
