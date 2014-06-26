using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine
{
    /// <summary>
    /// Denotes an exception due to an unexpected data case
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class UnexpectedCaseException : System.Exception
    {
        public UnexpectedCaseException()
        {
            
        }

        public UnexpectedCaseException(string msg)
            : base(msg)
        {

        }

        public UnexpectedCaseException(string msg, Exception inner)
            : base(msg, inner)
        {

        }

        public UnexpectedCaseException(Object caseData)
        {
            this.CaseData = caseData;
        }

        public UnexpectedCaseException(string msg, Object caseData)
            : base(msg)
        {
            this.CaseData = caseData;
        }

        public UnexpectedCaseException(string msg, Object caseData, Exception inner)
            : base(msg, inner)
        {
            this.CaseData = caseData;
        }

#if !SILVERLIGHT
        protected UnexpectedCaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

        public object CaseData { get; set; }
    }
}
