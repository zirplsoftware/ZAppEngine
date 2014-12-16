using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Reflection;

namespace Zirpl.Common.Tests.Reflection
{
    [TestFixture]
    public class ReflectionTypeAccessorTests
    {
        public class MockParent
        {
            public int PublicProperty { get; set; }
            public virtual int PublicOverriddenProperty { get; set; }

            public int privatePropertyValue;
            private int PrivateProperty
            {
                get
                {
                    return this.privatePropertyValue;
                }
                set
                {
                    this.privatePropertyValue = value;
                }
            }

            public int protectedOverriddenPropertyValueOfParent;
            protected virtual int ProtectedOverriddenProperty
            {
                get
                {
                    return this.protectedOverriddenPropertyValueOfParent;
                }
                set
                {
                    this.protectedOverriddenPropertyValueOfParent = value;
                }
            }


            public int PublicField;
            private int PrivateField;
            protected int ProtectedField;
        }

        public class MockChild : MockParent
        {
            public int publicOverriddenPropertyValue;
            public override int PublicOverriddenProperty
            {
                get
                {
                    return this.publicOverriddenPropertyValue;
                }
                set
                {
                    this.publicOverriddenPropertyValue = value;
                }
            }
            public int protectedOverriddenPropertyValueOfChild;
            protected override int ProtectedOverriddenProperty
            {
                get
                {
                    return this.protectedOverriddenPropertyValueOfChild;
                }
                set
                {
                    this.protectedOverriddenPropertyValueOfChild = value;
                }
            }

            private int PrivateField;
        }

        [Test]
        public void TestGetSetPropertyValue()
        {
            var parent = new MockParent();
            var parentAccessor = TypeAccessorFactory.GetReflectionTypeAccessor(parent.GetType());

            parent.PublicProperty = 1;
            parentAccessor.GetPropertyValue(parent, "PublicProperty").Should().Be(1);
            parentAccessor.SetPropertyValue(parent, "PublicProperty", 2);
            parent.PublicProperty.Should().Be(2);

            parent.PublicOverriddenProperty = 1;
            parentAccessor.GetPropertyValue(parent, "PublicOverriddenProperty").Should().Be(1);
            parentAccessor.SetPropertyValue(parent, "PublicOverriddenProperty", 2);
            parent.PublicOverriddenProperty.Should().Be(2);

            parent.privatePropertyValue = 1;
            parentAccessor.GetPropertyValue(parent, "PrivateProperty").Should().Be(1);
            parentAccessor.SetPropertyValue(parent, "PrivateProperty", 2);
            parent.privatePropertyValue.Should().Be(2);

            parent.protectedOverriddenPropertyValueOfParent = 1;
            parentAccessor.GetPropertyValue(parent, "ProtectedOverriddenProperty").Should().Be(1);
            parentAccessor.SetPropertyValue(parent, "ProtectedOverriddenProperty", 2);
            parent.protectedOverriddenPropertyValueOfParent.Should().Be(2);

            var child = new MockChild();
            var childAccessor = TypeAccessorFactory.GetReflectionTypeAccessor(child.GetType());

            child.PublicProperty = 1;
            childAccessor.GetPropertyValue(child, "PublicProperty").Should().Be(1);
            childAccessor.SetPropertyValue(child, "PublicProperty", 2);
            child.PublicProperty.Should().Be(2);

            child.PublicOverriddenProperty = 1;
            childAccessor.GetPropertyValue(child, "PublicOverriddenProperty").Should().Be(1);
            childAccessor.SetPropertyValue(child, "PublicOverriddenProperty", 2);
            child.PublicOverriddenProperty.Should().Be(2);
            child.publicOverriddenPropertyValue.Should().Be(2);

            child.privatePropertyValue = 1;
            childAccessor.GetPropertyValue(child, "PrivateProperty").Should().Be(1);
            childAccessor.SetPropertyValue(child, "PrivateProperty", 2);
            child.privatePropertyValue.Should().Be(2);

            child.protectedOverriddenPropertyValueOfChild = 1;
            childAccessor.GetPropertyValue(child, "ProtectedOverriddenProperty").Should().Be(1);
            childAccessor.SetPropertyValue(child, "ProtectedOverriddenProperty", 2);
            child.protectedOverriddenPropertyValueOfChild.Should().Be(2);
            child.protectedOverriddenPropertyValueOfParent.Should().Be(0);


            MockParent childAsParent = new MockChild();
            var childAsParentAccessor = TypeAccessorFactory.GetReflectionTypeAccessor(childAsParent.GetType());

            childAsParent.PublicProperty = 1;
            childAsParentAccessor.GetPropertyValue(childAsParent, "PublicProperty").Should().Be(1);
            childAsParentAccessor.SetPropertyValue(childAsParent, "PublicProperty", 2);
            childAsParent.PublicProperty.Should().Be(2);

            childAsParent.PublicOverriddenProperty = 1;
            childAsParentAccessor.GetPropertyValue(childAsParent, "PublicOverriddenProperty").Should().Be(1);
            childAsParentAccessor.SetPropertyValue(childAsParent, "PublicOverriddenProperty", 2);
            childAsParent.PublicOverriddenProperty.Should().Be(2);
            ((MockChild)childAsParent).publicOverriddenPropertyValue.Should().Be(2);

            childAsParent.privatePropertyValue = 1;
            childAsParentAccessor.GetPropertyValue(childAsParent, "PrivateProperty").Should().Be(1);
            childAsParentAccessor.SetPropertyValue(childAsParent, "PrivateProperty", 2);
            childAsParent.privatePropertyValue.Should().Be(2);

            ((MockChild)childAsParent).protectedOverriddenPropertyValueOfChild = 1;
            childAsParentAccessor.GetPropertyValue(childAsParent, "ProtectedOverriddenProperty").Should().Be(1);
            childAsParentAccessor.SetPropertyValue(childAsParent, "ProtectedOverriddenProperty", 2);
            ((MockChild)childAsParent).protectedOverriddenPropertyValueOfChild.Should().Be(2);
            childAsParent.protectedOverriddenPropertyValueOfParent.Should().Be(0);

        }


