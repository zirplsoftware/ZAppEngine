using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
