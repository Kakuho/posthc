using System.ComponentModel.DataAnnotations;

namespace Phc.Data.Dto
{
    public class AlbumDto
    {
        public string Name { get; set; } = null!;
        public string? BandName { get; set; }
        public long? Runtime{ get; set; }
    }
}
