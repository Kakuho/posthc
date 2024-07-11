using System.ComponentModel.DataAnnotations;

namespace Phc.Data
{
    public class Band
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public string? Genre {get; set;}

        [DataType(DataType.Date)]
        public DateTime? Formed  { get; set; } 
        // these two are for business logic
        [DataType(DataType.Date)]
        public DateTime AddedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastUpdated { get; set; }

        // Navigations
        public ICollection<Album> Albums { get; set; } = null!;
    }
}
