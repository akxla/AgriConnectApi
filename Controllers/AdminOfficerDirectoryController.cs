
using System.Threading.Tasks;

using AgriConnectApi.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/admin/officers")]
    [Authorize(Roles = "Admin")]
    public class AdminOfficerDirectoryController : ControllerBase
    {
        private readonly IAdminOfficerDirectoryService _service;

        public AdminOfficerDirectoryController(
            IAdminOfficerDirectoryService service)
        {
            _service = service;
        }

        // GET /api/admin/officers
        [HttpGet]
        public async Task<IActionResult> GetAllOfficers()
        {
            var officers = await _service.GetAllOfficersAsync();

            return Ok(officers);
        }

        // GET /api/admin/officers/2
        [HttpGet("{officerId:int}")]
        public async Task<IActionResult> GetOfficerById(int officerId)
        {
            if (officerId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid Agriculture Officer ID."
                });
            }

            var officer =
                await _service.GetOfficerByIdAsync(officerId);

            if (officer == null)
            {
                return NotFound(new
                {
                    message = "Agriculture Officer not found."
                });
            }

            return Ok(officer);
        }
    }
}
