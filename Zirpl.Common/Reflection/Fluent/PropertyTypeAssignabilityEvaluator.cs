using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class PropertyTypeAssignabilityEvaluator :AssignabilityEvaluatorBase
    {
        public override bool IsMatch(MemberInfo memberInfo)
        {
            return IsTypeMatch(((PropertyInfo)memberInfo).PropertyType);
        }
    }
}
