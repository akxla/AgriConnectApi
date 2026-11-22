using System;

namespace AgriConnectApi.DTOs.AgricultureOfficer
{
    public class OfficerProfileDto
    {
        public int OfficerId { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? EmployeeNumber { get; set; }
        public string? Specialization { get; set; }
        public string? District { get; set; }
        public string? Office { get; set; }
        public string? ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}