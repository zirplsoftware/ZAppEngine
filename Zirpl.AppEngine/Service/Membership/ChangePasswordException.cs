using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Service.Membership
{
#if !PORTABLE
    [Serializable]
#endif
    public class ChangePasswordException : Exception
    {

        public ChangePasswordException()
            : base()
        {
        }

        public ChangePasswordException(string message)
            : base(message)
        {
        }

        public ChangePasswordException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !PORTABLE
        protected ChangePasswordException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
