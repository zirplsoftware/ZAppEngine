using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Service.Membership
{
#if !SILVERLIGHT && !PORTABLE
    [Serializable]
#endif
    public class ChangeUserNameException : Exception
    {
        public ChangeUserNameError Error { get; set; }

        public ChangeUserNameException()
            : base()
        {
        }

        public ChangeUserNameException(string message)
            : base(message)
        {
        }

        public ChangeUserNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ChangeUserNameException(ChangeUserNameError error)
            : base()
        {
            this.Error = error;
        }

        public ChangeUserNameException(string message, ChangeUserNameError error)
            : base(message)
        {
            this.Error = error;
        }

        public ChangeUserNameException(string message, ChangeUserNameError error, Exception innerException)
            : base(message, innerException)
        {
            this.Error = error;
        }

#if !SILVERLIGHT && !PORTABLE
        protected ChangeUserNameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
