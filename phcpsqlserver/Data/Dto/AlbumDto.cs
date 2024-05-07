using System.ComponentModel.DataAnnotations;

namespace Phc.Data.Dto
{
    public class AlbumDto
    {
        public string Name { get; set; }
        public string? BandName { get; set; }
        public long? Runtime{ get; set; }
    }
}
