using CsvHelper.Configuration.Attributes;

namespace MyF1Project.Models.csv
{
    public class driversRecord
    {
        //driverId,driverRef,number,code,forename,surname,dob,nationality,url
        [Name("driverId")]
        public int? driverId { get; set; }
        [Name("driverRef")]
        public string? driverRef { get; set; }
        [Name("number")]
        public string? number { get; set; }
        [Name("code")]
        public string? code { get; set; }
        [Name("forename")]
        public string? forename { get; set; }
        [Name("surname")]
        public string? surname { get; set; }
        [Name("dob")]
        public string? dob { get; set; }
        [Name("nationality")]
        public string? nationality { get; set; }
        [Name("url")]
        public string? url { get; set; }
    }
}
