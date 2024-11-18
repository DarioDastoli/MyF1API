using CsvHelper.Configuration.Attributes;

namespace MyF1Project.Models.csv
{
    public class LaptimeRecord
    {
        [Name("raceId")]
        public int? raceId { get; set; }
        [Name("driverId")]
        public int? driverId { get; set; }
        [Name("lap")]
        public int? lap { get; set; }
        [Name("position")]
        public int? position { get; set; }
        [Name("time")]
        public string? time { get; set; }
        [Name("milliseconds")]
        public int? milliseconds { get; set; }

    }
}
