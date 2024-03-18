namespace DotnetCoding.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> ReservationIds { get; set; } = new List<int>();
    }
}
