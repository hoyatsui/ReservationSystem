namespace DotnetCoding.DTOs
{
    public class AppointmentSlotDto
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int? ReservationId { get; set; }
        public string ClientName { get; set; }
    }

}
