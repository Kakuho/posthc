namespace Phc.Exceptions{

    public class AlbumNotFoundException : Exception{
        public AlbumNotFoundException()
        {
        }

        public AlbumNotFoundException(string message): 
            base(message)
        {
        }

        public AlbumNotFoundException(string message, Exception inner):
            base(message, inner)
        {
        }
    }
    
}