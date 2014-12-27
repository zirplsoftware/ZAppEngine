using System;
using FluentValidation;

namespace Zirpl.AppEngine.Validation.EntityFramework
{
    public abstract class DbEntityValidatorBase<T> : AbstractValidator<T>
    {
        public void InsertRuleSet(Action action)
        {
            this.RuleSet("Insert", action);
        }

        public void UpdateRuleSet(Action action)
        {
            this.RuleSet("Update", action);
        }
    }
}
