using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace Zirpl.AppEngine.Validation
{
    public class ValidationException : Exception
    {
        private ICollection<ValidationError> validationErrors;
        public ICollection<ValidationError> ValidationErrors
        {
            get { return this.validationErrors ?? new Collection<ValidationError>(); }
            set { this.validationErrors = value; }
        }

        public ValidationException()
            :base()
        {
        }
        
        public ValidationException(string message)
            :base(message)
        {
        }
        
        public ValidationException(string message, Exception innerException)
            :base(message, innerException)
        {
        }

        public ValidationException(ICollection<ValidationError> validationErrors)
            : base()
        {
            this.ValidationErrors = validationErrors;
        }

        public ValidationException(string message, ICollection<ValidationError> validationErrors)
            : base(message)
        {
            this.ValidationErrors = validationErrors;
        }

        public ValidationException(string message, ICollection<ValidationError> validationErrors, Exception innerException)
            : base(message, innerException)
        {
            this.ValidationErrors = validationErrors;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                message.Append(base.Message);
                if (message.Length > 0)
                {
                    message.Append(": ");
                }
                if (this.ValidationErrors != null)
                {
                    int i = 0;
                    int count = this.ValidationErrors.Count;
                    foreach (var validationError in this.ValidationErrors)
                    {
                        i++;
                        message.AppendFormat("Error {0} of {1}: {2}", i, count, validationError.ToString());
                        message.AppendLine();
                    }
                }
                return message.ToString();
            }
        }
    }
}
