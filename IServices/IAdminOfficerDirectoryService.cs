
using System.Collections.Generic;
using System.Threading.Tasks;
using AgriConnectApi.DTOs.Admin;

namespace AgriConnectApi.IServices
{
    public interface IAdminOfficerDirectoryService
    {
        Task<List<AdminOfficerDto>> GetAllOfficersAsync();

        Task<AdminOfficerDto?> GetOfficerByIdAsync(int officerId);
    }
}
