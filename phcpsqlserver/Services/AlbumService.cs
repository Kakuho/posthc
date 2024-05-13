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
            Album a = await _context.Albums
                        .SingleOrDefaultAsync(a => a.Id == id) 
                        ?? throw new ArgumentNullException("Band is null");
            return a;
        }

        public async Task<List<Album>> GetAllAlbums()
        {
            return await _context.Albums.ToListAsync();
        }

        public Album AddAlbum(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
            return album;
        }

        // this code is creating a circular reference
        // album -> band -
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
              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.WriteLine(album.BandName);
              Band band = await _bandservice.GetBandByNameAsync(album.BandName);
              if(band is null){
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ERROR CANNOT FIND");
              }
              else{
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("band is not null");
                saveAlbum.bandId = band.Id;
                saveAlbum.Band = band;
              }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("we outside");
            _context.Albums.Add(saveAlbum);
            _context.SaveChanges();
            return saveAlbum;
        }

        public async Task<bool> DeleteAlbum(string name)
        {
            Album album = await _context.Albums
                            .FirstOrDefaultAsync(a => a.Name == name) 
                            ?? throw new ArgumentNullException("Album Name look up returned null");
            _context.Albums.Remove(album);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<Album>> GetAlbumsFromBand(string? BandName)
        {
            if(BandName is not null){
              var albums = await _context.Albums
                            .Where(a => a.Band.Name == BandName)
                            .ToListAsync() 
                            ?? throw new ArgumentNullException("there are no albums for that band");
              return albums;
            }
            else{
              throw new ArgumentNullException("Bandname is null");
            }
        }
    }
}
