
using System;
using System.Collections.Generic;

namespace DotnetCoding.Core.Models
{
    public class AppointmentSlot
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }

        public Provider Provider { get; set; }
        public Reservation Reservations { get; set; }
    }
}
