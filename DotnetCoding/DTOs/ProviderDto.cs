namespace DotnetCoding.DTOs
{
    public class ProviderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> AppointmentSlotIds { get; set; } = new List<int>();
    }
}
