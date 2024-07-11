namespace Phc.Data.Dto{
    public class PlaylistInputDto{
        public required string Name {get; set;}
        public long? Runtime {get; set;}
    }
    
    public class PlaylistResponseDto{
        public required long Id {get; set;}
        public required string Name {get; set;}
        public long? Runtime {get; set;}
    }
}