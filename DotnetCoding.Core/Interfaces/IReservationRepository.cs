using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {

        Task RemoveOldReservations(DateTime cutoffDate);
    }


}
