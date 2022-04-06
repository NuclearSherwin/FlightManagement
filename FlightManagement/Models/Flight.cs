using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightManagement.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int PlaneId { get; set; }
        public DateTime FlightDate { get; set; }
        [Required]
        public string Destination { get; set; }
        
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("PlaneId")]
        public Plane Plane { get; set; }
        
    }
}