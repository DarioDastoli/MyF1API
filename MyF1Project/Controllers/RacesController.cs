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
            [FromQuery] RequestDTO input)
        {
            var query = _context.Races.AsQueryable();
            if(!string.IsNullOrEmpty(input.FilterQuery))
                query = query.Where(r => r.Id.Equals(Int32.Parse(input.FilterQuery)));

            query = query
                .OrderBy($"{input.SortColumn} {input.SortOrder}")
                .Skip(input.PageIndex * input.PageSize)
                .Take(input.PageSize);


            return new RestDTO<Race[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                RecordCount = await _context.Races.CountAsync(),
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                        Url.Action(
                            null, 
                            "Races",
                            new{ input.PageIndex, input.PageSize}, 
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

    }
}
