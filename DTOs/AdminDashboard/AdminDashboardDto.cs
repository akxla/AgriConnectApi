
using System;

namespace AgriConnectApi.DTOs.AdminDashboard
{
    public class AdminDashboardDto
    {
        public int TotalOfficers { get; set; }

        public int ActiveOfficers { get; set; }

        public int InactiveOfficers { get; set; }

        public int TotalRecommendations { get; set; }

        public int PublishedRecommendations { get; set; }

        public int DraftRecommendations { get; set; }

        public int ArchivedRecommendations { get; set; }

        public DateTime GeneratedAtUtc { get; set; }
    }
}
