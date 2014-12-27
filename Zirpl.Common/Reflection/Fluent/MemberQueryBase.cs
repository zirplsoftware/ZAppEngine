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
    internal abstract class MemberQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery> : 
        IMemberQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>,
        IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>
        where TMemberInfo : MemberInfo
    {
        private readonly Type _type;
        private readonly BindingFlagsBuilder _bindingFlagsBuilder;
        private readonly AccessibilityMatcher _accessibilityMatcher;
        private readonly NameMatcher _nameMatcher;

        internal MemberQueryBase(Type type)
        {
            _type = type;
            _bindingFlagsBuilder = new BindingFlagsBuilder();
            _accessibilityMatcher = new AccessibilityMatcher();
            _nameMatcher = new NameMatcher();
        }

        #region Abstract methods

        protected abstract bool IsMatch(MemberInfo memberInfo);
        protected abstract MemberTypeFlags MemberTypes { get; }

        #endregion

        #region IMemberQuery implementation
        public TAccessibilityQuery WithAccessibility()
        {
            return (TAccessibilityQuery)(Object)this;
        }
        #endregion

        #region IAccessibilityQuery implementation
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Public()
        {
            _bindingFlagsBuilder.Public = true;
            _accessibilityMatcher.Public = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.NotPublic()
        {
            _bindingFlagsBuilder.NonPublic = true;
            _accessibilityMatcher.Private = true;
            _accessibilityMatcher.Protected = true;
            _accessibilityMatcher.Internal = true;
            _accessibilityMatcher.ProtectedInternal = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Private()
        {
            _bindingFlagsBuilder.NonPublic = true;
            _accessibilityMatcher.Private = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Protected()
        {
            _bindingFlagsBuilder.NonPublic = true;
            _accessibilityMatcher.Protected = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.Internal()
        {
            _bindingFlagsBuilder.NonPublic = true;
            _accessibilityMatcher.Internal = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TAccessibilityQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.ProtectedInternal()
        {
            _bindingFlagsBuilder.NonPublic = true;
            _accessibilityMatcher.ProtectedInternal = true;
            return (TAccessibilityQuery)(Object)this;
        }
        TMemberQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.All()
        {
            _bindingFlagsBuilder.Public = true;
            _bindingFlagsBuilder.NonPublic = true;
            _accessibilityMatcher.Public = true;
            _accessibilityMatcher.Private = true;
            _accessibilityMatcher.Protected = true;
            _accessibilityMatcher.Internal = true;
            _accessibilityMatcher.ProtectedInternal = true;
            return (TMemberQuery)(Object)this;
        }
        TMemberQuery IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>.And()
        {
            return (TMemberQuery)(Object)this;
        }
        #endregion

        public TMemberQuery AreInstance()
        {
            _bindingFlagsBuilder.Instance = true;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery AreStatic()
        {
            _bindingFlagsBuilder.Static = true;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery AreStaticInBaseTypes()
        {
            if (_bindingFlagsBuilder.FlattenHeirarchy) throw new InvalidOperationException("Cannot call IncludeStaticInBaseTypes after IncludeDeclaredOnly");

            _bindingFlagsBuilder.Static = true;
            _bindingFlagsBuilder.FlattenHeirarchy = true;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery AreDeclaredOnlyOnThisType()
        {
            if (_bindingFlagsBuilder.FlattenHeirarchy) throw new InvalidOperationException("Cannot call IncludeDeclaredOnly after IncludeStaticInBaseTypes");

            _bindingFlagsBuilder.DeclaredOnly = true;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery IgnoreCase()
        {
            _bindingFlagsBuilder.IgnoreCase = true;
            _nameMatcher.IgnoreCase = true;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery ByName(String name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (!_nameMatcher.Names.Contains(name))
            {
                _nameMatcher.Names.Add(name);
            }
            return (TMemberQuery)(Object)this;
        }

        #region IEnumerable implementation
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
            if (!_accessibilityMatcher.IsMatch(memberInfo)) return false;
            if (!_nameMatcher.IsMatch(memberInfo)) return false;
            return IsMatch(memberInfo);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region Nested classes

        private class NameMatcher
        {
            internal readonly IList<String> Names;
            internal bool IgnoreCase { get; set; }

            internal NameMatcher()
            {
                Names = new List<string>();
            }

            internal bool IsMatch(MemberInfo memberInfo)
            {
                if (Names.Any())
                {
                    var namesList = IgnoreCase
                           ? from o in Names select o.ToLowerInvariant()
                           : Names;
                    return namesList.Contains(IgnoreCase
                        ? memberInfo.Name.ToLowerInvariant()
                        : memberInfo.Name)
                           && IsMatch(memberInfo);
                }
                return true;
            }
        }

        private class AccessibilityMatcher
        {
            internal bool Public { get; set; }
            internal bool Private { get; set; }
            internal bool Protected { get; set; }
            internal bool ProtectedInternal { get; set; }
            internal bool Internal { get; set; }

            internal bool IsMatch(MemberInfo memberInfo)
            {
                if (!Public
                    || !Private
                    || !Protected
                    || !Internal
                    || !ProtectedInternal)
                {
                    if (memberInfo is MethodBase)
                    {
                        var method = (MethodBase)memberInfo;
                        if (!IsMethodMatch(method)) return false;
                    }
                    else if (memberInfo is EventInfo)
                    {
                        var eventInfo = (EventInfo)memberInfo;
                        var addMethod = eventInfo.GetAddMethod(true);
                        var removeMethod = eventInfo.GetRemoveMethod(true);
                        if (!IsMethodMatch(addMethod) && !IsMethodMatch(removeMethod)) return false;
                    }
                    else if (memberInfo is FieldInfo)
                    {
                        var field = (FieldInfo)memberInfo;
                        if (!Public && field.IsPublic) return false;
                        if (!Private && field.IsPrivate) return false;
                        if (!Protected && field.IsFamily) return false;
                        if (!ProtectedInternal && field.IsFamilyAndAssembly) return false;
                        if (!Internal && field.IsAssembly) return false;
                    }
                    else if (memberInfo is PropertyInfo)
                    {
                        var propertyinfo = (PropertyInfo)memberInfo;
                        var getMethod = propertyinfo.GetGetMethod(true);
                        var setMethod = propertyinfo.GetSetMethod(true);
                        if (!IsMethodMatch(getMethod) && !IsMethodMatch(setMethod)) return false;
                    }
                    else if (memberInfo is Type)
                    {
                        // nested types
                        var type = (Type) memberInfo;
                        if (!Public && type.IsPublic) return false;
                        if (!Private && type.IsNestedPrivate) return false;
                        if (!Protected && type.IsNestedFamily) return false;
                        if (!ProtectedInternal && type.IsNestedFamANDAssem) return false;
                        if (!Internal && type.IsNestedAssembly) return false;
                    }
                }
                return true;
            }

            private bool IsMethodMatch(MethodBase method)
            {
                if (method == null) return false;
                if (!Public && method.IsPublic) return false;
                if (!Private && method.IsPrivate) return false;
                if (!Protected && method.IsFamily) return false;
                if (!ProtectedInternal && method.IsFamilyAndAssembly) return false;
                if (!Internal && method.IsAssembly) return false;
                return true;
            }
        }

        #endregion
    }
}
