using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DotnetCoding.Services
{
    public class ReservationService : IReservationService
    {
        public IUnitOfWork _unitOfWork;
/*        private readonly IServiceScopeFactory _serviceScopeFactory;
*/        private readonly IDatabase _redisDb;

        public ReservationService(/*IServiceScopeFactory serviceScopeFactory*/IUnitOfWork unitOfWork, IConnectionMultiplexer redisConnection)
        {
            /*_serviceScopeFactory = serviceScopeFactory;*/
            _unitOfWork = unitOfWork;
            _redisDb = redisConnection.GetDatabase();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            /*using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();*/
                var reservations = await _unitOfWork.Reservations.GetAllAsync();

                return reservations;
            /*}*/
        }


        public async Task<Reservation> CreateReservationAsync(int clientId, int appointmentSlotId)
        {
            /*using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();*/
                var slot = await _unitOfWork.Appointments.GetByIdAsync(appointmentSlotId);
                var client = await _unitOfWork.Clients.GetByIdAsync(clientId);
                if (slot == null||client==null || !slot.IsActive || slot.StartTime <= DateTime.Now.AddHours(24))
                {
                    throw new ArgumentException("Invalid appointment slot.");
                }

                var reservation = new Reservation
                {
                    ClientId = clientId,
                    AppointmentSlotId = appointmentSlotId,
                    IsConfirmed = false,
                    CreatedAt = DateTime.Now,
                    AppointmentSlot = slot,
                    Client = client
                };
                await _unitOfWork.Reservations.AddAsync(reservation);

                await _unitOfWork.SaveAsync();
                await _redisDb.StringSetAsync($"reservation:{reservation.Id}", appointmentSlotId, TimeSpan.FromMinutes(1));

                slot.IsActive = false;
                await _unitOfWork.SaveAsync();
                return reservation;
            /*}*/
        }

        public async Task ConfirmReservationAsync(int reservationId)
        {
        /*    using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();*/
                var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
                if (reservation == null || reservation.IsConfirmed)
                {
                    throw new ArgumentException($"Invalid reservation ID: {reservationId}");
                }
                reservation.IsConfirmed = true;
                await _unitOfWork.SaveAsync();

                await _redisDb.KeyDeleteAsync($"reservation:{reservationId}");
           /* }*/
        }
       

        public async Task CancelReservationAsync(int reservationId)
        
        {
          /*  using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();*/
                var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
                if (reservation != null && !reservation.IsConfirmed)
                {
                    var slot = await _unitOfWork.Appointments.GetByIdAsync(reservation.AppointmentSlotId);
                    if (slot != null)
                    {
                        slot.IsActive = true;
                        await _unitOfWork.Reservations.DeleteAsync(reservation);
                        await _unitOfWork.SaveAsync();
                    }
                }
            /*}*/
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            /*using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();*/
                var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId);
                if (reservation != null)
                {
                    await _unitOfWork.Reservations.DeleteAsync(reservation);
                    await _unitOfWork.SaveAsync();
                }
            /*}*/
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByClientIdAsync(int clientId)
        {
            var reservations = await _unitOfWork.Reservations.FindAsync(r => r.ClientId == clientId);
            return reservations;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByProviderIdAsync(int providerId)
        {
            var reservations = await _unitOfWork.Reservations.FindAsync(r => r.AppointmentSlot.ProviderId == providerId);
            return reservations;
        }
        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
           /* using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();*/

                return await _unitOfWork.Reservations.GetByIdAsync(id);
           /* }*/
        }

        public async Task RemoveReservationExpirationKeyAsync(int reservationId)
        {

            var redisKey = $"reservation:{reservationId}";
            await _redisDb.KeyDeleteAsync(redisKey);
        }

    }
}
