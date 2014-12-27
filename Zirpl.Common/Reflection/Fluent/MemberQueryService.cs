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

        internal IEnumerable<MemberInfo> FindMembers(MemberTypes memberTypes, BindingFlags bindingFlags, String name = null, bool includePrivateOnBaseTypes = false, int? levelsDeep = null)
        {
            var list = new List<MemberInfo>();
            if (includePrivateOnBaseTypes)
            {
                // TODO: this is NOT correct, due to re-returning ALL on the base classes
                if (levelsDeep.HasValue)
                {
                    var type = _type;
                    var levelsDeeper = levelsDeep.Value;
                    while (type != null
                        && levelsDeeper > 0)
                    {
                        list.AddRange(GetMembers(_type).Where(o => !list.Contains(o)));
                        type = type.BaseType;
                        levelsDeeper -= 1;
                    }
                }
                else
                {
                    var type = _type;
                    while (type != null)
                    {
                        list.AddRange(GetMembers(_type).Where(o => !list.Contains(o)));
                        type = type.BaseType;
                    }
                }
            }
            else
            {
                list.AddRange(GetMembers(_type));
            }
            return list;
        }

        private IEnumerable<MemberInfo> GetMembers(Type type)
        {
            var found = new List<MemberInfo>();
#if PORTABLE
            if (MemberTypes.HasFlag(MemberTypeFlags.Constructor))
            {
                found.AddRange(type.GetConstructors(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (MemberTypes.HasFlag(MemberTypeFlags.Event))
            {
                found.AddRange(type.GetEvents(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (MemberTypes.HasFlag(MemberTypeFlags.Field))
            {
                found.AddRange(type.GetFields(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (MemberTypes.HasFlag(MemberTypeFlags.Method))
            {
                found.AddRange(type.GetMethods(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (MemberTypes.HasFlag(MemberTypeFlags.NestedType))
            {
                found.AddRange(type.GetNestedTypes(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (MemberTypes.HasFlag(MemberTypeFlags.Property))
            {
                found.AddRange(type.GetProperties(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
#else
            found.AddRange(type.FindMembers((MemberTypes)MemberTypesBuilder.MemberTypes, BindingFlagsBuilder.BindingFlags, FindMemberMatch, null));
#endif
            return found;
        }

        private bool FindMemberMatch(MemberInfo memberInfo, Object searchCriteria)
        {
            return true;
        }
    }
}
