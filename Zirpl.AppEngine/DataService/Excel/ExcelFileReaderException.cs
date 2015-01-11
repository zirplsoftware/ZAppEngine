using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.DataService.Excel
{
    /// <summary>
    /// Denotes an exception that occurs within the ExcelFileReader
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class ExcelFileReaderException : System.Exception
    {
        public ExcelFileReaderException()
        {
        }

        public ExcelFileReaderException(string msg)
            : base(msg)
        {

        }

        public ExcelFileReaderException(string msg, Exception inner)
            : base(msg, inner)
        {

        }

#if !PORTABLE
        protected ExcelFileReaderException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
        }
#endif
    }
}
