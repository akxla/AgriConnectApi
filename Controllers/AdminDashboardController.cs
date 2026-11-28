
using System.Threading.Tasks;

using AgriConnectApi.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/admin/dashboard")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService _service;

        public AdminDashboardController(
            IAdminDashboardService service)
        {
            _service = service;
        }

        // GET /api/admin/dashboard
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboard = await _service.GetDashboardAsync();

            return Ok(dashboard);
        }
    }
}
