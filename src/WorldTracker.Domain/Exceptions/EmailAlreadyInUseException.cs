using System.Net;

namespace WorldTracker.Domain.Exceptions
{
    public class EmailAlreadyInUseException : HttpRequestException
    {
        private const string MESSAGE = "The email address is already in use.";

        public EmailAlreadyInUseException()
            : this(MESSAGE)
        {
        }

        private EmailAlreadyInUseException(string message)
            : base(message, null, HttpStatusCode.Conflict)
        {
        }
    }
}
