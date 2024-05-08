using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;

namespace Phc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumFromBandController : ControllerBase
    {
        // this class exposes a resource which represents a collection of albums from a particular band
        private readonly PhcContext _context;
        private readonly IAlbumService _albumservice;

        public AlbumFromBandController(PhcContext context, IAlbumService albumservice)
        {
            _context = context;
            _albumservice = albumservice;
        }

        [HttpGet("{BandName}")]
        public async Task<ActionResult<Band>> GetAllAlbumFromBand(string BandName)
        {
            var albumlist = await _albumservice.GetAlbumsFromBand(BandName);
            if (albumlist is null)
            {
                return new BadRequestResult();
            }
            else
            {
                return Ok(albumlist);
            }
        }

        [HttpPost("{BandName}")]
        public async Task<ActionResult<Band>> AddAnAlbumToBand(string BandName)
        {
            var albumlist = await _albumservice.GetAlbumsFromBand(BandName);
            if (albumlist is null)
            {
                return new BadRequestResult();
            }
            else
            {
                return Ok(albumlist);
            }
        }

    }
}
