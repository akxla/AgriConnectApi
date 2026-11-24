
using System;

namespace AgriConnectApi.DTOs.OfficerDashboard
{
    public class RecentRecommendationDto
    {
        public int RecommendationId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
