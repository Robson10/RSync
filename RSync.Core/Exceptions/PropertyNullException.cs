using System;

namespace RSync.Core.Exceptions
{
    public class PropertyNullException : Exception
    {
        public PropertyNullException(string message) : base(message)
        {
        }

        public PropertyNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PropertyNullException()
        {
        }
    }
}
