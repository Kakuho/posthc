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

        [HttpGet()]
        public async Task<ActionResult<List<Album>>> GetAllAlbums()
        {
            List<Album> allalbums = await _albumservice.GetAllAlbums();
            if (allalbums is null)
            {
                return new BadRequestResult();
            }
            else
            {
                return Ok(allalbums);
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

        [HttpDelete]
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
    }
}
