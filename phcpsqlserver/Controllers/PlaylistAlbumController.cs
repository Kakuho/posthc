using Microsoft.AspNetCore.Mvc;
using Phc.Data.Dto;
using Phc.Data;
using Phc.Service.Interface;

namespace Phc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistAlbumController : ControllerBase
    {   
        private readonly PhcContext _context;

        public PlaylistAlbumController(PhcContext context)
        {
            _context = context;
        }

        // probably make this async
        [HttpGet()]
        public ActionResult<List<Album>> GetPlaylistAlbums(long? albumId, long? playlistId)
          // get all PlaylistAlbum Entities
        {
          if(albumId is null && playlistId is null){

          }
          else if(albumId is null){

          }
          else if(playlistId is null){

          }

           throw new InvalidOperationException("get playlist/id is not implemented");
        }

        // probably make this async
        [HttpGet("{id}")]
        public ActionResult<List<Album>> GetPlaylistAlbumsById(long id, long? albumId, long? playlistId)
          // get all the albums from a particular playlist
        {
          if(albumId is null && playlistId is null){

          }
          else if(albumId is null){

          }
          else if(playlistId is null){

          }
           throw new InvalidOperationException("get playlist/{id} is not implemented");
        }

        // probably make this async
        [HttpPost]
        public ActionResult<Playlist> PostAlbumsToPlaylist()
          // add an album to a playlist
        {
           throw new InvalidOperationException("post playlist is not implemented");
        }

        // probably make this async
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteAlbumFromPlaylist(string albumname)
          // remove an album from a playlist
        {
           throw new InvalidOperationException("delete playlist is not implemented");
        }
    }
}
