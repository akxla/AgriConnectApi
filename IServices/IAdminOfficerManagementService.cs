
using System.Threading.Tasks;
using AgriConnectApi.DTOs.Admin;

namespace AgriConnectApi.IServices
{
    public interface IAdminOfficerManagementService
    {
        Task<AdminOfficerDto?> UpdateOfficerStatusAsync(
            int officerId,
            bool isActive);
    }
}
