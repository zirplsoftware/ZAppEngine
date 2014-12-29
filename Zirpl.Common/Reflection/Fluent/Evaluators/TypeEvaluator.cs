using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal class TypeEvaluator : IMatchEvaluator
    {
        // TODO: how can these be used? Type.FindInterfaces, Type.IsIstanceOf, Type.IsSubClassOf

        internal NameEvaluator NameEvaluator { get; private set; }
        internal TypeFullNameEvaluator FullNameEvaluator { get; private set; }
        internal Type AssignableFrom { get; set; }
        internal IEnumerable<Type> AssignableFroms { get; set; }
        internal Type AssignableTo { get; set; }
        internal IEnumerable<Type> AssignableTos { get; set; }
        internal bool Any { get; set; }

        internal TypeEvaluator()
        {
            NameEvaluator = new NameEvaluator();
            FullNameEvaluator = new TypeFullNameEvaluator();
        }

        // TODO: implement all these
        //private bool _isValueType;
        //private bool _isNullableValueType;
        //private bool _isValueTypeOrNullableValueType;
        //private bool _isEnum;
        //private bool _isNullableEnum;
        //private bool _isEnumOrIsNullableEnum;
        //private bool _isClass;
        //private bool _isInterface;
        //private bool _isClassOrInterface;
        //private bool _isPrimtive;
        //private Type _implementingInterface;
        //private IEnumerable<Type> _implementingAllInterfaces;
        //private IEnumerable<Type> _implementingAnyInterfaces;
        //private Type _exact;
        //private IEnumerable<Type> _exactAny;

        public virtual bool IsMatch(MemberInfo memberInfo)
        {
            var type = (Type) memberInfo;
            if (AssignableFrom != null
                   || AssignableFroms != null
                   || AssignableTo != null
                   || AssignableTos != null)
            {
                if (type == null)
                {
                    return false;
                }
                else
                {
                    if (AssignableFrom != null && !type.IsAssignableFrom(AssignableFrom)) return false;
                    if (AssignableTo != null && !AssignableTo.IsAssignableFrom(type)) return false;
                    if (AssignableFroms != null && !Any && !AssignableFroms.All(type.IsAssignableFrom)) return false;
                    if (AssignableTos != null && !Any && !AssignableTos.All(o => o.IsAssignableFrom(type))) return false;
                    if (AssignableFroms != null && Any && !AssignableFroms.Any(type.IsAssignableFrom)) return false;
                    if (AssignableTos != null && Any && !AssignableTos.All(o => o.IsAssignableFrom(type))) return false;
                }
            }
            return NameEvaluator.IsMatch(type) && FullNameEvaluator.IsMatch(type);
        }
    }
}
