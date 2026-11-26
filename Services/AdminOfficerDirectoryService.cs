
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AgriConnectApi.Data;
using AgriConnectApi.DTOs.Admin;
using AgriConnectApi.IServices;

using Microsoft.EntityFrameworkCore;

namespace AgriConnectApi.Services
{
    public class AdminOfficerDirectoryService
        : IAdminOfficerDirectoryService
    {
        private readonly AppDbContext _context;

        public AdminOfficerDirectoryService(AppDbContext context)
        {
            _context = context;
        }

        // Get all Agriculture Officer profiles
        public async Task<List<AdminOfficerDto>> GetAllOfficersAsync()
        {
            return await _context.AgricultureOfficers
                .AsNoTracking()
                .OrderBy(o => o.FullName)
                .Select(o => new AdminOfficerDto
                {
                    OfficerId = o.OfficerId,
                    UserId = o.UserId,
                    FullName = o.FullName,
                    EmployeeNumber = o.EmployeeNumber,
                    Specialization = o.Specialization,
                    District = o.District,
                    Office = o.Office,
                    ContactNumber = o.ContactNumber,
                    IsActive = o.IsActive,
                    CreatedAt = o.CreatedAt
                })
                .ToListAsync();
        }

        // Get one Agriculture Officer by ID
        public async Task<AdminOfficerDto?> GetOfficerByIdAsync(
            int officerId)
        {
            return await _context.AgricultureOfficers
                .AsNoTracking()
                .Where(o => o.OfficerId == officerId)
                .Select(o => new AdminOfficerDto
                {
                    OfficerId = o.OfficerId,
                    UserId = o.UserId,
                    FullName = o.FullName,
                    EmployeeNumber = o.EmployeeNumber,
                    Specialization = o.Specialization,
                    District = o.District,
                    Office = o.Office,
                    ContactNumber = o.ContactNumber,
                    IsActive = o.IsActive,
                    CreatedAt = o.CreatedAt
                })
                .FirstOrDefaultAsync();
        }
    }
}
