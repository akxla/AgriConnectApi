
using System.Linq;
using System.Threading.Tasks;

using AgriConnectApi.Data;
using AgriConnectApi.DTOs.OfficerDashboard;
using AgriConnectApi.IServices;

using Microsoft.EntityFrameworkCore;

namespace AgriConnectApi.Services
{
    public class OfficerDashboardService
        : IOfficerDashboardService
    {
        private readonly AppDbContext _context;

        public OfficerDashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OfficerDashboardDto?> GetDashboardAsync(
            int userId)
        {
            // Step 1: Find the logged-in officer
            var officer = await _context.AgricultureOfficers
                .AsNoTracking()
                .FirstOrDefaultAsync(o =>
                    o.UserId == userId && o.IsActive);

            if (officer == null)
            {
                return null;
            }

            // Step 2: Select only this officer's recommendations
            var recommendations = _context.Recommendations
                .AsNoTracking()
                .Where(r => r.OfficerId == officer.OfficerId);

            // Step 3: Count all recommendations
            int total = await recommendations.CountAsync();

            // Step 4: Count published recommendations
            int published = await recommendations
                .CountAsync(r => r.Status == "Published");

            // Step 5: Count draft recommendations
            int drafts = await recommendations
                .CountAsync(r => r.Status == "Draft");

            // Step 6: Count archived recommendations
            int archived = await recommendations
                .CountAsync(r => r.Status == "Archived");

            // Step 7: Retrieve the latest five recommendations
            var recent = await recommendations
                .OrderByDescending(r => r.CreatedAt)
                .ThenByDescending(r => r.RecommendationId)
                .Take(5)
                .Select(r => new RecentRecommendationDto
                {
                    RecommendationId = r.RecommendationId,
                    Title = r.Title,
                    Category = r.Category,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();

            // Step 8: Return dashboard data
            return new OfficerDashboardDto
            {
                OfficerId = officer.OfficerId,
                FullName = officer.FullName,
                EmployeeNumber = officer.EmployeeNumber,
                Specialization = officer.Specialization,
                District = officer.District,

                TotalRecommendations = total,
                PublishedRecommendations = published,
                DraftRecommendations = drafts,
                ArchivedRecommendations = archived,

                RecentRecommendations = recent
            };
        }
    }
}
