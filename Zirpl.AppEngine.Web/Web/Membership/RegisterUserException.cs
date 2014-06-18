using System;
using System.Runtime.Serialization;
using System.Web.Security;

namespace Zirpl.AppEngine.Web.Membership
{
    public class RegisterUserException : Exception
    {
        public MembershipCreateStatus Error { get; set; }

        public RegisterUserException()
            : base()
        {
        }

        public RegisterUserException(string message)
            : base(message)
        {
        }

        public RegisterUserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RegisterUserException(MembershipCreateStatus error)
            : base()
        {
            this.Error = error;
        }

        public RegisterUserException(string message, MembershipCreateStatus error)
            : base(message)
        {
            this.Error = error;
        }

        public RegisterUserException(string message, MembershipCreateStatus error, Exception innerException)
            : base(message, innerException)
        {
            this.Error = error;
        }

        protected RegisterUserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
