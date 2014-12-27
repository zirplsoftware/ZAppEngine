using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zirpl.Collections;

namespace Zirpl.Reflection.Fluent
{
    internal abstract class MemberQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery, TScopeQuery> : 
        IMemberQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery, TScopeQuery>,
        IAccessibilityQueryBase<TMemberInfo, TMemberQuery, TAccessibilityQuery>,
        IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>
        where TMemberInfo : MemberInfo
    {
        private readonly Type _type;
        private readonly BindingFlagsBuilder _bindingFlagsBuilder;
        private readonly AccessibilityMatcher _accessibilityMatcher;
        private readonly NameMatcher _nameMatcher;
        private readonly ScopeMatcher _scopeMatcher;

        internal MemberQueryBase(Type type)
        {
            _type = type;
            _bindingFlagsBuilder = new BindingFlagsBuilder();
            _accessibilityMatcher = new AccessibilityMatcher();
            _nameMatcher = new NameMatcher();
            _scopeMatcher = new ScopeMatcher(type);
        }

        #region Abstract methods

        protected abstract bool IsMatch(MemberInfo memberInfo);
        protected abstract MemberTypeFlags MemberTypes { get; }

        #endregion

        #region IMemberQuery implementation
        public TAccessibilityQuery OfAccessibility()
        {
            return (TAccessibilityQuery)(Object)this;
        }
        public TScopeQuery OfScope()
        {
            return (TScopeQuery)(Object)this;
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
      
        #region IScopeQuery implementation
        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.Instance()
        {
            _bindingFlagsBuilder.Instance = true;
            _scopeMatcher.Instance = true;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.Static()
        {
            _bindingFlagsBuilder.Static = true;
            _scopeMatcher.Static = true;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.DeclaredOnThisType()
        {
            _scopeMatcher.DeclaredOnThisType = true;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.DeclaredOnBaseTypes(int levelsDeep)
        {
            if (levelsDeep <= 0) throw new ArgumentOutOfRangeException("levelsDeep", "Must be greater than 0");

            _scopeMatcher.DeclaredOnBaseTypes = true;
            _scopeMatcher.LevelsDeep = levelsDeep;
            return (TScopeQuery)(Object)this;
        }

        TScopeQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.DeclaredOnBaseTypes()
        {
            _scopeMatcher.DeclaredOnBaseTypes = true;
            return (TScopeQuery)(Object)this;
        }

        TMemberQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.All(int levelsDeep)
        {
            if (levelsDeep <= 0) throw new ArgumentOutOfRangeException("levelsDeep", "Must be greater than 0");

            _bindingFlagsBuilder.Instance = true;
            _bindingFlagsBuilder.Static = true;
            _scopeMatcher.Instance = true;
            _scopeMatcher.Static = true;
            _scopeMatcher.DeclaredOnThisType = true;
            _scopeMatcher.DeclaredOnBaseTypes = true;
            _scopeMatcher.LevelsDeep = levelsDeep;
            return (TMemberQuery)(Object)this;
        }

        TMemberQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.All()
        {
            _bindingFlagsBuilder.Instance = true;
            _bindingFlagsBuilder.Static = true;
            _scopeMatcher.Instance = true;
            _scopeMatcher.Static = true;
            _scopeMatcher.DeclaredOnThisType = true;
            _scopeMatcher.DeclaredOnBaseTypes = true;
            return (TMemberQuery)(Object)this;
        }

        TMemberQuery IScopeQueryBase<TMemberInfo, TMemberQuery, TScopeQuery>.And()
        {
            return (TMemberQuery)(Object)this;
        }
        #endregion
        
        public TMemberQuery IgnoreCase()
        {
            _bindingFlagsBuilder.IgnoreCase = true;
            _nameMatcher.IgnoreCase = true;
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery Named(String name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            if (!_nameMatcher.Names.Contains(name))
            {
                _nameMatcher.Names.Add(name);
            }
            return (TMemberQuery)(Object)this;
        }

        public TMemberQuery NamedIn(IEnumerable<String> names)
        {
            if (names == null) throw new ArgumentNullException("names");

            _nameMatcher.Names.AddRange(names.Where(o => !_nameMatcher.Names.Contains(o)));
            return (TMemberQuery)(Object)this;
        }

        #region IEnumerable implementation
        public IEnumerator<TMemberInfo> GetEnumerator()
        {
            var list = new List<TMemberInfo>();
            if (_scopeMatcher.DeclaredOnBaseTypes)
            {
                if (_scopeMatcher.LevelsDeep.HasValue)
                {
                    var type = _type;
                    var levelsDeeper = _scopeMatcher.LevelsDeep.Value;
                    while (type != null
                        && levelsDeeper > 0)
                    {
                        list.AddRange(GetMembers(_type));
                        type = type.BaseType;
                        levelsDeeper -= 1;
                    }
                }
                else
                {
                    var type = _type;
                    while (type != null)
                    {
                        list.AddRange(GetMembers(_type));
                        type = type.BaseType;
                    }
                }
            }
            else
            {
                list.AddRange(GetMembers(_type));   
            }
            return list.GetEnumerator();
        }

        private IEnumerable<TMemberInfo> GetMembers(Type type)
        {
            var found = new List<TMemberInfo>();
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
            found.AddRange(type.FindMembers((MemberTypes)MemberTypes, _bindingFlagsBuilder.BindingFlags, FindMemberMatch, null));
#endif
            return found;
        }

        private bool FindMemberMatch(MemberInfo memberInfo, Object searchCriteria)
        {
            if (!_accessibilityMatcher.IsMatch(memberInfo)) return false;
            if (!_nameMatcher.IsMatch(memberInfo)) return false;
            if (!_scopeMatcher.IsMatch(memberInfo)) return false;
            return IsMatch(memberInfo);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region Nested types

        [Flags]
        internal enum MemberTypeFlags
        {
            Constructor = 1,
            Event = 2,
            Field = 4,
            Method = 8,
            NestedType = 128,
            Property = 16,
            TypeInfo = 32,
            Custom = 64,
            All = 191
        }

        private sealed class ScopeMatcher
        {
            private readonly Type _reflectedType;
            internal bool Instance { get; set; }
            internal bool Static { get; set; }
            internal bool DeclaredOnThisType { get; set; }
            internal bool DeclaredOnBaseTypes { get; set; }
            internal int? LevelsDeep { get; set; }

            internal ScopeMatcher(Type type)
            {
                _reflectedType = type;
            }


            internal bool IsMatch(MemberInfo memberInfo)
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
                    if (!Instance && !field.IsStatic) return false;
                    if (!Static && field.IsStatic) return false;
                    if (!IsDeclaredTypeMatch(field)) return false;
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
                    var type = (Type)memberInfo;
                    if (!IsDeclaredTypeMatch(memberInfo)) return false;
                }
                return true;
            }

            private bool IsMethodMatch(MethodBase method)
            {
                if (method == null) return false;
                if (!Instance && !method.IsStatic) return false;
                if (!Static && method.IsStatic) return false;
                return IsDeclaredTypeMatch(method);
            }

            private bool IsDeclaredTypeMatch(MemberInfo memberInfo)
            {
                if (!DeclaredOnThisType && memberInfo.DeclaringType.Equals(_reflectedType)) return false;
                if (!DeclaredOnBaseTypes && !memberInfo.DeclaringType.Equals(_reflectedType)) return false;
                if (DeclaredOnBaseTypes
                    && !memberInfo.DeclaringType.Equals(_reflectedType)
                    && LevelsDeep.HasValue)
                {
                    var found = false;
                    var type = _reflectedType.BaseType;
                    int levelsDeeper = LevelsDeep.Value - 1;
                    while (type != null)
                    {
                        if (memberInfo.DeclaringType.Equals(type))
                        {
                            found = true;
                            type = null;
                        }
                        else
                        {
                            type = levelsDeeper == 0 ? null : type.BaseType;
                            levelsDeeper -= 1;
                        }
                    }
                    return found;
                }
                return true;
            }
        }

        private sealed class NameMatcher
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

        private sealed class AccessibilityMatcher
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

        private sealed class BindingFlagsBuilder
        {
            internal bool Public { get; set; }
            internal bool Instance { get; set; }
            internal bool Static { get; set; }
            internal bool NonPublic { get; set; }
            internal bool IgnoreCase { get; set; }

            internal BindingFlags BindingFlags
            {
                get
                {
                    var bindings = BindingFlags.DeclaredOnly;
                    bindings = Public ? bindings | BindingFlags.Public : bindings;
                    bindings = NonPublic ? bindings | BindingFlags.NonPublic : bindings;
                    bindings = Instance ? bindings | BindingFlags.Instance : bindings;
                    bindings = Static ? bindings | BindingFlags.Static : bindings;
                    bindings = IgnoreCase ? bindings | BindingFlags.IgnoreCase : bindings;
                    return bindings;
                }
            }
        }

        #endregion
    }
}
