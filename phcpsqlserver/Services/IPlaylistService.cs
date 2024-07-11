using Phc.Data;
using Phc.Data.Dto;

namespace Phc.Service.Interface
{
    public interface IPlaylistService
    {
        public Task<List<PlaylistResponseDto>> GetAllPlaylistsAsync();
        public Task<PlaylistResponseDto> GetPlaylistByIdAsync(long id);
        public Task<PlaylistResponseDto> AddPlaylist(PlaylistInputDto playlist);
        public Task DeletePlaylist(long id);
        public Task<PlaylistResponseDto> UpdatePlaylist(long id, PlaylistInputDto playlist);
    }
}
