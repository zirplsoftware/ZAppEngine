using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Model
{
#if !PORTABLE
    [Serializable]
#endif
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

#if !PORTABLE
        protected PersistedObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        public IPersistable PersistedObject { get; set; }
    }
}