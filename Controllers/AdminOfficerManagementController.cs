
using System.Threading.Tasks;

using AgriConnectApi.DTOs.Admin;
using AgriConnectApi.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/admin/officers")]
    [Authorize(Roles = "Admin")]
    public class AdminOfficerManagementController : ControllerBase
    {
        private readonly IAdminOfficerManagementService _service;

        public AdminOfficerManagementController(
            IAdminOfficerManagementService service)
        {
            _service = service;
        }

        // PATCH /api/admin/officers/2/status
        [HttpPatch("{officerId:int}/status")]
        public async Task<IActionResult> UpdateOfficerStatus(
            int officerId,
            [FromBody] UpdateOfficerStatusDto request)
        {
            if (officerId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid Agriculture Officer ID."
                });
            }

            if (!request.IsActive.HasValue)
            {
                return BadRequest(new
                {
                    message = "IsActive is required."
                });
            }

            var officer = await _service.UpdateOfficerStatusAsync(
                officerId,
                request.IsActive.Value);

            if (officer == null)
            {
                return NotFound(new
                {
                    message = "Agriculture Officer not found."
                });
            }

            return Ok(new
            {
                message = officer.IsActive
                    ? "Agriculture Officer activated successfully."
                    : "Agriculture Officer deactivated successfully.",
                data = officer
            });
        }
    }
}
