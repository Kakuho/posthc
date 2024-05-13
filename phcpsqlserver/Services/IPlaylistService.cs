using Phc.Data;
using Phc.Data.Dto;

namespace Phc.Service.Interface
{
    public interface IPlaylistService
    {
        public Task<Playlist> GetPlaylistByIdAsync(long id);
        public Task<Playlist> GetPlaylistByNameAsync(string name);
        public Task<List<Playlist>> GetAllPlaylists();
        public Playlist AddPlaylist(Playlist playlist);
       //public Playlist AddPlaylist(PlaylistDto pl);
        public Task<bool> DeletePlaylist(string name);
    }
}
