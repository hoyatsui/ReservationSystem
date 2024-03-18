namespace DotnetCoding.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsConfirmed { get; set; }
        public string ProviderName { get; set; }
        public string ClientName { get; set; }
    }

}
