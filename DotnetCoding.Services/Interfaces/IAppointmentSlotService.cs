using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IAppointmentSlotService
    {
        Task<IEnumerable<AppointmentSlot>> GetAppointmentSlotsAsync();

        Task CreateSlotsForProviderAsync(int providerId, DateTime start, DateTime end);
        Task<IEnumerable<AppointmentSlot>> GetAllAsync();
        Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsAsync(int? providerId = null, DateTime? start = null, DateTime? end = null);
        Task<IEnumerable<AppointmentSlot>> GetSlotsByProviderIdAsync(int providerId);
    }
}
