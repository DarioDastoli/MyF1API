using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyF1Project.DTO;
using MyF1Project.Models;

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
        public async Task<RestDTO<Race[]>> Get()
        {
            var query = _context.Races;
            return new RestDTO<Race[]>()
            {
                Data = await query.ToArrayAsync(),
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                        Url.Action(null, "Races", null, Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }
    }
}
