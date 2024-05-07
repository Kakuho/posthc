using System.ComponentModel.DataAnnotations;

namespace Phc.Data
{
    public class Album
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Runtime { get; set; } // in minutes
        [DataType(DataType.Date)]
        public DateTime AddedOn{get; set;}
        //public string Genre {get; set;}  // should be an enum
        public long? bandId { get; set; }
        public Band? Band { get; set; }
    }
}
