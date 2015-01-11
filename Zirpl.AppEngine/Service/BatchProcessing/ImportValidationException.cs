using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Service.BatchProcessing
{
    /// <summary>
    /// An exception that occurs during a data import process
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class ImportValidationException : Exception
    {
        public ImportValidationException()
        {
            this.Errors = new List<int>();
        }

        public ImportValidationException(String message)
            : base(message)
        {
            this.Errors = new List<int>();
        }

        public ImportValidationException(String message, Exception innerException)
            : base(message, innerException)
        {
            this.Errors = new List<int>();
        }

#if !PORTABLE
        public ImportValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Errors = new List<int>();
        }
#endif

        /// <summary>
        /// This list of errors that caused the exception
        /// </summary>
        public IList<int> Errors { get; private set; }
    }
}
