
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AgriConnectApi.Data;
using AgriConnectApi.Models;
using AgriConnectApi.DTOs.Recommendation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectApi.Controllers
{
    [ApiController]
    [Route("api/recommendations")]
    public class RecommendationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecommendationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/recommendations
        // Public users see only published recommendations
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var recommendations = await _context.Recommendations
                .AsNoTracking()
                .Where(r => r.Status == "Published")
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new RecommendationResponseDto
                {
                    RecommendationId = r.RecommendationId,
                    OfficerId = r.OfficerId,
                    Title = r.Title,
                    Description = r.Description,
                    Category = r.Category,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                })
                .ToListAsync();

            return Ok(recommendations);
        }

        // GET: api/recommendations/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var recommendation = await _context.Recommendations
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.RecommendationId == id &&
                    r.Status == "Published");

            if (recommendation == null)
            {
                return NotFound(new
                {
                    message = "Published recommendation not found."
                });
            }

            return Ok(ToDto(recommendation));
        }

        // GET: api/recommendations/mine
        [HttpGet("mine")]
        [Authorize(Roles = "AgricultureOfficer")]
        public async Task<IActionResult> GetMine()
        {
            var officer = await GetCurrentOfficerAsync();

            if (officer == null)
                return Forbid();

            var recommendations = await _context.Recommendations
                .AsNoTracking()
                .Where(r => r.OfficerId == officer.OfficerId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new RecommendationResponseDto
                {
                    RecommendationId = r.RecommendationId,
                    OfficerId = r.OfficerId,
                    Title = r.Title,
                    Description = r.Description,
                    Category = r.Category,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                })
                .ToListAsync();

            return Ok(recommendations);
        }

        // POST: api/recommendations
        [HttpPost]
        [Authorize(Roles = "AgricultureOfficer")]
        public async Task<IActionResult> Create(
            [FromBody] CreateRecommendationDto dto)
        {
            var officer = await GetCurrentOfficerAsync();

            if (officer == null)
                return Forbid();

            if (string.IsNullOrWhiteSpace(dto.Title) ||
                string.IsNullOrWhiteSpace(dto.Description))
            {
                return BadRequest(new
                {
                    message = "Title and description are required."
                });
            }

            var recommendation = new Recommendation
            {
                OfficerId = officer.OfficerId,
                Title = dto.Title.Trim(),
                Description = dto.Description.Trim(),
                Category = dto.Category?.Trim(),
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            _context.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();

            return Created(
                $"/api/recommendations/{recommendation.RecommendationId}",
                new
                {
                    message = "Recommendation created successfully.",
                    recommendation = ToDto(recommendation)
                });
        }

        // PUT: api/recommendations/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "AgricultureOfficer")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateRecommendationDto dto)
        {
            var officer = await GetCurrentOfficerAsync();

            if (officer == null)
                return Forbid();

            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(r =>
                    r.RecommendationId == id &&
                    r.OfficerId == officer.OfficerId);

            if (recommendation == null)
                return NotFound(new
                {
                    message = "Your recommendation was not found."
                });

            if (string.IsNullOrWhiteSpace(dto.Title) ||
                string.IsNullOrWhiteSpace(dto.Description))
            {
                return BadRequest(new
                {
                    message = "Title and description are required."
                });
            }

            recommendation.Title = dto.Title.Trim();
            recommendation.Description = dto.Description.Trim();
            recommendation.Category = dto.Category?.Trim();
            recommendation.Status = dto.Status;
            recommendation.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Recommendation updated successfully.",
                recommendation = ToDto(recommendation)
            });
        }

        // DELETE: api/recommendations/5
        // Soft delete: archive rather than physically delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "AgricultureOfficer")]
        public async Task<IActionResult> Archive(int id)
        {
            var officer = await GetCurrentOfficerAsync();

            if (officer == null)
                return Forbid();

            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(r =>
                    r.RecommendationId == id &&
                    r.OfficerId == officer.OfficerId);

            if (recommendation == null)
                return NotFound(new
                {
                    message = "Your recommendation was not found."
                });

            if (recommendation.Status == "Archived")
            {
                return BadRequest(new
                {
                    message = "Recommendation is already archived."
                });
            }

            recommendation.Status = "Archived";
            recommendation.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Recommendation archived successfully."
            });
        }

        // Find the current officer using the JWT's UserId
        private async Task<AgricultureOfficer?>
            GetCurrentOfficerAsync()
        {
            var claimValue = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(claimValue, out int userId))
                return null;

            return await _context.AgricultureOfficers
                .AsNoTracking()
                .FirstOrDefaultAsync(o =>
                    o.UserId == userId && o.IsActive);
        }

        // Convert database entity into response DTO
        private static RecommendationResponseDto ToDto(
            Recommendation recommendation)
        {
            return new RecommendationResponseDto
            {
                RecommendationId = recommendation.RecommendationId,
                OfficerId = recommendation.OfficerId,
                Title = recommendation.Title,
                Description = recommendation.Description,
                Category = recommendation.Category,
                Status = recommendation.Status,
                CreatedAt = recommendation.CreatedAt,
                UpdatedAt = recommendation.UpdatedAt
            };
        }
    }
}
