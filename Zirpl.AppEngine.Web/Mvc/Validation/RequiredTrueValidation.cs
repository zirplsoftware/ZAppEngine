using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Zirpl.AppEngine.Web.Mvc.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class RequiredTrueAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value != null
                && (value as bool?).HasValue
                && (bool)value ? ValidationResult.Success : new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]
                   {
                       new ModelClientValidationRule()
                       {
                           ErrorMessage =
                               string.Format(ErrorMessageString,
                                   metadata.GetDisplayName()),
                           ValidationType = "requiredtrue"
                       }
                   };
        }
    }
}
