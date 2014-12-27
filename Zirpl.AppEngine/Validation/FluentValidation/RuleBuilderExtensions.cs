using System;
using FluentValidation;

namespace Zirpl.AppEngine.Validation
{
    public static class RuleBuilderExtensions
    {   
        public static IRuleBuilderOptions<TEntity, Decimal> MaxDecimalPlaces<TEntity>(
           this IRuleBuilder<TEntity, Decimal> ruleBuilder,
            int maxDecimalPlaces)
        {
            return ruleBuilder.Must((val) => val.DecimalPlacesCount() <= maxDecimalPlaces)
                    .WithMessage("Max decimal places is {0}", maxDecimalPlaces);
        }

        public static IRuleBuilderOptions<TEntity, Double> MaxDecimalPlaces<TEntity>(
           this IRuleBuilder<TEntity, Double> ruleBuilder,
            int maxDecimalPlaces,
            String messageOverride = null)
        {
            return ruleBuilder.Must((val) => val.DecimalPlacesCount() <= maxDecimalPlaces)
                    .WithMessage("Max decimal places is {0}", maxDecimalPlaces);
        }

        public static IRuleBuilderOptions<TEntity, Single> MaxDecimalPlaces<TEntity>(
           this IRuleBuilder<TEntity, Single> ruleBuilder,
            int maxDecimalPlaces,
            String messageOverride = null)
        {
            return ruleBuilder.Must((val) => val.DecimalPlacesCount() <= maxDecimalPlaces)
                    .WithMessage("Max decimal places is {0}", maxDecimalPlaces);
        }

        public static IRuleBuilder<TEntity, TProperty?> Null<TEntity, TProperty>(
           this IRuleBuilder<TEntity, TProperty?> ruleBuilder)
            where TProperty : struct
        {
            return ruleBuilder.Must((val) => !val.HasValue).WithMessage("Must be null");
        }

        public static IRuleBuilder<TEntity, TProperty> Null<TEntity, TProperty>(
           this IRuleBuilder<TEntity, TProperty> ruleBuilder)
            where TProperty : class
        {
            return ruleBuilder.Must((val) => val == null).WithMessage("Must be null");
        }
    }
}
