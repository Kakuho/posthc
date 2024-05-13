using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;

namespace Phc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly PhcContext _context;
        private readonly IAlbumService _albumservice;

        public AlbumsController(PhcContext context, IAlbumService albumservice)
        {
            _context = context;
            _albumservice = albumservice;
        }

        // probably make this async
        [HttpGet()]
        public async Task<ActionResult<List<Album>>> GetAlbums(string? bandname)
        {
            List<Album> albumlist;
            if (bandname is not null)
            {
              albumlist = await _albumservice.GetAlbumsFromBand(bandname);
            }
            else
            {
              albumlist = await _albumservice.GetAllAlbums();
            }
            if (albumlist is not null)
            {
                return Ok(albumlist);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbumById(long id)
        {
            Album a = await _albumservice.GetAlbumByIdAsync(id);
            if (a is null)
            {
                return new BadRequestResult();
            }
            else
            {
                return Ok(a);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(AlbumDto album)
        {
            Album saved = await _albumservice.AddAlbum(album);
            return new CreatedAtActionResult(nameof(PostAlbum), "Albums", new { id = saved.Id }, saved);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAlbum(string albumname)
        {
            bool deleted = await _albumservice.DeleteAlbum(albumname);
            if (deleted)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<string> PutAlbum(int id)
        {
          return Ok("I Aint Implemented yet!");
        }

    }
}
