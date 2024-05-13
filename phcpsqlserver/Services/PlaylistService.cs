using Phc.Data;
using Phc.Data.Dto;
using Phc.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Phc.Service
{
    public class PlaylistService : IPlaylistService
    {
        private PhcContext _context;

        public PlaylistService(PhcContext context)
        {
            _context = context;
        }

        public async Task<Playlist> GetPlaylistByIdAsync(long id)
        {
            Playlist pl = await _context.Playlists
                            .SingleOrDefaultAsync(pl => pl.Id == id)
                            ?? throw new ArgumentNullException("null playlist");
            return pl;
        }

        public async Task<Playlist> GetPlaylistByNameAsync(string name)
        {
            Playlist output = await _context.Playlists
                                .FirstOrDefaultAsync(pl => pl.Name == name)

                                ?? throw new ArgumentNullException("null playlist");
            return output;
        }


        public async Task<List<Playlist>> GetAllPlaylists()
        {
            return await _context.Playlists.ToListAsync();
        }

        public Playlist AddPlaylist(Playlist pl)
        {
            Playlist savePlaylist = new Playlist()
            {
                Id = pl.Id,
                Name = pl.Name,
                AddedOn = DateTime.SpecifyKind(pl.AddedOn, DateTimeKind.Utc),
            };
            _context.Playlists.Add(savePlaylist);
            _context.SaveChanges();
            return savePlaylist;
        }

        public async Task<bool> DeletePlaylist(string name)
        {
            Playlist pl = await _context.Playlists
                            .FirstAsync(pl => pl.Name == name)
                            ?? throw new ArgumentNullException("null playlist");
            _context.Playlists.Remove(pl);
            _context.SaveChanges();
            return true;
        }
    }
}
