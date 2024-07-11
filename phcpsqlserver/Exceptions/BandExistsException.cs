
namespace Phc.Exceptions{
    public class BandExistsException: Exception{
        public BandExistsException()
        {

        }

        public BandExistsException(string message): base(message)
        {

        }

        public BandExistsException(string message, Exception inner):
            base(message, inner)
        {

        }
    }
}
    