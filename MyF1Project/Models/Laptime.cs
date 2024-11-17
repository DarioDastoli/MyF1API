using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyF1Project.Models
{
    [Table("Laptimes")]
    public class Laptime
    {
        [Key]
        [Required]
        public Race Race { get; set; }
        [Key]
        [Required]
        public Driver Driver { get; set; }
        [Key]
        [Required]
        public int Lap { get; set; }
        public int Position { get; set; }
        public string Time { get; set; }
        public int Milliseconds { get; set; }
    }
}