        [Test]
        public void TestGetSetFieldValue()
        {
            var parent = new MockParent();
            var parentAccessor = TypeAccessorFactory.GetReflectionTypeAccessor(parent.GetType());

            parent.PublicField = 1;
            parentAccessor.GetFieldValue(parent, "PublicField").Should().Be(1);
            parentAccessor.SetFieldValue(parent, "PublicField", 2);
            parent.PublicField.Should().Be(2);

            parentAccessor.GetFieldValue(parent, "PrivateField").Should().Be(0);
            parentAccessor.SetFieldValue(parent, "PrivateField", 1);
            parentAccessor.GetFieldValue(parent, "PrivateField").Should().Be(1);

            parentAccessor.GetFieldValue(parent, "ProtectedField").Should().Be(0);
            parentAccessor.SetFieldValue(parent, "ProtectedField", 1);
            parentAccessor.GetFieldValue(parent, "ProtectedField").Should().Be(1);

            var child = new MockChild();
            var childAccessor = TypeAccessorFactory.GetReflectionTypeAccessor(child.GetType());

            child.PublicField = 1;
            childAccessor.GetFieldValue(child, "PublicField").Should().Be(1);
            childAccessor.SetFieldValue(child, "PublicField", 2);
            child.PublicField.Should().Be(2);

            childAccessor.GetFieldValue(child, "PrivateField").Should().Be(0);
            childAccessor.SetFieldValue(child, "PrivateField", 1);
            childAccessor.GetFieldValue(child, "PrivateField").Should().Be(1);
            parentAccessor.GetFieldValue(child, "PrivateField").Should().Be(0);

            childAccessor.GetFieldValue(child, "ProtectedField").Should().Be(0);
            childAccessor.SetFieldValue(child, "ProtectedField", 1);
            childAccessor.GetFieldValue(child, "ProtectedField").Should().Be(1);

            MockParent childAsParent = new MockChild();
            var childAsParentAccessor = TypeAccessorFactory.GetReflectionTypeAccessor(childAsParent.GetType());

            childAsParent.PublicField = 1;
            childAsParentAccessor.GetFieldValue(childAsParent, "PublicField").Should().Be(1);
            childAsParentAccessor.SetFieldValue(childAsParent, "PublicField", 2);
            child.PublicField.Should().Be(2);

            childAsParentAccessor.GetFieldValue(childAsParent, "PrivateField").Should().Be(0);
            childAsParentAccessor.SetFieldValue(childAsParent, "PrivateField", 1);
            childAsParentAccessor.GetFieldValue(childAsParent, "PrivateField").Should().Be(1);
            parentAccessor.GetFieldValue(childAsParent, "PrivateField").Should().Be(0);

            childAsParentAccessor.GetFieldValue(childAsParent, "ProtectedField").Should().Be(0);
            childAsParentAccessor.SetFieldValue(childAsParent, "ProtectedField", 1);
            childAsParentAccessor.GetFieldValue(childAsParent, "ProtectedField").Should().Be(1);

        }
    }
}
