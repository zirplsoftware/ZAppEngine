using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Mapping
{    
    /// <summary>
    /// Denotes an exception due to data being mapped incorrectly
    /// </summary>
#if !SILVERLIGHT && !PORTABLE
    [Serializable]
#endif
    public class InvalidMappingDataException : System.Exception
    {
        public InvalidMappingDataException()
        { }

        public InvalidMappingDataException(string msg)
            : base(msg)
        {

        }

        public InvalidMappingDataException(string msg, Exception inner)
            : base(msg, inner)
        {

        }

#if !SILVERLIGHT && !PORTABLE
        protected InvalidMappingDataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        public string MappingField { get; set; }
    }
}
