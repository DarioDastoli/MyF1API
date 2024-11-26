using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyF1Project.Attributes;
using MyF1Project.DTO;
using MyF1Project.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;

namespace MyF1Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private readonly ApplicationDbContext _context;

        public DriversController(
            ILogger<DriversController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 6)]
        public async Task<RestDTO<Driver[]>> Get(
            int pageIndex = 0,
            [Range(1, 100)] int pageSize = 10,
            [SortColumnValidator(typeof(DriverDTO))]string? sortColumn = "Id",
            [SortOrderValidator] string? sortOrder = "ASC",
            string? filterQuery = null)
        {
            var query = _context.Drivers.AsQueryable();

            if (!string.IsNullOrEmpty(filterQuery))
            {
                query = query.Where(d =>
                    d.DriverRef.ToUpper()
                    .Equals(filterQuery.ToUpper()));

            };

            query = query
                .OrderBy($"{sortColumn} {sortOrder}")
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return new RestDTO<Driver[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = await _context.Races.CountAsync(),
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "Drivers",
                            new{ pageIndex, pageSize},
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

        [HttpPost]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<Driver?>> Post(DriverDTO model)
        {
            var driver = await _context.Drivers
                .Where(d => d.Id == model.id)
                .FirstOrDefaultAsync();

            if (driver != null)
            {
                if (!string.IsNullOrEmpty(model.DriverRef))
                {
                    driver.DriverRef = model.DriverRef;
                }
                _context.Drivers.Update(driver);
                await _context.SaveChangesAsync();

            }

            return new RestDTO<Driver?>()
            {
                Data = driver,
                Links = new List<LinkDTO>
                    {
                        new LinkDTO(
                            Url.Action(
                            null,
                            "Drivers",
                            model,
                            Request.Scheme)!,
                            "self",
                            "POST"),
                    }
            };
        }
    }
}
