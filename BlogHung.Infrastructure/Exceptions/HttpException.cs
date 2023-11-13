using System;

namespace BlogHung.Infrastructure.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(string message)
           : base(message)
        {
        }

        public HttpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
