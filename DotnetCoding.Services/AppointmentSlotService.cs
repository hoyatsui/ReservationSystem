using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class AppointmentSlotService : IAppointmentSlotService
    {
        public IUnitOfWork _unitOfWork;

        public AppointmentSlotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<AppointmentSlot>> GetAppointmentSlotsAsync()
        {
            var appointmentSlots = await _unitOfWork.Appointments.GetAllAsync(); 
            return appointmentSlots;
        }

        public async Task CreateSlotsForProviderAsync(int providerId, DateTime start, DateTime end)
        {
            if (end <= start)
            {
                throw new ArgumentOutOfRangeException("End time must be after start time");
            }
            var slots = new List<AppointmentSlot>();
            var currentTime = start;
            while (currentTime < end)
            {
                slots.Add(new AppointmentSlot
                {
                    ProviderId = providerId,
                    StartTime = currentTime,
                    EndTime = currentTime.AddMinutes(15),
                    IsActive = true
                });
                currentTime = currentTime.AddMinutes(15);
            }
            foreach (var slot in slots)
            {
                await _unitOfWork.Appointments.AddAsync(slot);
            }
        }

        public async Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsAsync(int? providerId = null, DateTime? start = null, DateTime? end =null)
        {
            var slots = await _unitOfWork.Appointments.FindAsync(
                slot => slot.IsActive &&
                (providerId == null || slot.ProviderId == providerId) &&
                (start == null || slot.StartTime >= start) &&
                (end == null || slot.EndTime <= end));
            return slots;
        }

        public async Task<IEnumerable<AppointmentSlot>> GetSlotsByProviderIdAsync(int providerId)
        {
            var slots = await _unitOfWork.Appointments.FindAsync(
                slot => slot.ProviderId == providerId);
            return slots;
        }


        public async Task<IEnumerable<AppointmentSlot>> GetAllAsync()
        {
            var slots = await _unitOfWork.Appointments.GetAllAsync();
            return slots;
        }
    }
}
