using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;
using Phc.Utilities;
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

        /*
        // this code is creating a circular reference
        // album -> band -
        public async Task<Album> AddAlbum(AlbumDto album)
        {
            // first check if the band is in the db
            Band band = await _bandservice.GetBandByNameAsync(album.BandName);
            if(band is null){
                return null;
            }
            // now we populate the db
            Album saveAlbum = new Album()
            {
                Name = album.Name,
                Band = band,
                Runtime = album.Runtime,
                AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
            _context.Albums.Add(saveAlbum);
            _context.SaveChanges();
            return saveAlbum;
        }
        */

        public async Task<Album> AddAlbum/*Sanitised*/(AlbumDto album)
        {
            // first check if the band is in the db
           
            bool bandExists = false;
            CancellationTokenSource bandSource = new CancellationTokenSource();
            CancellationToken token = bandSource.Token;
            await _context.Bands.ForEachAsync(b => {
                if(StringSanitiser.Compare(b.Name, album.BandName)) {
                    Console.WriteLine("yes");
                    bandExists = true;
                    bandSource.Cancel();
                }
            }, token);

            // check to see if the album is already in the db
            
            bool albumExists = false;
            CancellationTokenSource albumSource = new CancellationTokenSource();
            token = albumSource.Token;
            await _context.Albums.ForEachAsync(a => {
                if(StringSanitiser.Compare(a.Name, album.Name)){
                    albumExists = true;
                    albumSource.Cancel();
                }
            }, token);

            if(albumExists || !bandExists){
                throw new InvalidOperationException();
            }

            // passes tests, so we populate the db

            Band band = await _bandservice.GetBandByNameAsync(album.BandName);
            Album saveAlbum = new Album()
            {
                Name = album.Name,
                Band = band,
                Runtime = album.Runtime,
                AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
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
