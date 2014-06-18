using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Model
{
    /// <summary>
    /// An exception thrown when an <see cref="IRange"/> object is not valid
    /// </summary>
    public class InvalidRangeException :Exception
    {
        /// <summary>
        /// Instantiates a new InvalidRangeException
        /// </summary>
        public InvalidRangeException()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new InvalidRangeException
        /// </summary>
        public InvalidRangeException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Instantiates a new InvalidRangeException
        /// </summary>
        public InvalidRangeException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Instantiates a new InvalidRangeException
        /// </summary>
        public InvalidRangeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
/* $Log: InvalidRangeException.cs,v $
/* Revision 1.2  2006/04/16 17:00:29  nathan
/* added NumericalFieldMetadataTest
/* finished DateFieldMetadataTest
/* removed all overloaded constructors from fieldMetadata classes
/* changed Min and Max lengths to be nullable in StringFieldMetadata
/* added documentation to PowerClasses classes
/* added log entry to all classes without it
/* fixed bugs in NumericalFieldMetadata and DateTimeFieldMetadata
/*
/* Revision 1.1  2006/04/14 22:30:12  nathan
/* Added PowerClasses project
/* Adding unit tests
/*
 */