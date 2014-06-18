using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Model
{
    public class NonPersistedObjectException : Exception
    {
        public NonPersistedObjectException()
        {
        }

        public NonPersistedObjectException(String message)
            : base(message)
        {
        }

        public NonPersistedObjectException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NonPersistedObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IPersistable NonPersistedObject { get; set; }
    }
}