using System;

namespace Phc.Exceptions{

    public class AlbumExistsException : Exception{
        public AlbumExistsException()
        {
        }

        public AlbumExistsException(string message): 
            base(message)
        {
        }

        public AlbumExistsException(string message, Exception inner):
            base(message, inner)
        {
        }
    }

}