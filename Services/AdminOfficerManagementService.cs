
using System.Threading.Tasks;

using AgriConnectApi.Data;
using AgriConnectApi.DTOs.Admin;
using AgriConnectApi.IServices;

using Microsoft.EntityFrameworkCore;

namespace AgriConnectApi.Services
{
    public class AdminOfficerManagementService
        : IAdminOfficerManagementService
    {
        private readonly AppDbContext _context;

        public AdminOfficerManagementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminOfficerDto?> UpdateOfficerStatusAsync(
            int officerId,
            bool isActive)
        {
            var officer = await _context.AgricultureOfficers
                .FirstOrDefaultAsync(o => o.OfficerId == officerId);

            if (officer == null)
            {
                return null;
            }

            officer.IsActive = isActive;

            await _context.SaveChangesAsync();

            return new AdminOfficerDto
            {
                OfficerId = officer.OfficerId,
                UserId = officer.UserId,
                FullName = officer.FullName,
                EmployeeNumber = officer.EmployeeNumber,
                Specialization = officer.Specialization,
                District = officer.District,
                Office = officer.Office,
                ContactNumber = officer.ContactNumber,
                IsActive = officer.IsActive,
                CreatedAt = officer.CreatedAt
            };
        }
    }
}
