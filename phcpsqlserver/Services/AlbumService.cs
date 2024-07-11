using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;
using Phc.Utilities;
using Phc.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Phc.Controllers;
using Microsoft.NET.StringTools;

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

        public async Task<AlbumResponseDto> GetAlbumByIdAsync(long id)
        {
            // if the album is null, we throw an exception
            Album a = await _context.Albums
                        .Include(a => a.Band)
                        .FirstOrDefaultAsync(a => a.Id == id) 
                        ?? throw new AlbumNotFoundException($"Album with id {id} cannot be found");
           // construct the response as DTO
            return new AlbumResponseDto(){
                Id = a.Id, 
                Name = a.Name, 
                BandId = a.Band.Id,
                BandName = a.Band.Name, 
                Runtime = a.Runtime
            }; 
        }

        public async Task<List<AlbumResponseDto>> GetAllAlbums()
        {
            List<Album> albums = await _context.Albums.Include(a => a.Band).ToListAsync();
            List<AlbumResponseDto> payload = new List<AlbumResponseDto>(albums.Count);
            foreach(Album a in albums){
                payload.Add(new AlbumResponseDto(){
                    Id = a.Id, 
                    Name = a.Name, 
                    BandId = a.Band.Id,
                    BandName = a.Band.Name,
                    Runtime = a.Runtime,
                    Genre = a.Genre
                    }
                );
            }
            return payload;
        }

        public async Task<List<AlbumResponseDto>> GetAlbumsFromBand(string? BandName)
        {
            if(BandName is not null){
                bool bandExists = await CheckBandExists(BandName);
                if(!bandExists){
                    throw new BandNotFoundException("Band cannot be found");
                }
                List<Album> albums = await _context.Albums
                                        .Include(a => a.Band)
                                        .Where(a => a.Band.Name == BandName)
                                        .ToListAsync();
                // now construct the payload
                List<AlbumResponseDto> payload = new List<AlbumResponseDto>();
                foreach(Album a in albums){
                    payload.Add(new AlbumResponseDto(){
                        Id = a.Id, 
                        Name = a.Name, 
                        BandId = a.Band.Id,
                        BandName = a.Band.Name,
                        Runtime = a.Runtime,
                        Genre = a.Genre
                        }
                    );
                }
              return payload;
            }
            else{
              throw new BandNotFoundException("ERROR: input band name is null");
            }
        }

        public async Task<Album> AddAlbum(Album album)
        {
            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task<bool> CheckBandExists(string bandName){
            // helper method to check if theres a band with the required band name in the db
            bool bandExists = false;
            CancellationTokenSource bandSource = new CancellationTokenSource();
            CancellationToken token = bandSource.Token;
            await _context.Bands.ForEachAsync(b => {
                if(StringSanitiser.Compare(b.Name, bandName)) {
                    bandExists = true;
                    bandSource.Cancel();
                }
            }, token);
            return bandExists;
        }

        public async Task<bool> CheckAlbumExists(string albumName){
            // helper method to check if there's an album with the band name already in the db
            bool albumExists = false;
            CancellationTokenSource albumSource = new CancellationTokenSource();
            CancellationToken token = albumSource.Token;
            await _context.Albums.ForEachAsync(a => {
                if(StringSanitiser.Compare(a.Name, albumName)){
                    albumExists = true;
                    albumSource.Cancel();
                }
            }, token);
            return albumExists;
        }

        public async Task<AlbumResponseDto> AddAlbum(AlbumInputDto album)
        {
            // checks to see if the band is in the db
            bool bandExists = await CheckBandExists(album.BandName);
            if(!bandExists){
                string bandNotFoundMessage = $"You need to enter a valid band that already exists in the database.";
                throw new BandNotFoundException(bandNotFoundMessage);
            }
            // checks to see if album already exists here
            bool albumExists = await CheckAlbumExists(album.Name);
            if(albumExists){
                string albumNotFoundMessage = $"The album you entered already exist.";
                throw new AlbumExistsException(albumNotFoundMessage);
            }
            // passes tests, so we populate the db
            string sanitisedName = StringSanitiser.Transform(album.BandName);
            Band band = await _context.Bands.SingleAsync(b => b.Name == sanitisedName);
            Album saveAlbum = new Album()
            {
                Name = StringSanitiser.Transform(album.Name),
                Runtime = album.Runtime,
                Genre = album.Genre,
                AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                LastUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Band = band,
                
            };
            await _context.Albums.AddAsync(saveAlbum);
            await _context.SaveChangesAsync();
            // now we return a dto for the output
            return new AlbumResponseDto(){
                Id = saveAlbum.Id,
                Name = saveAlbum.Name,
                BandId = saveAlbum.Band.Id,
                BandName = saveAlbum.Band.Name,
                Runtime = saveAlbum.Runtime,
                Genre = saveAlbum.Genre,
            };
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            Album album = await _context.Albums
                            .FirstOrDefaultAsync(a => a.Id == id) 
                            ?? throw new AlbumNotFoundException("ERROR: Album Name look up returned null");
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAlbum(int id, AlbumInputDto details){
            // first the easy properties which dont require band
            Album album = await _context.Albums.Include(a => a.Band).FirstAsync(a => a.Id == id);
            album.Name = details.Name ?? album.Name;
            album.Runtime = details.Runtime ?? album.Runtime;
            album.Genre = details.Genre ?? album.Genre;
            album.LastUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            // now we check if we want to change the band name
            if(details.BandName is not null){
                bool bandExists = await CheckBandExists(details.BandName);
                if(!bandExists){
                    throw new BandNotFoundException($"Band cannot be found, no band with the name {details.BandName}");
                }
                if(StringSanitiser.Compare(album.Band.Name, details.BandName)){
                    // we want to keep the band name, just want to change the other properties
                    _context.Albums.Update(album);
                    await _context.SaveChangesAsync();
                    return;
                }
                else{
                    // we want to change the band the band relates to
                    string sanitisedName = StringSanitiser.Transform(details.BandName);
                    Band band = await _context.Bands
                                //.FirstOrDefaultAsync(b => StringSanitiser.Compare(b.Name, details.BandName))
                                //.FirstOrDefaultAsync(b => StringSanitiser.Transform(b.Name) ==  StringSanitiser.Transform(details.BandName))
                                .FirstOrDefaultAsync(b => b.Name.TrimEnd().TrimStart().ToLower() == sanitisedName)
                                ?? throw new BandNotFoundException("Cannot find the band!");
                    album.Band = band;
                    _context.Albums.Update(album);
                    _context.SaveChanges();
                    return;
                }
            }
            else{
                throw new BandNotFoundException("Band cannot be found, bandname field is empty");
            }
        }
    }
}
