using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DbContextClass dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dbContext.Clients.Include(c => c.Reservations).ToListAsync();
        }
    }
}
