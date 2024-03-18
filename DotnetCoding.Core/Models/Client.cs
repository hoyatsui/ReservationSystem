

using System.Collections.Generic;

namespace DotnetCoding.Core.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
