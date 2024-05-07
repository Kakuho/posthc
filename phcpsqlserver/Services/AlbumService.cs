using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Phc.Service
{
    public class AlbumService : IAlbumService
    {
        private PhcContext _context;
        private IBandService _bandservice;

        public AlbumService(PhcContext context, IBandService bandservice)
        {
            _context = context;
            _bandservice = bandservice;
        }

        public async Task<Album> GetAlbumByIdAsync(long id)
        {
            Album a = _context.Albums.SingleOrDefault(a => a.Id == id);
            return a;
        }

        public Task<List<Album>> GetAllAlbums()
        {
            return _context.Albums.ToListAsync();
        }

        public Album AddAlbum(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
            return album;
        }

        public async Task<Album> AddAlbum(AlbumDto album)
        {
            Album saveAlbum = new Album()
            {
                Id = 0, // this does not matter
                Name = album.Name,
                Runtime = album.Runtime,
                AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
            if(album.BandName is not null){
              Band band = await _bandservice.GetBandByNameAsync(album.BandName);
              if(band is null){
                // throw an error
              }
              else{
                saveAlbum.bandId = band.Id;
                saveAlbum.Band = band;
              }
            }
            _context.Albums.Add(saveAlbum);
            _context.SaveChanges();
            return saveAlbum;
        }

        public async Task<bool> DeleteAlbum(string name)
        {
            Album album = await _context.Albums.FirstAsync(a => a.Name == name);
            if (album is null)
            {
                return false;
            }
            else
            {
                _context.Albums.Remove(album);
                _context.SaveChanges();
                return true;
            }
        }
    }
}
