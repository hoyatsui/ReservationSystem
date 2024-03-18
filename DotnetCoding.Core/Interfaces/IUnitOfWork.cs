

namespace DotnetCoding.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProviderRepository Providers { get; }
        IClientRepository Clients { get; }
        IAppointmentSlotRepository Appointments { get; }
        IReservationRepository Reservations { get; }
        Task SaveAsync();
    }
}
