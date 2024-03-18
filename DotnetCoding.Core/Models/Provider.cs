
namespace DotnetCoding.Core.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AppointmentSlot> AppointmentSlots { get; set; }
    }
}
