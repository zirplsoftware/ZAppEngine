using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MethodReturnTypeEvaluator :TypeEvaluator
    {
        internal bool Void { get; set; }
        internal bool NotVoid { get; set; }

        public override bool IsMatch(MemberInfo memberInfo)
        {
            // TODO: do the null checks
            return base.IsMatch(((MethodInfo)memberInfo).ReturnType);
        }
    }
}
