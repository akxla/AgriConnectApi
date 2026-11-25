
using System;

namespace AgriConnectApi.DTOs.SupplyRequestReview
{
    public class PendingSupplyRequestDto
    {
        public int SupplyRequestId { get; set; }

        public int FarmerId { get; set; }

        public string RequestStatus { get; set; }
            = string.Empty;

        public DateTime? RequestedAt { get; set; }
    }
}
