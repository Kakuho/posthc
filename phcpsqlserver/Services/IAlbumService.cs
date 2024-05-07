using Phc.Data;
using Phc.Data.Dto;

namespace Phc.Service.Interface
{
    public interface IAlbumService
    {
        public Task<Album> GetAlbumByIdAsync(long id);
        public Task<List<Album>> GetAllAlbums();
        public Album AddAlbum(Album album);
        public Task<Album> AddAlbum(AlbumDto album);
        public Task<bool> DeleteAlbum(string name);
    }
}
