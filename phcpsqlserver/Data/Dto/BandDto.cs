using System.ComponentModel.DataAnnotations;

namespace Phc.Data.Dto
{ 
    public class BandDto
    {
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Formed { get; set; }
    }

    public class BandResponseDto
    {
        public required long Id {get; set;}
        public string? Name {get; set;}
        public string? Genre {get; set;}
        public DateTime? Formed{ get; set;}
    }
    
    public class BandInputDto
    {
        public required string Name {get; set;}
        public string? Genre {get; set;}
        public DateTime? Formed{ get; set;}
    }
}
