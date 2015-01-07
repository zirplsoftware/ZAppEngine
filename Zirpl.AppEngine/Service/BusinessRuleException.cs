using System;
using System.Runtime.Serialization;

namespace Zirpl.AppEngine.Service
{
#if !SILVERLIGHT && !PORTABLE
    [Serializable]
#endif
    public class BusinessRuleException<TEnum> : Exception where TEnum : struct, IComparable, IConvertible, IFormattable
    {
        public TEnum BusinessRule { get; set; }

        public BusinessRuleException()
            :base()
        {
            this.AssertTEnumIsEnum();
        }
        
        public BusinessRuleException(string message)
            :base(message)
        {
            this.AssertTEnumIsEnum();
        }
        
        public BusinessRuleException(string message, Exception innerException)
            :base(message, innerException)
        {
            this.AssertTEnumIsEnum();
        }

        public BusinessRuleException(TEnum businessRule)
            : base()
        {
            this.AssertTEnumIsEnum();
            this.BusinessRule = businessRule;
        }

        public BusinessRuleException(string message, TEnum businessRule)
            : base(message)
        {
            this.AssertTEnumIsEnum();
            this.BusinessRule = businessRule;
        }

        public BusinessRuleException(string message, TEnum businessRule, Exception innerException)
            : base(message, innerException)
        {
            this.AssertTEnumIsEnum();
            this.BusinessRule = businessRule;
        }

#if !SILVERLIGHT && !PORTABLE
        protected BusinessRuleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.AssertTEnumIsEnum();
        }
#endif

        protected void AssertTEnumIsEnum()
        {
            if (!typeof (TEnum).IsEnum)
            {
                throw new NotSupportedException("The generic parameter TEnum can only be an Enum type");
            }
        }
    }
}
