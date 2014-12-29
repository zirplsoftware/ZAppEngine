using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class EventHandlerTypeEvaluator :TypeEvaluator
    {
        public override bool IsMatch(MemberInfo memberInfo)
        {
            return base.IsMatch(((EventInfo)memberInfo).EventHandlerType);
        }
    }
}
