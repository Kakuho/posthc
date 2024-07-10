using System.ComponentModel.DataAnnotations;

namespace Phc.Data
{
    public class Album
    {
        public long Id { get; set; }
        public string Name { get; set; }  = null!;
        public long? Runtime { get; set; }  // in minutes
        public string? Genre { get; set; }    // should be an enum

        [DataType(DataType.Date)]
        public DateTime AddedOn{ get; set;}
        [DataType(DataType.Date)]
        public DateTime? LastUpdated{ get; set; }

        // navigations for bands
        public long bandId { get; set; }
        public required Band Band { get; set; }

        // navigations for playlist
        public List<Playlist> Playlists { get; } = [];
        public List<JoinPlaylistAlbum> PlaylistAlbums { get; } = [];
    } 
}
