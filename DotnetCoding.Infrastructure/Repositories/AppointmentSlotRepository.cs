using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class AppointmentSlotRepository : GenericRepository<AppointmentSlot>, IAppointmentSlotRepository
    {
        public AppointmentSlotRepository(DbContextClass dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<AppointmentSlot>> GetAllAsync()
        {
            return await _dbContext.AppointmentSlots.Include(a => a.Reservations).ThenInclude(r => r.Client).Include(a => a.Provider).ToListAsync();
        }
        public async Task RemoveOldSlots(DateTime cutoffDate)
        {
            var oldSlots = _dbContext.AppointmentSlots.Where(s => s.StartTime < cutoffDate);
            _dbContext.AppointmentSlots.RemoveRange(oldSlots);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<AppointmentSlot>> FindAsync(Expression<Func<AppointmentSlot, bool>> predicate)
        {
            return await _dbContext.AppointmentSlots.Include(a => a.Reservations).ThenInclude(r => r.Client).Include(a => a.Provider).Where(predicate).ToListAsync();
        }
    }
}
