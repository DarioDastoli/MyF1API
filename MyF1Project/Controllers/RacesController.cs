using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using MyF1Project.DTO;
using MyF1Project.Models;
using System.ComponentModel.DataAnnotations;
using MyF1Project.Attributes;

namespace MyF1Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly ILogger<RacesController> _logger;
        private readonly ApplicationDbContext _context;

        public RacesController(
            ApplicationDbContext context,
            ILogger<RacesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetRaces")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<Race[]>> Get(
            int pageIndex = 0,
            [Range(1, 100)] int pageSize = 10,
            [SortColumnValidator(typeof(Race))]string? sortColumn = "Id",
            [RegularExpression("ASC|DESC")] string? sortOrder = "ASC",
            string? filterQuery = null)
        {
            var query = _context.Races.AsQueryable();
            if(!string.IsNullOrEmpty(filterQuery))
                query = query.Where(r => r.Id.Equals(Int32.Parse(filterQuery)));

            query = query
                .OrderBy($"{sortColumn} {sortOrder}")
                .Skip(pageIndex * pageSize)
                .Take(pageSize);


            return new RestDTO<Race[]>()
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
                            "Races",
                            new{ pageIndex, pageSize}, 
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

    }
}
