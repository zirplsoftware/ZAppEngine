using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Model
{
    public class PersistedObjectException : Exception
    {
        public PersistedObjectException()
        {
        }

        public PersistedObjectException(String message)
            : base(message)
        {
        }

        public PersistedObjectException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public PersistedObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IPersistable PersistedObject { get; set; }
    }
}