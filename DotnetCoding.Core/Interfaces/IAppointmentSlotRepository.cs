using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IAppointmentSlotRepository : IGenericRepository<AppointmentSlot>
    {
        Task RemoveOldSlots(DateTime cutoffDate);
    }
}
