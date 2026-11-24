
using System.Threading.Tasks;
using AgriConnectApi.DTOs.OfficerDashboard;

namespace AgriConnectApi.IServices
{
    public interface IOfficerDashboardService
    {
        Task<OfficerDashboardDto?> GetDashboardAsync(int userId);
    }
}
