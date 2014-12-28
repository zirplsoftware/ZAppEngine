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

        internal IEnumerable<MemberInfo> FindMembers(MemberTypeFlags memberTypes, BindingFlags bindingFlags, IEnumerable<String> names)
        {
            return GetMembers(_type, memberTypes, bindingFlags, names);
        }

        internal IEnumerable<MemberInfo> FindPrivateMembersOnBaseTypes(MemberTypeFlags memberTypes, BindingFlags bindingFlags, IEnumerable<String> names, int levelsDeep)
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
                    list.AddRange(GetMembers(type, memberTypes, bindingFlags, names).Where(o => !list.Contains(o) && accessibilityEvaluator.IsMatch(o)));
                    type = type.BaseType;
                    levelsDeeper -= 1;
                }
            }
            else
            {
                var type = _type;
                while (type != null)
                {
                    list.AddRange(GetMembers(type, memberTypes, bindingFlags, names).Where(o => !list.Contains(o) && accessibilityEvaluator.IsMatch(o)));
                    type = type.BaseType;
                }
            }
            // TODO: check for hidden by signature
            return list;
        }

        private IEnumerable<MemberInfo> GetMembers(Type type, MemberTypeFlags memberTypes, BindingFlags bindingFlags, IEnumerable<String> names)
        {
            var found = new List<MemberInfo>();

            if (memberTypes.HasFlag(MemberTypeFlags.Constructor))
            {
                if (names != null
                    && names.Any())
                {
                    // do nothing as there are no constructors by name
                }
                else
                {
                    found.AddRange(type.GetConstructors(bindingFlags));   
                }
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Event))
            {
                if (names != null
                    && names.Any())
                {
                    foreach (var name in names)
                    {
                        found.Add(type.GetEvent(name, bindingFlags));
                    }
                }
                else
                {
                    found.AddRange(type.GetEvents(bindingFlags));
                }
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Field))
            {
                if (names != null
                    && names.Any())
                {
                    foreach (var name in names)
                    {
                        found.Add(type.GetField(name, bindingFlags));
                    }
                }
                else
                {
                    found.AddRange(type.GetFields(bindingFlags));
                }
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Method))
            {
                if (names != null
                    && names.Any())
                {
                    foreach (var name in names)
                    {
                        found.Add(type.GetMethod(name, bindingFlags));
                    }
                }
                else
                {
                    found.AddRange(type.GetMethods(bindingFlags));
                }
            }
            if (memberTypes.HasFlag(MemberTypeFlags.NestedType))
            {
                if (names != null
                    && names.Any())
                {
                    foreach (var name in names)
                    {
                        found.Add(type.GetNestedType(name, bindingFlags));
                    }
                }
                else
                {
                    found.AddRange(type.GetNestedTypes(bindingFlags));
                }
            }
            if (memberTypes.HasFlag(MemberTypeFlags.Property))
            {
                if (names != null
                    && names.Any())
                {
                    foreach (var name in names)
                    {
                        found.Add(type.GetProperty(name, bindingFlags));
                    }
                }
                else
                {
                    found.AddRange(type.GetProperties(bindingFlags));
                }
            }

            //found.AddRange(type.FindMembers(memberTypes, bindingFlags, FindMemberMatch, null));
            return found;
        }
    }
}
