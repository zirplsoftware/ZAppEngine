using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.Enums;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MemberTypeFlagsBuilder
    {
        private readonly MemberTypeEvaluator _memberTypeEvaluator;
        internal MemberTypeFlagsBuilder(MemberTypeEvaluator memberTypeEvaluator)
        {
            _memberTypeEvaluator = memberTypeEvaluator;
        }
        internal MemberTypeFlags MemberTypeFlags
        {
            get
            {
                var memberTypes = default(MemberTypeFlags);
                if (_memberTypeEvaluator.Constructor
                    || _memberTypeEvaluator.Event
                    || _memberTypeEvaluator.Field
                    || _memberTypeEvaluator.Method
                    || _memberTypeEvaluator.NestedType
                    || _memberTypeEvaluator.Property)
                {
                    if (_memberTypeEvaluator.Constructor) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Constructor);
                    if (_memberTypeEvaluator.Event) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Event);
                    if (_memberTypeEvaluator.Field) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Field);
                    if (_memberTypeEvaluator.Method) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Method);
                    if (_memberTypeEvaluator.NestedType) memberTypes = memberTypes.AddFlag(MemberTypeFlags.NestedType);
                    if (_memberTypeEvaluator.Property) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Property);
                }
                else
                {
                    // default to ALL 
                    memberTypes = MemberTypeFlags.AllExceptCustomAndTypeInfo;
                }
                return memberTypes;
            }
        }
    }
}
