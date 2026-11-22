
using System;

namespace AgriConnectApi.DTOs.Recommendation
{
    public class RecommendationResponseDto
    {
        public int RecommendationId { get; set; }
        public int OfficerId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
