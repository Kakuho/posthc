using System.ComponentModel.DataAnnotations;

namespace Phc.Data
{
    public class Band
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Formed { get; set; }
        [DataType(DataType.Date)]
        public DateTime AddedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastModified { get; set; }

        // Navigations
        public ICollection<Album> Albums { get; set; }
    }
}
