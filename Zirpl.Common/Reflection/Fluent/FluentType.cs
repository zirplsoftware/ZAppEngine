using System;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class FluentType
    {
        private readonly Type _type;

        internal FluentType(Type type)
        {
            _type = type;
        }

        public PropertyInfoQuery Properties 
        {
            get
            {
                return new PropertyInfoQuery(_type);
            }
        }

        public FieldInfoQuery Fields
        {
            get
            {
                return new FieldInfoQuery(_type);
            }
        }

        public MethodInfoQuery Methods
        {
            get
            {
                return new MethodInfoQuery(_type);
            }
        }

        public ConstructorInfoQuery Constructors
        {
            get
            {
                return new ConstructorInfoQuery(_type);
            }
        }

        public NestedTypeQuery NestedTypes
        {
            get
            {
                return new NestedTypeQuery(_type);
            }
        }

        public EventInfoQuery Events
        {
            get
            {
                return new EventInfoQuery(_type);
            }
        }

        public MemberInfoQuery Members
        {
            get
            {
                return new MemberInfoQuery(_type);
            }
        }
        public FluentType FluentBaseType
        {
            get
            {
                if (_type.BaseType != null)
                {
                    _type.BaseType.Fluent();
                }
                return null;
            }
        }

        public Type Type
        {
            get { return _type; }
        }
    }
}
