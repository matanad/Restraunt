using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class NewReservation
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public DateTime ReservationTime { get; set; }
        [Required]
        public int TableNumber { get; set; }
    }
}
