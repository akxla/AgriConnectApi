
using AgriConnectApi.DTOs.SupplyRequestReview;
using AgriConnectApi.IServices;

namespace AgriConnectApi.Services
{
    public class SupplyRequestReviewService
        : ISupplyRequestReviewService
    {
        public string? ValidateReviewDecision(
            ReviewSupplyRequestDto dto)
        {
            // Check that review information was provided
            if (dto == null)
            {
                return "Review details are required.";
            }

            // Check whether the decision is supported
            if (dto.Decision != "Approved" &&
                dto.Decision != "Rejected")
            {
                return "Decision must be Approved or Rejected.";
            }

            // A rejected request must have an explanation
            if (dto.Decision == "Rejected" &&
                string.IsNullOrWhiteSpace(dto.Remarks))
            {
                return "A rejection reason is required.";
            }

            // Valid request
            return null;
        }
    }
}

