using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Model
{
#if !SILVERLIGHT && !PORTABLE
    [Serializable]
#endif
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

#if !SILVERLIGHT && !PORTABLE
        protected NonPersistedObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        public IPersistable NonPersistedObject { get; set; }
    }
}