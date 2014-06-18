using System;

namespace Zirpl.AppEngine.Mapping
{    
    /// <summary>
    /// Denotes an exception due to data being mapped incorrectly
    /// </summary>
    [Serializable]
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

        public string MappingField { get; set; }
    }
}
