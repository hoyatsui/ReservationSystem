using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByClientIdAsync(int clientId);
        Task<IEnumerable<Reservation>> GetReservationsByProviderIdAsync(int providerId);
        Task<Reservation> CreateReservationAsync(int clientId, int appointmentSlotId);
        Task ConfirmReservationAsync(int reservationId);
        Task CancelReservationAsync(int reservationId);

        Task DeleteReservationAsync(int reservationId);

        Task RemoveReservationExpirationKeyAsync(int reservationId);
    }
}
