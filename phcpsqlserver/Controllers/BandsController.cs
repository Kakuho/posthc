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

        [HttpGet()]
        public async Task<ActionResult<List<Band>>> GetAllBands()
        {
            List<Band> allbands = await _bandservice.GetAllBands();
            if (allbands is null)
            {
                return new BadRequestResult();
            }
            else
            {
                return Ok(allbands);
            }
        }

        /*
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
        */

        [HttpGet("{BandName}")]
        public async Task<ActionResult<Band>> GetBandById(string BandName)
        {
            Band b = await _bandservice.GetBandByNameAsync(BandName);
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
        public async Task<ActionResult<Band>> PostBand(BandDto band)
        {
            Band saved = _bandservice.AddBand(band);
            return new CreatedAtActionResult(nameof(PostBand), "Bands", new { id = saved.Id }, saved);
        }



        [HttpDelete]
        public async Task<ActionResult<bool>> PostBand(string bandname)
        {
            bool deleted = await _bandservice.DeleteBand(bandname);
            if (deleted)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpGet("working")]
        public async Task<ActionResult<Band>> doingsomething()
        {
            return Ok();
        }
    }
}
