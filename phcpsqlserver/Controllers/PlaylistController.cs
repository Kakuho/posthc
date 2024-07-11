using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;
using Phc.Exceptions;

namespace Phc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly PhcContext _context;
        private readonly IPlaylistService _playlistservice;

        public PlaylistController(PhcContext context, IPlaylistService playlistservice)
        {
            _context = context;
            _playlistservice = playlistservice;
        }

        [HttpGet()]
        public async Task<ActionResult<List<PlaylistResponseDto>>> GetAllPlaylists()
        {
          List<PlaylistResponseDto> payload = await _playlistservice.GetAllPlaylistsAsync();
          return Ok(payload);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistResponseDto>> GetPlaylistById(long id)
        {
            try{
                PlaylistResponseDto payload = await _playlistservice.GetPlaylistByIdAsync(id);
                return payload;
            }
            catch(PlaylistNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistResponseDto>> PostPlaylist(PlaylistInputDto input)
        {
            try{
                PlaylistResponseDto payload = await _playlistservice.AddPlaylist(input);
                return payload;
            }
            catch(PlaylistExistsException e){
                return new JsonResult(new ErrorDto(){StatusCode = 400, Error = e.Message}){StatusCode = 400};
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(long id)
        {
            try{
                await _playlistservice.DeletePlaylist(id);
                return Ok();
            }
            catch(PlaylistNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
            
        }

        // probably make this async
        [HttpPut("{id}")]
        public async Task<ActionResult<PlaylistResponseDto>> PutAlbum(int id, [FromBody] PlaylistInputDto input)
        {
            try{
                PlaylistResponseDto payload = await _playlistservice.UpdatePlaylist(id, input);
                return Ok();
            }
            catch(PlaylistNotFoundException e){
                return new JsonResult(new ErrorDto(){StatusCode = 404, Error = e.Message}){StatusCode = 404};
            }
        }
    }
}
