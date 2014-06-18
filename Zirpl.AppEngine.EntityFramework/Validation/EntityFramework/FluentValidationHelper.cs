using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FluentValidation;
using FluentValidation.Internal;

namespace Zirpl.AppEngine.Validation.EntityFramework
{
    public class FluentValidationHelper : IValidationHelper
    {
        public IValidatorFactory ValidationFactory { get; set; }

        public bool IsValidatable(Object obj)
        {
            return this.GetValidator(obj) != null;
        }

        public ICollection<ValidationError> Validate(params object[] objs)
        {
            List<ValidationError> list = new List<ValidationError>();
            foreach (var objToValidate in objs)
            {
                var validator = this.GetValidator(objToValidate);

                if (objToValidate is DbEntityEntry)
                {
                    var nonRuleSetContext = new FluentValidation.ValidationContext(
                        ((DbEntityEntry)objToValidate).Entity,
                        new PropertyChain(),
                        new RulesetValidatorSelector());

                    String ruleSet = ((DbEntityEntry)objToValidate).State == EntityState.Added
                                         ? "Insert"
                                         : "Update";
                    var ruleSetContext = new FluentValidation.ValidationContext(
                        ((DbEntityEntry)objToValidate).Entity,
                        new PropertyChain(),
                        new RulesetValidatorSelector(ruleSet));

                    var validationResult = validator.Validate(nonRuleSetContext);
                    foreach (var error in validationResult.Errors)
                    {
                        list.Add(new EntityValidationError(error.PropertyName, error.ErrorMessage, objToValidate));
                    }
                    validationResult = validator.Validate(ruleSetContext);
                    foreach (var error in validationResult.Errors)
                    {
                        list.Add(new EntityValidationError(error.PropertyName, error.ErrorMessage, objToValidate));
                    }
                }
                else
                {
                    var validationResult = validator.Validate(objToValidate);
                    foreach (var error in validationResult.Errors)
                    {
                        list.Add(new EntityValidationError(error.PropertyName, error.ErrorMessage, objToValidate));
                    }
                }
            }
            return list;
        }

        private FluentValidation.IValidator GetValidator(Object obj)
        {
            Type typeToValidate = null;
            if (obj is DbEntityEntry)
            {
                obj = ((DbEntityEntry)obj).Entity;
            }
            typeToValidate = obj.GetType();
            if (typeToValidate.FullName.StartsWith("System.Data.Entity.DynamicProxies"))
            {
                typeToValidate = typeToValidate.BaseType;
            }
            var validator = this.ValidationFactory.GetValidator(typeToValidate);
            return validator;
        }

        public void AssertValid(params object[] objs)
        {
            ICollection<ValidationError> list = Validate(objs);
            if (list.Count > 0)
            {
                throw new Zirpl.AppEngine.Validation.ValidationException(list);
            }
        }
    }
}