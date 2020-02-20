using System;

namespace RSync.Core.Exceptions
{
    public class FileEmptyException : Exception
    {
        public FileEmptyException(string message) : base(message)
        {
        }

        public FileEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FileEmptyException()
        {
        }
    }
}
