using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyF1Project.Models
{
    [Table("Races")]
    public class Race
    {
        [Key]
        [Required]
        public int Id { get; set; } 
        public int Year { get; set; }

        public int Round { get; set; }

        public int CircuitId {  get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string Time { get; set; }
        public string Url { get; set; }
        public string Fp1Date { get; set; }
        public string Fp1Time { get; set; }
        public string Fp2Date { get; set; }
        public string Fp2Time { get; set; }
        public string Fp3Date { get; set; }
        public string Fp3Time { get; set; }
        public string QualiDate { get; set; }
        public string QualiTime { get; set; }
        public string SprintDate { get; set; }
        public string SprintTime { get; set; }

        public ICollection<Laptime>? Laptime { get; set; }
    }
}
