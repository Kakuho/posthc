using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;

namespace Phc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly PhcContext _context;
        private readonly IBandService _bandservice;

        public BandsController(PhcContext context, IBandService bandservice)
        {
            _context = context;
            _bandservice = bandservice;
        }

        [HttpGet("test")]
        public string TestString()
        {
            return "ayo here is some string";
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Band>> GetBandById(long id)
        {
            Band b = await _bandservice.GetBandByIdAsync(id);
            if (b is null)
            {
                return new BadRequestResult();
            }
            else
            {
                return Ok(b);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Band>> PostBand(BandDto b)
        {
            Band saved = _bandservice.AddBand(b);
            return new CreatedAtActionResult(nameof(PostBand), "Bands", new { id = saved.Id }, saved);
        }
    }
}
