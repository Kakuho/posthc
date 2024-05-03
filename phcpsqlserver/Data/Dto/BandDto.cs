using System.ComponentModel.DataAnnotations;

namespace Phc.Data.Dto
{
    public class BandDto
    {
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Formed { get; set; }
    }
}
