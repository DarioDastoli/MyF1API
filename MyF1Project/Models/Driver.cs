using System.ComponentModel.DataAnnotations.Schema;

namespace MyF1Project.Models
{
    [Table("Drivers")]
    public class Driver
    {
        public int Id { get; set; }
        public string DriverRef { get; set; }
        public int Number { get; set; }
        public string Code{ get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DOB { get; set; }
        public string Nationality { get; set; }
        public string Url { get; set; }
        public ICollection<Laptime> Laptime { get; set; } 
    }
}
