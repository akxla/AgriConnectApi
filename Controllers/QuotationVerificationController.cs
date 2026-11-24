
using System.Threading.Tasks;
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
                message = "Pending quotation retrieval requires the " +
                          "Supplier Quotation module."
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

            string? error = _service.ValidateDecision(dto);

            if (error != null)
            {
                return BadRequest(new
                {
                    message = error
                });
            }

            // Do not claim the quotation was approved/rejected yet.
            // Database integration will be added after
            // Member 2 completes the Quotation entity.

            return StatusCode(501, new
            {
                message = "Decision validated, but quotation " +
                          "verification is not yet implemented.",
                quotationId,
                decision = dto.Decision
            });
        }
    }
}
