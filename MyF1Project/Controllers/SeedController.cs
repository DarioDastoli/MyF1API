
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyF1Project.Models;
using MyF1Project.Models.csv;
using System.Globalization;

namespace MyF1Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public SeedController(
            ApplicationDbContext context,
            IWebHostEnvironment env,
            ILogger<SeedController> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        //[HttpPut(Name = "Seed")]
        //[ResponseCache(NoStore = true)]
        //public async Task<JsonResult> Put()
        //{
        //    var config = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
        //    {
        //        HasHeaderRecord = true,
        //        Delimiter = ",",
        //    };

        //    using var driversReader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/drivers.csv"));
        //    using var driversCsv = new CsvReader(driversReader, config);

        //    var existingDrivers = await _context.Drivers
        //        .ToDictionaryAsync(d => d.Id);
        //    var driverRecords = driversCsv.GetRecords<driversRecord>();
        //    var skippedDriverRows = 0;

        //    foreach (var driverRecord in driverRecords)
        //    {
        //        if (!existingDrivers.IsNullOrEmpty() && existingDrivers.ContainsKey(driverRecord.driverId.Value))
        //        {
        //            skippedDriverRows++;
        //            continue;
        //        }
        //        var driver = new Driver()
        //        {
        //            Id = driverRecord.driverId.Value,
        //            DriverRef = driverRecord.driverRef,
        //            Number = driverRecord.number == "\\N" ? 0 : Int32.Parse(driverRecord.number),
        //            Code = driverRecord.code,
        //            Forename = driverRecord.forename,
        //            Surname = driverRecord.surname,
        //            DOB = DateTime.ParseExact(driverRecord.dob, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), //1984-12-07
        //            Nationality = driverRecord.nationality,
        //            Url = driverRecord.url
        //        };
        //        _context.Drivers.Add(driver);
        //    }

        //    _context.SaveChanges();

        //    return new JsonResult(new
        //    {
        //        Drivers = _context.Drivers.Count(),
        //        SkippedDriverRows = skippedDriverRows,
        //    });

        //}

        //[HttpPut(Name = "SeedRaces")]
        //[ResponseCache(NoStore = true)]
        //public async Task<JsonResult> Put()
        //{
        //    var config = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
        //    {
        //        HasHeaderRecord = true,
        //        Delimiter = ",",
        //    };

        //    using var racesReader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/races.csv"));
        //    using var racesCsv = new CsvReader(racesReader, config);

        //    var existingRaces = await _context.Races
        //        .ToDictionaryAsync(r => r.Id);
        //    var raceRecords = racesCsv.GetRecords<RaceRecord>();
        //    var skippedRaceRows = 0;

        //    foreach (var raceRecord in raceRecords)
        //    {
        //        if (existingRaces.ContainsKey(raceRecord.Id.Value))
        //        {
        //            skippedRaceRows++;
        //            continue;
        //        }
        //        var race = new Race()
        //        {
        //            Id = raceRecord.Id.Value,
        //            Year = raceRecord.Year.Value,
        //            Round = raceRecord.Round.Value,
        //            CircuitId = raceRecord.circuitId.Value,
        //            name = raceRecord.name,
        //            date = raceRecord.date,
        //            Time = raceRecord.Time,
        //            Url = raceRecord.url,
        //            Fp1Date = raceRecord.Fp1Date,
        //            Fp1Time = raceRecord.Fp1Time,
        //            Fp2Date = raceRecord.Fp2Date,
        //            Fp2Time = raceRecord.Fp2Time,
        //            Fp3Date = raceRecord.Fp3Date,
        //            Fp3Time = raceRecord.Fp3Time,
        //            QualiDate = raceRecord.QualiDate,
        //            QualiTime = raceRecord.QualiTime,
        //            SprintDate = raceRecord.SprintDate,
        //            SprintTime = raceRecord.SprintTime

        //        };
        //        _context.Races.Add(race);
        //    }
        //    _context.SaveChanges();

        //    return new JsonResult(new
        //    {
        //        Races = _context.Races.Count(),
        //        SkippedRaceRows = skippedRaceRows,
        //    });

        //}


        [HttpPut(Name = "Seed")]
        [ResponseCache(NoStore = true)]
        public async Task<JsonResult> Put()
        {
            #region SETUP
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };

            using var racesReader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/races.csv"));
            using var racesCsv = new CsvReader(racesReader, config);
            using var lapTimesReader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/lap_times.csv"));
            using var lapTimesCsv = new CsvReader(lapTimesReader, config);
            using var driversReader = new StreamReader(Path.Combine(_env.ContentRootPath, "Data/drivers.csv"));
            using var driversCsv = new CsvReader(driversReader, config);

            var existingRaces = await _context.Races
                .ToDictionaryAsync(r => r.Id);

            var existingLapTimes = await _context.Laptimes
                .ToDictionaryAsync(lt => lt.Id);

            var existingReader = await _context.Drivers
                .ToDictionaryAsync(d => d.Id);

            #endregion

            #region EXECUTE
            var raceRecords = racesCsv.GetRecords<RaceRecord>();
            var laptimeRecords = lapTimesCsv.GetRecords<LaptimeRecord>();
            var driverRecords = driversCsv.GetRecords<driversRecord>();
            var skippedRaceRows = 0;
            var skippedLapRows = 0;
            var skippedDriverRows = 0;

            foreach (var raceRecord in raceRecords)
            {
                if (!existingRaces.IsNullOrEmpty()
                    || existingRaces.ContainsKey(raceRecord.Id.Value)
                    )
                {
                    skippedRaceRows++;
                    continue;
                }
                var race = new Race()
                {
                    Id = raceRecord.Id.Value,
                    Year = raceRecord.Year.Value,
                    Round = raceRecord.Round.Value,
                    CircuitId = raceRecord.circuitId.Value,
                    name = raceRecord.name,
                    date = raceRecord.date,
                    Time = raceRecord.Time,
                    Url = raceRecord.url,
                    Fp1Date = raceRecord.Fp1Date,
                    Fp1Time = raceRecord.Fp1Time,
                    Fp2Date = raceRecord.Fp2Date,
                    Fp2Time = raceRecord.Fp2Time,
                    Fp3Date = raceRecord.Fp3Date,
                    Fp3Time = raceRecord.Fp3Time,
                    QualiDate = raceRecord.QualiDate,
                    QualiTime = raceRecord.QualiTime,
                    SprintDate = raceRecord.SprintDate,
                    SprintTime = raceRecord.SprintTime

                };
                _context.Races.Add(race);
            }

            using var racesTransaction = _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Races ON");
            await _context.SaveChangesAsync();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Races OFF");
            racesTransaction.Commit();

            foreach (var driverRecord in driverRecords)
            {
                if (!existingLapTimes.IsNullOrEmpty() || existingLapTimes.ContainsKey(driverRecord.driverId.Value))
                {
                    skippedDriverRows++;
                    continue;
                }
                var driver = new Driver()
                {
                    Id = driverRecord.driverId.Value,
                    DriverRef = driverRecord.driverRef,
                    Number = driverRecord.number == "\\N" ? 0 : Int32.Parse(driverRecord.number),
                    Code = driverRecord.code,
                    Forename = driverRecord.forename,
                    Surname = driverRecord.surname,
                    DOB = DateTime.ParseExact(driverRecord.dob, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture), //1984-12-07
                    Nationality = driverRecord.nationality,
                    Url = driverRecord.url
                };
                _context.Drivers.Add(driver);
            }

            using var driversTransaction = _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Drivers ON");
            await _context.SaveChangesAsync();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Drivers OFF");
            driversTransaction.Commit();
            var laptimesList = new List<Laptime>();
            var id = 0;
            foreach (var laptimeRecord in laptimeRecords)
            {
                if (existingLapTimes.ContainsKey(laptimeRecord.raceId.Value)
                    && existingLapTimes.ContainsKey(laptimeRecord.driverId.Value)
                    && existingLapTimes.ContainsKey(laptimeRecord.lap.Value))
                {
                    skippedLapRows++;
                    continue;
                }
                id++;

                var lapTime = new Laptime()
                {
                    Id = id,
                    Lap = laptimeRecord.lap.Value,
                    Position = laptimeRecord.position.Value,
                    Time = laptimeRecord.time,
                    Milliseconds = laptimeRecord.milliseconds.Value,
                    Race = _context.Races.Single(r => r.Id == laptimeRecord.raceId),
                    Driver = _context.Drivers.Single(d => d.Id == laptimeRecord.driverId)
                };
                laptimesList.Add(lapTime);
        
            }
            _context.Laptimes.AddRange(laptimesList);

            using var lapTimesTransaction = _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Laptimes ON");
            await _context.SaveChangesAsync();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Laptimes OFF");
            lapTimesTransaction.Commit();
            #endregion

            return new JsonResult(new
            {
                Races = _context.Races.Count(),
                Drivers = _context.Drivers.Count(),
                Laptimes = _context.Laptimes.Count(),
                SkippedRaceRows = skippedRaceRows,
                SkippedDriverRows = skippedDriverRows,
                SkippedLaptimesRows = skippedLapRows
            });

        }

    }
}
