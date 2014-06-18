using System;

namespace Zirpl.AppEngine.Validation
{
    public class ValidationError
    {
        public ValidationError()
        {
            
        }

        public ValidationError(String propertyName, String errorMessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }

        public String ErrorMessage { get; set; }
        public String PropertyName { get; set; }

        public override string ToString()
        {
            //return base.ToString();

            return String.Format("{0}: {1}", this.PropertyName, this.ErrorMessage);
        }
    }
}
