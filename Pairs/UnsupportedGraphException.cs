using System;
using System.Runtime.Serialization;

namespace Pairs
{
    [Serializable]
    internal class UnsupportedGraphException : Exception
    {
        public UnsupportedGraphException()
        {
        }

        public UnsupportedGraphException(string message) : base(message)
        {
        }

        public UnsupportedGraphException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedGraphException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}