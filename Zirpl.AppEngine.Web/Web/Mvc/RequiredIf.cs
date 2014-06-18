using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Zirpl.AppEngine.Web.Mvc
{
    // Taken from the blog post at:
    // http://www.devtrends.co.uk/blog/the-complete-guide-to-validation-in-asp.net-mvc-3-part-2

    public class ModelClientValidationRequiredIfRule : ModelClientValidationRule
    {
        public ModelClientValidationRequiredIfRule(string errorMessage,
                                                   string otherProperty,
                                                   RequiredIfComparison comparison,
                                                   object value)
        {
            ErrorMessage = errorMessage;
            ValidationType = "requiredif";
            ValidationParameters.Add("other", otherProperty);
            ValidationParameters.Add("comp", comparison.ToString().ToLower());
            ValidationParameters.Add("value", value.ToString().ToLower());
        }
    }

    public enum RequiredIfComparison
    {
        IsEqualTo,
        IsNotEqualTo
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessageFormatString = "The {0} field is required.";

        public string ComparativePropertyName { get; private set; }
        public RequiredIfComparison Comparison { get; private set; }
        public object ComparisonValue { get; private set; }

        public RequiredIfAttribute(string comparativeProperty, RequiredIfComparison comparison, object comparisonValue)
        {
            if (string.IsNullOrEmpty(comparativeProperty))
            {
                throw new ArgumentNullException("comparativeProperty");
            }

            ComparativePropertyName = comparativeProperty;
            Comparison = comparison;
            ComparisonValue = comparisonValue;

            ErrorMessage = DefaultErrorMessageFormatString;
        }

        private bool IsRequired(object actualComparativePropertyValue)
        {
            bool isRequired = false;
            switch (this.Comparison)
            {
                case RequiredIfComparison.IsNotEqualTo:
                    isRequired = (actualComparativePropertyValue == null
                               && this.ComparisonValue != null)
                              || (actualComparativePropertyValue != null
                                  && this.ComparisonValue == null)
                              || (actualComparativePropertyValue != null
                                  && !actualComparativePropertyValue.Equals(this.ComparisonValue));
                    break;
                default:
                    isRequired = (actualComparativePropertyValue == null 
                                && this.ComparisonValue == null)
                                || (actualComparativePropertyValue != null
                                    && actualComparativePropertyValue.Equals(this.ComparisonValue));
                    break;
            }
            return isRequired;
        }

        protected override ValidationResult IsValid(object value,
                                                    ValidationContext validationContext)
        {
            if (value == null)
            {
                var comparativePropertyInfo = validationContext.ObjectInstance.GetType().GetProperty(this.ComparativePropertyName);
                var actualComparativePropertyValue = comparativePropertyInfo.GetValue(validationContext.ObjectInstance, null);

                if (IsRequired(actualComparativePropertyValue))
                {
                    return new ValidationResult(
                        string.Format(ErrorMessageString, validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
                                                            ModelMetadata metadata,
                                                            ControllerContext context)
        {
            return new[]
            {
                new ModelClientValidationRequiredIfRule(string.Format(ErrorMessageString, 
                    metadata.GetDisplayName()), ComparativePropertyName, Comparison, ComparisonValue)
            };
        }
    }
}
