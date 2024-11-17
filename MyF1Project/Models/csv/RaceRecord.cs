using CsvHelper.Configuration.Attributes;

namespace MyF1Project.Models.csv
{
    public class RaceRecord
    {
        //,,,,,,,,,,,,,,
        [Name("raceId")]
        public int? Id { get; set; }
        [Name("year")]
        public int? Year { get; set; }
        [Name("round")]
        public int? Round { get; set; }
        [Name("circuitId")]
        public int? CircuitId { get; set; }
        [Name("name")]
        public string? name { get; set; }
        [Name("date")]
        public string? date { get; set; }
        [Name("time")]
        public string? Time { get; set; }
        [Name("url")]
        public string? Url { get; set; }
        [Name("fp1_date")]
        public string? Fp1Date { get; set; }
        [Name("fp1_time")]
        public string? Fp1Time { get; set; }
        [Name("fp2_date")]
        public string? Fp2Date { get; set; }
        [Name("fp2_time")]
        public string? Fp2Time { get; set; }
        [Name("fp3_date")]
        public string? Fp3Date { get; set; }
        [Name("fp3_time")]
        public string? Fp3Time { get; set; }
        [Name("quali_date")]
        public string? QualiDate { get; set; }
        [Name("quali_time")]
        public string? QualiTime { get; set; }
        [Name("sprint_date")]
        public string? SprintDate { get; set; }
        [Name("sprint_time")]
        public string? SprintTime { get; set; }
    }
}
