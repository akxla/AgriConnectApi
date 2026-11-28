
using System;
using System.Threading.Tasks;

using AgriConnectApi.Data;
using AgriConnectApi.DTOs.AdminDashboard;
using AgriConnectApi.IServices;

using Microsoft.EntityFrameworkCore;

namespace AgriConnectApi.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly AppDbContext _context;

        public AdminDashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardDto> GetDashboardAsync()
        {
            var totalOfficers = await _context.AgricultureOfficers
                .CountAsync();

            var activeOfficers = await _context.AgricultureOfficers
                .CountAsync(o => o.IsActive);

            var totalRecommendations = await _context.Recommendations
                .CountAsync();

            var publishedRecommendations =
                await _context.Recommendations
                    .CountAsync(r => r.Status == "Published");

            var draftRecommendations =
                await _context.Recommendations
                    .CountAsync(r => r.Status == "Draft");

            var archivedRecommendations =
                await _context.Recommendations
                    .CountAsync(r => r.Status == "Archived");

            return new AdminDashboardDto
            {
                TotalOfficers = totalOfficers,

                ActiveOfficers = activeOfficers,

                InactiveOfficers = totalOfficers - activeOfficers,

                TotalRecommendations = totalRecommendations,

                PublishedRecommendations = publishedRecommendations,

                DraftRecommendations = draftRecommendations,

                ArchivedRecommendations = archivedRecommendations,

                GeneratedAtUtc = DateTime.UtcNow
            };
        }
    }
}
