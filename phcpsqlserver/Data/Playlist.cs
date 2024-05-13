using System.ComponentModel.DataAnnotations;

namespace Phc.Data
{
    public class Playlist
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? Runtime { get; set; } // in minutes
        [DataType(DataType.Date)]
        public DateTime AddedOn{get; set;}
        // navigations for albums
        public List<Album> Albums { get; } = [];
        public List<JoinPlaylistAlbum> PlaylistAlbums { get; } = [];
    }
}
