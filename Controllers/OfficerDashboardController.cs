
using System.Security.Claims;
using System.Threading.Tasks;

using AgriConnectApi.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/agricultureofficers/dashboard")]
    [Authorize(Roles = "AgricultureOfficer")]
    public class OfficerDashboardController : ControllerBase
    {
        private readonly IOfficerDashboardService _dashboardService;

        public OfficerDashboardController(
            IOfficerDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: api/agricultureofficers/dashboard
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            // Get user identity from the JWT
            string? claimValue = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(claimValue, out int userId))
            {
                return Unauthorized(new
                {
                    message = "Invalid user identity."
                });
            }

            // Request dashboard data from the service
            var dashboard = await _dashboardService
                .GetDashboardAsync(userId);

            if (dashboard == null)
            {
                return NotFound(new
                {
                    message =
                        "Active Agriculture Officer profile not found."
                });
            }

            return Ok(dashboard);
        }
    }
}
