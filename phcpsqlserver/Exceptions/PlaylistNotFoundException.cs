namespace Phc.Exceptions{

    public class PlaylistNotFoundException : Exception{
        public PlaylistNotFoundException()
        {
        }

        public PlaylistNotFoundException(string message): 
            base(message)
        {
        }

        public PlaylistNotFoundException(string message, Exception inner):
            base(message, inner)
        {
        }
        
    }
    
}