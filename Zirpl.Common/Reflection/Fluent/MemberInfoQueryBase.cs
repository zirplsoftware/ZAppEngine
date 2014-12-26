using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal abstract class MemberInfoQueryBase<TSelf, TMemberInfo> : IEnumerable<TMemberInfo> 
        where TSelf : MemberInfoQueryBase<TSelf, TMemberInfo>
        where TMemberInfo : MemberInfo
    {
        private readonly Type _type;
        private readonly BindingFlagsBuilder _bindingFlagsBuilder;
        private readonly IList<String> _names;

        internal MemberInfoQueryBase(Type type)
        {
            _type = type;
            _bindingFlagsBuilder = new BindingFlagsBuilder();
            _names = new List<string>();
        }

        protected abstract bool IsMatch(MemberInfo memberInfo);
        protected abstract MemberTypeFlags MemberTypes { get; }

        public TSelf ArePublic()
        {
            _bindingFlagsBuilder.Public = true;
            return (TSelf)(Object)this;
        }

        public TSelf AreInstance()
        {
            _bindingFlagsBuilder.Instance = true;
            return (TSelf)(Object)this;
        }

        public TSelf AreStatic()
        {
            _bindingFlagsBuilder.Static = true;
            return (TSelf)(Object)this;
        }

        public TSelf AreNotPublic()
        {
            _bindingFlagsBuilder.NonPublic = true;
            return (TSelf)(Object)this;
        }

        public TSelf AreStaticInBaseTypes()
        {
            if (_bindingFlagsBuilder.FlattenHeirarchy) throw new InvalidOperationException("Cannot call IncludeStaticInBaseTypes after IncludeDeclaredOnly");

            _bindingFlagsBuilder.Static = true;
            _bindingFlagsBuilder.FlattenHeirarchy = true;
            return (TSelf)(Object)this;
        }

        public TSelf AreDeclaredOnlyOnThisType()
        {
            if (_bindingFlagsBuilder.FlattenHeirarchy) throw new InvalidOperationException("Cannot call IncludeDeclaredOnly after IncludeStaticInBaseTypes");

            _bindingFlagsBuilder.DeclaredOnly = true;
            return (TSelf)(Object)this;
        }

        public TSelf IgnoreCase()
        {
            _bindingFlagsBuilder.IgnoreCase = true;
            return (TSelf)(Object)this;
        }

        public TSelf ByName(String name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (!_names.Contains(name))
            {
                _names.Add(name);
            }
            return (TSelf)(Object)this;
        }

        public IEnumerator<TMemberInfo> GetEnumerator()
        {
            var list = new List<TMemberInfo>();
#if PORTABLE
            var found = new List<TMemberInfo>();
            if (this.MemberTypes.HasFlag(MemberTypeFlags.Constructor))
            {
                found.AddRange(_type.GetConstructors(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (this.MemberTypes.HasFlag(MemberTypeFlags.Event))
            {
                found.AddRange(_type.GetEvents(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (this.MemberTypes.HasFlag(MemberTypeFlags.Field))
            {
                found.AddRange(_type.GetFields(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (this.MemberTypes.HasFlag(MemberTypeFlags.Method))
            {
                found.AddRange(_type.GetMethods(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (this.MemberTypes.HasFlag(MemberTypeFlags.NestedType))
            {
                found.AddRange(_type.GetNestedTypes(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
            if (this.MemberTypes.HasFlag(MemberTypeFlags.Property))
            {
                found.AddRange(_type.GetProperties(_bindingFlagsBuilder.BindingFlags).Where(o => FindMemberMatch(o, null)).Select(o => (TMemberInfo)(Object)o));
            }
#else
            var found = _type.FindMembers((MemberTypes)MemberTypes, _bindingFlagsBuilder.BindingFlags, FindMemberMatch, null);
#endif
            list.AddRange(found.Select(o => (TMemberInfo)o));
            return list.GetEnumerator();
        }

        private bool FindMemberMatch(MemberInfo memberInfo, Object searchCriteria)
        {
            if (_names.Any())
            {
                var namesList = !_bindingFlagsBuilder.IgnoreCase
                       ? from o in _names select o.ToLowerInvariant()
                       : _names;
                return namesList.Contains(_bindingFlagsBuilder.IgnoreCase
                    ? memberInfo.Name.ToLowerInvariant()
                    : memberInfo.Name)
                       && IsMatch(memberInfo);
            }
            else
            {
                return IsMatch(memberInfo);   
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
