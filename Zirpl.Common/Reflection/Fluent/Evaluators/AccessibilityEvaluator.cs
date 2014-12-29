using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class AccessibilityEvaluator : IMatchEvaluator
    {
        internal bool Public { get; set; }
        internal bool Private { get; set; }
        internal bool Protected { get; set; }
        internal bool ProtectedInternal { get; set; }
        internal bool Internal { get; set; }

        public bool IsMatch(MemberInfo memberInfo)
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
                    if (Public
                       || Private
                       || Protected
                       || ProtectedInternal
                       || Internal)
                    {
                        if (!Public && field.IsPublic) return false;
                        if (!Private && field.IsPrivate) return false;
                        if (!Protected && field.IsFamily) return false;
                        if (!ProtectedInternal && field.IsFamilyAndAssembly) return false;
                        if (!Internal && field.IsAssembly) return false;
                    }
                    else
                    {
                        // default case is just to use Public
                        if (!field.IsPublic) return false;
                    }
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
                    if (Public
                         || Private
                         || Protected
                         || ProtectedInternal
                         || Internal)
                    {
                        if (!Public && type.IsPublic) return false;
                        if (!Private && type.IsNestedPrivate) return false;
                        if (!Protected && type.IsNestedFamily) return false;
                        if (!ProtectedInternal && type.IsNestedFamANDAssem) return false;
                        if (!Internal && type.IsNestedAssembly) return false;
                    }
                    else
                    {
                        // default case is just to use Public
                        if (!type.IsPublic) return false;
                    }
                }
                else
                {
                    throw new UnexpectedCaseException("Unexpected MemberInfo type", memberInfo);
                }
            }
            return true;
        }

        private bool IsMethodMatch(MethodBase method)
        {
            if (method == null) return false;
            if (Public
                || Private
                || Protected
                || ProtectedInternal
                || Internal)
            {
                if (!Public && method.IsPublic) return false;
                if (!Private && method.IsPrivate) return false;
                if (!Protected && method.IsFamily) return false;
                if (!ProtectedInternal && method.IsFamilyAndAssembly) return false;
                if (!Internal && method.IsAssembly) return false;
            }
            else
            {
                // default case is just to use Public
                if (!method.IsPublic) return false;
            }
            return true;
        }
    }
}
