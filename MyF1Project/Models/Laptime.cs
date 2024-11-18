using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyF1Project.Models
{
    [Table("Laptimes")]
    public class Laptime
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int Lap { get; set; }
        public int Position { get; set; }
        public string Time { get; set; }
        public int Milliseconds { get; set; }
        public Race Race { get; set; }
        public Driver Driver { get; set; }
    }
}
