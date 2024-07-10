using System.ComponentModel.DataAnnotations;
using NuGet.Protocol.Core.Types;
using Phc.Data;

namespace Phc.Data.Dto
{
    public class AlbumResponseDto
    {
        // general response dto for a httpget
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required long BandId { get; set; }
        public required string BandName { get; set; }
        public long? Runtime{ get; set; }
        public string? Genre {get; set;}
    }

    public class AlbumInputDto
    {
        public required string Name { get; set; }
        public required string BandName { get; set; }
        public long? Runtime{ get; set; }
        public string? Genre{ get; set; }
    }
}
