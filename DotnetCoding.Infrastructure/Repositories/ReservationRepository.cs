using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContextClass dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _dbContext.Reservations.Include(r => r.Client).Include(r => r.AppointmentSlot).ThenInclude(a=>a.Provider).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> FindAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return await _dbContext.Reservations.Include(r => r.Client).Include(r => r.AppointmentSlot).ThenInclude(a => a.Provider).Where(predicate).ToListAsync();
        }
      

        public async Task RemoveOldReservations(DateTime cutoffDate)
        {
            var oldReservations = _dbContext.Reservations
                .Where(r => r.AppointmentSlot.StartTime < cutoffDate);
            _dbContext.Reservations.RemoveRange(oldReservations);
            await _dbContext.SaveChangesAsync();
        }
    }
}
