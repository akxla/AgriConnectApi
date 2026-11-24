
using AgriConnectApi.DTOs.QuotationVerification;
using AgriConnectApi.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/agricultureofficers/quotations")]
    [Authorize(Roles = "AgricultureOfficer")]
    public class QuotationVerificationController : ControllerBase
    {
        private readonly IQuotationVerificationService _service;

        public QuotationVerificationController(
            IQuotationVerificationService service)
        {
            _service = service;
        }

        // GET: api/agricultureofficers/quotations/pending
        [HttpGet("pending")]
        public IActionResult GetPendingQuotations()
        {
            return StatusCode(501, new
            {
                message = "Pending quotation retrieval is not yet implemented."
            });
        }

        // POST: api/agricultureofficers/quotations/5/verify
        [HttpPost("{quotationId:int}/verify")]
        public IActionResult VerifyQuotation(
            int quotationId,
            [FromBody] VerifyQuotationDto dto)
        {
            if (quotationId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid quotation ID."
                });
            }

            var validationError = _service.ValidateDecision(dto);

            if (validationError != null)
            {
                return BadRequest(new
                {
                    message = validationError
                });
            }

            // This does not update any quotation yet.
            // Actual database integration comes later.

            return StatusCode(501, new
            {
                message = "Verification input is valid, but saving " +
                          "the decision is not yet implemented.",
                quotationId
            });
        }
    }
}
