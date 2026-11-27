
using AgriConnectApi.DTOs.Admin;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : ControllerBase
    {
        // GET /api/admin/users
        [HttpGet]
        public IActionResult GetUsers()
        {
            return StatusCode(
                StatusCodes.Status501NotImplemented,
                new
                {
                    message =
                        "User management is pending integration with " +
                        "the shared User database and authentication service."
                });
        }

        // POST /api/admin/users/register-officer
        [HttpPost("register-officer")]
        public IActionResult RegisterOfficer(
            [FromBody] RegisterOfficerRequestDto request)
        {
            return StatusCode(
                StatusCodes.Status501NotImplemented,
                new
                {
                    message =
                        "Officer account registration is pending integration " +
                        "with the shared User and authentication services."
                });
        }
    }
}
