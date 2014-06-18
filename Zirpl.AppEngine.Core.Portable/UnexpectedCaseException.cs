using System;

namespace Zirpl.AppEngine.Core
{
    /// <summary>
    /// Denotes an exception due to an unexpected data case
    /// </summary>
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

        public object CaseData { get; set; }
    }
}
