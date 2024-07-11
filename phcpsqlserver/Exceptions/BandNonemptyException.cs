namespace Phc.Exceptions{
    public class BandNonEmptyException: Exception{
        public BandNonEmptyException()
        {

        }

        public BandNonEmptyException(string message): base(message)
        {

        }

        public BandNonEmptyException(string message, Exception inner):
            base(message, inner)
        {

        }
    }
}
    