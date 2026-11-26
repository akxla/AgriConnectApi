
using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.DTOs.Admin
{
    public class UpdateOfficerStatusDto
    {
        [Required]
        public bool? IsActive { get; set; }
    }
}
