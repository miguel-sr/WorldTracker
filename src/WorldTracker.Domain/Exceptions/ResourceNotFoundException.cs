using System.Net;

namespace WorldTracker.Domain.Exceptions
{
    public class ResourceNotFoundException : HttpRequestException
    {
        private const string DEFAULT_MESSAGE = "The requested resource was not found.";
        private const string MESSAGE_TEMPLATE = "The requested resource [{0}] with {1} [{2}] was not found.";

        /// <summary>
        /// Generic 404 exception.
        /// </summary>
        public ResourceNotFoundException()
            : base(DEFAULT_MESSAGE, null, HttpStatusCode.NotFound)
        {
        }

        /// <summary>
        /// 404 for a resource with default key name "ID".
        /// </summary>
        public ResourceNotFoundException(string resourceName, object keyValue)
            : this(resourceName, "ID", keyValue)
        {
        }

        /// <summary>
        /// 404 for a resource with custom key name and value.
        /// </summary>
        public ResourceNotFoundException(string resourceName, string keyName, object keyValue)
            : base(string.Format(MESSAGE_TEMPLATE, resourceName, keyName, keyValue?.ToString()), null, HttpStatusCode.NotFound)
        {
        }

        public ResourceNotFoundException(string message)
            : base(message, null, HttpStatusCode.NotFound)
        {
        }
    }
}
