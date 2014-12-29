using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zirpl.Collections;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MemberQueryService
    {
        private readonly Type _type;

        internal MemberQueryService(Type type)
        {
            _type = type;
        }

        internal IEnumerable<MemberInfo> FindMembers(MemberTypeFlags memberTypeFlags, BindingFlags bindingFlags)
        {
            return GetMembers(_type, memberTypeFlags, bindingFlags);
        }

        internal IEnumerable<MemberInfo> FindPrivateMembersOnBaseTypes(MemberTypeFlags memberTypes, BindingFlags bindingFlags, int levelsDeep)
        {
            var list = new List<MemberInfo>();
            var accessibilityEvaluator = new AccessibilityEvaluator();
            accessibilityEvaluator.Private = true;
            if (levelsDeep > 0)
            {
                var type = _type;
                var levelsDeeper = levelsDeep;
                while (type != null
                    && levelsDeeper > 0)
                {
                    list.AddRange(GetMembers(type, memberTypes, bindingFlags).Where(o => !list.Contains(o) && accessibilityEvaluator.IsMatch(o)));
                    type = type.BaseType;
                    levelsDeeper -= 1;
                }
            }
            else
            {
                var type = _type;
                while (type != null)
                {
                    list.AddRange(GetMembers(type, memberTypes, bindingFlags).Where(o => !list.Contains(o) && accessibilityEvaluator.IsMatch(o)));
                    type = type.BaseType;
                }
            }
            // TODO: check for hidden by signature
            return list;
        }

        private IEnumerable<MemberInfo> GetMembers(Type type, MemberTypeFlags memberTypes, BindingFlags bindingFlags)
        {
            var found = new List<MemberInfo>();

            if (memberTypes.HasFlag(MemberTypeFlags.Constructor))
            {
                found.AddRange(type.GetConstructors(bindingFlags));  
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Event))
            {
                found.AddRange(type.GetEvents(bindingFlags));
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Field))
            {
                found.AddRange(type.GetFields(bindingFlags));
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Method))
            {
                found.AddRange(type.GetMethods(bindingFlags));
            }
            if (memberTypes.HasFlag(MemberTypeFlags.NestedType))
            {
                found.AddRange(type.GetNestedTypes(bindingFlags));
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Property))
            {
                found.AddRange(type.GetProperties(bindingFlags));
            }

            //found.AddRange(type.FindMembers(memberTypes, bindingFlags, FindMemberMatch, null));
            return found;
        }
    }
}
