namespace Phc.Exceptions{
    public class PlaylistExistsException : Exception{
        public PlaylistExistsException()
        {
        }

        public PlaylistExistsException(string message): 
            base(message)
        {
        }

        public PlaylistExistsException(string message, Exception inner):
            base(message, inner)
        {
        }
    }

}