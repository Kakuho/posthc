using System.ComponentModel.DataAnnotations;

namespace Phc.Data
{
    public class Album
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long? Runtime { get; set; } // in minutes
        [DataType(DataType.Date)]
        public DateTime AddedOn{get; set;}
        public string? Genre {get; set;}  // should be an enum
        // navigations for bands
        public long bandId { get; set; }
        public Band Band { get; set; } = null!;
        // navigations for playlist
        public List<Playlist> Playlists {get;} = [];
        public List<JoinPlaylistAlbum> PlaylistAlbums {get;} = [];
    } 
}
