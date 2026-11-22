
using System;
using System.Security.Claims;
using System.Threading.Tasks;

using AgriConnectApi.Data;
using AgriConnectApi.DTOs.AgricultureOfficer;
using AgriConnectApi.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/agricultureofficers")]
    [Authorize(Roles = "AgricultureOfficer")]
    public class AgricultureOfficersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgricultureOfficersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/agricultureofficers/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid user identity."
                });
            }

            var officer = await _context.AgricultureOfficers
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.UserId == userId);

            if (officer == null)
            {
                return NotFound(new
                {
                    message = "Agriculture Officer profile not found."
                });
            }

            if (!officer.IsActive)
            {
                return StatusCode(403, new
                {
                    message = "Officer profile is inactive."
                });
            }

            return Ok(ToProfileDto(officer));
        }

        // PUT: api/agricultureofficers/me
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile(
            [FromBody] UpdateOfficerProfileDto dto)
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid user identity."
                });
            }

            var officer = await _context.AgricultureOfficers
                .FirstOrDefaultAsync(o => o.UserId == userId);

            if (officer == null)
            {
                return NotFound(new
                {
                    message = "Agriculture Officer profile not found."
                });
            }

            if (!officer.IsActive)
            {
                return StatusCode(403, new
                {
                    message = "Officer profile is inactive."
                });
            }

            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                return BadRequest(new
                {
                    message = "Full name is required."
                });
            }

            officer.FullName = dto.FullName.Trim();
            officer.Specialization = dto.Specialization?.Trim();
            officer.District = dto.District?.Trim();
            officer.Office = dto.Office?.Trim();
            officer.ContactNumber = dto.ContactNumber?.Trim();

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Officer profile updated successfully.",
                profile = ToProfileDto(officer)
            });
        }

        // Retrieve UserId from authenticated JWT
        private int? GetCurrentUserId()
        {
            string? value = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            return int.TryParse(value, out int userId)
                ? userId
                : null;
        }

        // Convert entity to response DTO
        private static OfficerProfileDto ToProfileDto(
            AgricultureOfficer officer)
        {
            return new OfficerProfileDto
            {
                OfficerId = officer.OfficerId,
                UserId = officer.UserId,
                FullName = officer.FullName,
                EmployeeNumber = officer.EmployeeNumber,
                Specialization = officer.Specialization,
                District = officer.District,
                Office = officer.Office,
                ContactNumber = officer.ContactNumber,
                IsActive = officer.IsActive,
                CreatedAt = officer.CreatedAt
            };
        }
    }
}
