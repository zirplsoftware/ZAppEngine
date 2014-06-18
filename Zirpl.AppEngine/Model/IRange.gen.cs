using System;

namespace Zirpl.AppEngine.Model
{
    /// <summary>
    /// Interface that describes a Range of values
    /// </summary>
    /// <typeparam name="T">Type of values described by the range</typeparam>
    public interface IRange<T> :IRange, IComparable<IRange<T>> where T:IComparable<T>
    {
        /// <summary>
        /// Gets or sets the Minimum value
        /// </summary>
        new T Minimum { get; set;}

        /// <summary>
        /// Gets or sets the Maximum value
        /// </summary>
        new T Maximum { get; set;}
    }
}
/* $Log: IRange.cs,v $
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