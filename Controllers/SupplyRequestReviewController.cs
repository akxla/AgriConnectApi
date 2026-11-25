
using AgriConnectApi.DTOs.SupplyRequestReview;
using AgriConnectApi.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/agricultureofficers/supplyrequests")]
    [Authorize(Roles = "AgricultureOfficer")]
    public class SupplyRequestReviewController : ControllerBase
    {
        private readonly ISupplyRequestReviewService _service;

        public SupplyRequestReviewController(
            ISupplyRequestReviewService service)
        {
            _service = service;
        }

        // GET: api/agricultureofficers/supplyrequests/pending
        [HttpGet("pending")]
        public IActionResult GetPendingRequests()
        {
            // Integration with Member 1 is still pending
            return StatusCode(501, new
            {
                message = "Retrieving pending supply requests " +
                          "is not yet implemented."
            });
        }

        // GET: api/agricultureofficers/supplyrequests/5
        [HttpGet("{requestId:int}")]
        public IActionResult GetRequestDetails(int requestId)
        {
            if (requestId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid supply request ID."
                });
            }

            return StatusCode(501, new
            {
                message = "Supply request details retrieval " +
                          "is not yet implemented.",
                supplyRequestId = requestId
            });
        }

        // POST: api/agricultureofficers/supplyrequests/5/review
        [HttpPost("{requestId:int}/review")]
        public IActionResult ReviewRequest(
            int requestId,
            [FromBody] ReviewSupplyRequestDto dto)
        {
            if (requestId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid supply request ID."
                });
            }

            string? validationError =
                _service.ValidateReviewDecision(dto);

            if (validationError != null)
            {
                return BadRequest(new
                {
                    message = validationError
                });
            }

            // Do not save or report success yet.
            // Database integration will be implemented later.

            return StatusCode(501, new
            {
                message = "Review details are valid, but saving " +
                          "the decision is not yet implemented.",
                supplyRequestId = requestId
            });
        }
    }
}
