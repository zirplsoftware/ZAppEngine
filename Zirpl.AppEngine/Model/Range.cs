using System;
using System.Text;

namespace Zirpl.AppEngine.Model
{
    /// <summary>
    /// Represents a range of values, with Minimum and Maximum values
    /// </summary>
    /// <typeparam name="T">An <see cref="IComparable<T>"/> object</typeparam>
    public class Range<T> : IRange<T> where T : IComparable<T>
	{
        public T Minimum { get; set; }
        public T Maximum { get; set; }

        /// <summary>
        /// Creates an uninitialized Range object
        /// </summary>
        public Range()
        {
        }

        /// <summary>
        /// Creates a range object and initializes the minimum and maximum values
        /// </summary>
        /// <param name="minimum">Minimum value</param>
        /// <param name="maximum">Maximum value</param>
        public Range(T minimum, T maximum) 
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
        }


        #region IRange Members

        object IRange.Minimum
        {
            get
            {
                return this.Minimum;
            }
            set
            {
                this.Minimum = (T)value;
            }
        }

        object IRange.Maximum
        {
            get
            {
                return this.Maximum;
            }
            set
            {
                this.Maximum = (T)value;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this.Maximum.CompareTo(this.Minimum) < 0)
                {
                    return false;
                }
                return true;
            }
        }

        #endregion

        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Range<T>))
            {
                throw new ArgumentException("Object to compare to must be a Range<" + typeof(T).Name + ">");
            }
            return CompareTo((IRange<T>)obj);
        }

        #endregion

        #region IComparable<IRange<T>> Members

        public int CompareTo(IRange<T> other)
        {
            if (!this.IsValid || !other.IsValid)
            {
                throw new InvalidRangeException("Cannot compare an invalid range");
            }

            if (this.Minimum.CompareTo(other.Minimum) > 0)
            {
                return 1;
            }
            else if (this.Minimum.CompareTo(other.Minimum) < 0)
            {
                return -1;
            }
            else if (this.Maximum.CompareTo(other.Maximum) > 0)
            {
                return 1;
            }
            else if (this.Maximum.CompareTo(other.Maximum) < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(this.Minimum.ToString());
            sb.Append(", ");
            sb.Append(this.Maximum.ToString());
            sb.Append(")");
            return sb.ToString();
        }

        /// <summary>
        /// Checks if this range object is equal to the object passed in
        /// </summary>
        /// <param name="obj">Object to compare to this 
        /// object for equality</param>
        /// <returns>Returns true is the objects represent the same range, 
        /// otherwise returns false</returns>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || !(this.GetType().Equals(obj.GetType()))) return false;

            Range<T> other = (Range<T>)obj;
            return this.Maximum.Equals(other.Maximum) && this.Minimum.Equals(other.Minimum);
        }

        /// <summary>
        /// Gets the HashCode for the Range
        /// </summary>
        /// <returns>returns the HashCode value for the range</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
    }
}
/* $Log: Range.cs,v $
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