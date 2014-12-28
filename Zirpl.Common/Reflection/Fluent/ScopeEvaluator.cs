using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class ScopeEvaluator : IMemberEvaluator
    {
        private readonly Type _reflectedType;
        private int? _levelsDeep;
        internal bool Instance { get; set; }
        internal bool Static { get; set; }
        internal bool DeclaredOnThisType { get; set; }
        internal bool DeclaredOnBaseTypes { get; set; }
        // TODO: implement including hiddenBySignature members, and exclude them otherwise
        internal int? LevelsDeep
        {
            get
            {
                return _levelsDeep;
            }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("value", "Must be greater than 0");
                _levelsDeep = value;
            }
        }

        internal ScopeEvaluator(Type type)
        {
            _reflectedType = type;
        }

        public bool IsMatch(MemberInfo memberInfo)
        {
            if (memberInfo is MethodBase)
            {
                var method = (MethodBase)memberInfo;
                if (!IsMethodStaticScopeMatch(method)) return false;
                if (!IsDeclaredTypeMatch(method)) return false;
            }
            else if (memberInfo is EventInfo)
            {
                var eventInfo = (EventInfo)memberInfo;
                var addMethod = eventInfo.GetAddMethod(true);
                var removeMethod = eventInfo.GetRemoveMethod(true);
                if (!IsMethodStaticScopeMatch(addMethod) && !IsMethodStaticScopeMatch(removeMethod)) return false;
                if (!IsDeclaredTypeMatch(addMethod) && !IsDeclaredTypeMatch(removeMethod)) return false;
            }
            else if (memberInfo is FieldInfo)
            {
                var field = (FieldInfo)memberInfo;
                if (Static
                    || Instance)
                {
                    if (!Instance && !field.IsStatic) return false;
                    if (!Static && field.IsStatic) return false;
                }
                else
                {
                    // by default use instance
                    if (field.IsStatic) return false;
                }
                if (!IsDeclaredTypeMatch(field)) return false;
            }
            else if (memberInfo is PropertyInfo)
            {
                var propertyinfo = (PropertyInfo)memberInfo;
                var getMethod = propertyinfo.GetGetMethod(true);
                var setMethod = propertyinfo.GetSetMethod(true);
                if (!IsMethodStaticScopeMatch(getMethod) && !IsMethodStaticScopeMatch(setMethod)) return false;
                if (!IsDeclaredTypeMatch(getMethod) && !IsDeclaredTypeMatch(setMethod)) return false;
            }
            else if (memberInfo is Type)
            {
                // nested types
                var type = (Type)memberInfo;
                if (!IsDeclaredTypeMatch(memberInfo)) return false;
            }
            else
            {
                throw new UnexpectedCaseException("Unexpected MemberInfo type", memberInfo);
            }
            return true;
        }

        private bool IsMethodStaticScopeMatch(MethodBase method)
        {
            if (method == null) return false;
            if (Static
                || Instance)
            {
                if (method.IsStatic && !Static && Instance) return false;
                if (!method.IsStatic && !Instance && Static) return false;
            }
            else
            {
                // by default, just use Instance
                if (method.IsStatic) return false;
            }
            return true;
        }

        private bool IsDeclaredTypeMatch(MemberInfo memberInfo)
        {
            if (DeclaredOnBaseTypes
                || DeclaredOnThisType)
            {
                if (memberInfo.DeclaringType.Equals(_reflectedType) && !DeclaredOnThisType) return false;
                if (!memberInfo.DeclaringType.Equals(_reflectedType) && !DeclaredOnBaseTypes) return false;
                if (LevelsDeep.HasValue
                    && !memberInfo.DeclaringType.Equals(_reflectedType))
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
            }
            // if neither was chosen, then evaluate to true
            return true;
        }
    }
}
