using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class NewTable
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public int Capacity { get; set; }
        public bool IsAvailable = true;
    }
}
