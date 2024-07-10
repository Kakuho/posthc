using Phc.Data;
using Phc.Data.Dto;

namespace Phc.Service.Interface
{
    public interface IAlbumService
    {
        public Task<AlbumResponseDto> GetAlbumByIdAsync(long id);
        public Task<List<AlbumResponseDto>> GetAllAlbums();
        public Task<Album> AddAlbum(Album album);
        public Task<AlbumResponseDto> AddAlbum(AlbumInputDto album);
        public Task<bool> DeleteAlbum(int id);
        public Task<List<AlbumResponseDto>> GetAlbumsFromBand(string BandName);
        public Task UpdateAlbum(int id, AlbumInputDto details);
        // some helper functions
        public Task<bool> CheckBandExists(string bandName);
    }
}
