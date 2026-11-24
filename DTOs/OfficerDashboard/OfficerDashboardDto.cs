
using System.Collections.Generic;

namespace AgriConnectApi.DTOs.OfficerDashboard
{
    public class OfficerDashboardDto
    {
        // Agriculture Officer information
        public int OfficerId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string? EmployeeNumber { get; set; }

        public string? Specialization { get; set; }

        public string? District { get; set; }

        // Dashboard statistics
        public int TotalRecommendations { get; set; }

        public int PublishedRecommendations { get; set; }

        public int DraftRecommendations { get; set; }

        public int ArchivedRecommendations { get; set; }

        // Recent recommendations
        public List<RecentRecommendationDto> RecentRecommendations
            { get; set; } = new();
    }
}
