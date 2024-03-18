

using System;
using System.Text.Json.Serialization;

namespace DotnetCoding.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int AppointmentSlotId {  get; set; }
        public int ClientId { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }

        
        public AppointmentSlot AppointmentSlot { get; set; }
        public Client Client { get; set; }
    }
}
