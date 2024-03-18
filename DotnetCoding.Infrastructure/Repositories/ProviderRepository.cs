using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(DbContextClass dbContext) : base(dbContext)
        {
        }


        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _dbContext.Providers.Include(p => p.AppointmentSlots).ToListAsync();
        }
    }
}
