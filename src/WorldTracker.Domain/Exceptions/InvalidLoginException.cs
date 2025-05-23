using System.Net;

namespace WorldTracker.Domain.Exceptions
{
    public class InvalidLoginException : HttpRequestException
    {
        private const string MESSAGE = "Invalid username or password.";

        public InvalidLoginException()
            : this(MESSAGE)
        {
        }

        private InvalidLoginException(string message)
            : base(message, null, HttpStatusCode.Unauthorized)
        {
        }
    }
}
