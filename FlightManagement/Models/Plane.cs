using System.ComponentModel.DataAnnotations;

namespace FlightManagement.Models
{
    public class Plane
    {
        [Key]
        public int PlaneId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}