
using System.Threading.Tasks;
using AgriConnectApi.DTOs.AdminDashboard;

namespace AgriConnectApi.IServices
{
    public interface IAdminDashboardService
    {
        Task<AdminDashboardDto> GetDashboardAsync();
    }
}
