using System;

namespace Zirpl.Reflection.Fluent
{
    public sealed class FluentType
    {
        private readonly Type _type;

        internal FluentType(Type type)
        {
            _type = type;
        }

        public IPropertyQuery Properties 
        {
            get
            {
                return new PropertyQuery(_type);
            }
        }

        public IFieldQuery Fields
        {
            get
            {
                return new FieldQuery(_type);
            }
        }

        public IMethodQuery Methods
        {
            get
            {
                return new MethodQuery(_type);
            }
        }

        public IConstructorQuery Constructors
        {
            get
            {
                return new ConstructorQuery(_type);
            }
        }

        public INestedTypeQuery NestedTypes
        {
            get
            {
                return new NestedTypeQuery(_type);
            }
        }

        public IEventQuery Events
        {
            get
            {
                return new EventQuery(_type);
            }
        }

        public IMemberQuery Members
        {
            get
            {
                return new MemberQuery(_type);
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
