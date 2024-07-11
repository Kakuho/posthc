using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Phc.Exceptions;
using Phc.Utilities;

namespace Phc.Service
{
    public class PlaylistService : IPlaylistService
    {
        private PhcContext _context;

        public PlaylistService(PhcContext context)
        {
            _context = context;
        }

        public async Task<List<PlaylistResponseDto>> GetAllPlaylistsAsync()
        {
            List<Playlist> playlists = await _context.Playlists.ToListAsync();
            List<PlaylistResponseDto> payload = new List<PlaylistResponseDto>(playlists.Count);
            playlists.ForEach(p => payload.Add( new PlaylistResponseDto(){
                Id = p.Id,
                Name = p.Name,
                Runtime = p.Runtime
                })
            );
            return payload;
        }

        public async Task<PlaylistResponseDto> GetPlaylistByIdAsync(long id)
        {
            Playlist pl = await _context.Playlists
                            .SingleOrDefaultAsync(pl => pl.Id == id)
                            ?? throw new PlaylistNotFoundException($"Cannot find playlist with id {id}");
            return new PlaylistResponseDto(){
                Id = pl.Id,
                Name = pl.Name,
                Runtime = pl.Runtime    
            };
        }

        

        public async Task<PlaylistResponseDto> AddPlaylist(PlaylistInputDto input)
        {
            // error checking to ensure no playlist with the same name can be inserted
            string sanitisedPlaylistName = StringSanitiser.Transform(input.Name);
            int count = await _context.Playlists.CountAsync(p => p.Name.ToLower().TrimStart().TrimEnd() == sanitisedPlaylistName);
            if(count > 0){
                throw new PlaylistExistsException($"cannot add playlist, there is already a playlist with name {input.Name} in the db");
            }
            // constructing the band to save
            Playlist savePlaylist = new Playlist()
            {
                Id = 0, // will be ignored
                Name = input.Name,
                Runtime = input.Runtime,
                AddedOn = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                //LastUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
            await _context.Playlists.AddAsync(savePlaylist);
            await _context.SaveChangesAsync();
            // now we return the representational state to the user
            return new PlaylistResponseDto(){
                Id = savePlaylist.Id,
                Name = savePlaylist.Name,
                Runtime = savePlaylist.Runtime
            };
        }

        public async Task DeletePlaylist(long id)
        {
            Playlist pl = await _context.Playlists
                            .FirstAsync(pl => pl.Id == id)
                            ?? throw new PlaylistNotFoundException($"Cannot find playlist entry with id {id}");
            _context.Playlists.Remove(pl);
            await _context.SaveChangesAsync();
        }

        public async Task<PlaylistResponseDto> UpdatePlaylist(long id, PlaylistInputDto input){
            // first ensure id is valid
            Playlist playlist = await _context.Playlists
                                .FirstAsync(p => p.Id == id)
                                ?? throw new PlaylistNotFoundException($"Playlist with id {id} cannot be found");
            // perform the changes
            playlist.Name = input.Name;
            playlist.Runtime = input.Runtime;       // this should be computed...
            _context.Playlists.Update(playlist);
            await _context.SaveChangesAsync();
            return new PlaylistResponseDto(){
                Id = playlist.Id,
                Name = playlist.Name,
                Runtime = playlist.Runtime
            };
        }
    }
}
