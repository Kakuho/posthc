using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;

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
        public async Task<ActionResult<List<Playlist>>> GetAllPlaylists()
        {
          List<Playlist> pls = await _playlistservice.GetAllPlaylists();
          return Ok(pls);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylistById(long id)
        {
          Playlist pl = await _playlistservice.GetPlaylistByIdAsync(id);
          if(pl is not null){
            return Ok(pl);
          }
          else{
            return new BadRequestResult();
          }
        }


        // probably make this async
        [HttpPost]
        public ActionResult<Playlist> PostPlaylist()
        {
           throw new InvalidOperationException("post playlist is not implemented");
        }

        // probably make this async
        [HttpDelete("{id}")]
        public ActionResult<bool> DeletePlaylist(int id)
        {
           throw new InvalidOperationException("delete playlist/{id} is not implemented");
        }

        // probably make this async
        [HttpPut("{id}")]
        public ActionResult<string> PutAlbum(int id)
        {
           throw new InvalidOperationException("put playlist/{id} is not implemented");
        }
    }
}
