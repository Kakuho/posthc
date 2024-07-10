using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;
using Phc.Exceptions;

namespace Phc.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        // Controller for Albums
        private readonly PhcContext _context;
        private readonly IAlbumService _albumservice;

        public AlbumsController(PhcContext context, IAlbumService albumservice)
        {
            _context = context;
            _albumservice = albumservice;
        }

        [HttpGet()]
        public async Task<ActionResult<List<AlbumResponseDto>>> GetAlbums(string? bandname)
        {
            // this controller action allows querying from a band
            if(bandname is not null){
                try 
                {
                    List<AlbumResponseDto> albums = await _albumservice.GetAlbumsFromBand(bandname);
                    return albums.Count == 0 ? NoContent() : Ok(albums);
                }
                catch(BandNotFoundException e){
                    return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
                }
            }
            else{
                // no query from bandname
                List<AlbumResponseDto> albumlist = await _albumservice.GetAllAlbums();
                return Ok(albumlist);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumResponseDto>> GetAlbumById(long id)
        {
            try{
                AlbumResponseDto a = await _albumservice.GetAlbumByIdAsync(id);
                return Ok(a);
            }
            catch(AlbumNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlbumResponseDto>> PostAlbum(AlbumInputDto album)
        {
            try{
                AlbumResponseDto payload = await _albumservice.AddAlbum(album);
                return Ok(payload);
            }
            catch(BandNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
            catch(AlbumExistsException e){
                return new JsonResult(new ErrorDto(){StatusCode = 400, Error = e.Message}){StatusCode = 400};
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            try{
                bool deleted = await _albumservice.DeleteAlbum(id);
                string message = $"album with id {id} successfully deleted";
                return Ok(message);
            }
            catch(AlbumNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, [FromBody] AlbumInputDto album)
        {
            try{
                await _albumservice.UpdateAlbum(id, album);
                return Ok();
            }
            catch(BandNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
        }
    }
}
