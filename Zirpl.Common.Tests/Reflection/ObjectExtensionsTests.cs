using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Telerik.JustMock;
using Zirpl.Reflection;

namespace Zirpl.Common.Tests.Reflection
{
    [TestFixture]
    public class ObjectExtensionsTests
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



            public bool methodCalled = false;
            public int parameter1Value = 0;
            public int parameter2Value = 0;

            public void PublicMethodNoParamatersNoReturn()
            {
                this.methodCalled = true;
            }

            public void PublicMethodWith1Parameter(int parameter1)
            {
                this.parameter1Value = parameter1;
            }

            public void PublicMethodWith2Parameters(int parameter1, int parameter2)
            {
                this.parameter1Value = parameter1;
                this.parameter2Value = parameter2;
            }
            private void PrivateMethodNoParamatersNoReturn()
            {
                this.methodCalled = true;
            }

            private void PrivateMethodWith1Parameter(int parameter1)
            {
                this.parameter1Value = parameter1;
            }

            private void PrivateMethodWith2Parameters(int parameter1, int parameter2)
            {
                this.parameter1Value = parameter1;
                this.parameter2Value = parameter2;
            }
            protected virtual void ProtectedMethodNoParamatersNoReturn()
            {
                this.methodCalled = true;
            }

            protected virtual void ProtectedMethodWith1Parameter(int parameter1)
            {
                this.parameter1Value = parameter1;
            }

            protected virtual void ProtectedMethodWith2Parameters(int parameter1, int parameter2)
            {
                this.parameter1Value = parameter1;
                this.parameter2Value = parameter2;
            }


            public void OverloadedMethod()
            {
                this.methodCalled = true;
            }

            public void OverloadedMethod(int parameter1)
            {
                this.parameter1Value = parameter1;
            }
            public void OverloadedMethod(Object parameter1)
            {
                //this.parameter1Value = parameter1;
            }

            public void OverloadedMethod(int parameter1, int parameter2)
            {
                this.parameter1Value = parameter1;
                this.parameter2Value = parameter2;
            }
            public void OverloadedMethod(String parameter1, int parameter2)
            {
            //    this.parameter1Value = parameter1.GetValueOrDefault();
                this.parameter2Value = parameter2;
            }
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


            public bool childMethodCalled = false;
            public int childParameter1Value = 0;
            public int childParameter2Value = 0;
            protected override void ProtectedMethodNoParamatersNoReturn()
            {
                this.childMethodCalled = true;
            }

            protected override void ProtectedMethodWith1Parameter(int parameter1)
            {
                this.childParameter1Value = parameter1;
            }

            protected override void ProtectedMethodWith2Parameters(int parameter1, int parameter2)
            {
                this.childParameter1Value = parameter1;
                this.childParameter2Value = parameter2;
            }

        }

        [Test]
        public void TestInvokeMethod()
        {
            var parent = new MockParent();
            parent.GetAccessor().InvokeMethod("OverloadedMethod", new object());
            parent.methodCalled.Should().BeFalse();
            parent.parameter1Value.Should().Be(0);
            parent.parameter2Value.Should().Be(0);

            new Action(() => new MockParent().GetAccessor().InvokeMethod("NonExistentMethod")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => new MockParent().GetAccessor().InvokeMethod(null)).ShouldThrow<ArgumentNullException>();
            // wrong param type
            new Action(() => new MockParent().GetAccessor().InvokeMethod("PublicMethodWith1Parameter", "test"))
                .ShouldThrow<ArgumentException>();
            // wrong number of parameters
            new Action(() => new MockParent().GetAccessor().InvokeMethod("PublicMethodWith1Parameter", 1, 2)).ShouldThrow<ArgumentOutOfRangeException>();


            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("OverloadedMethod");
            parent.methodCalled.Should().BeTrue();
            parent.parameter1Value.Should().Be(0);
            parent.parameter2Value.Should().Be(0);
            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("OverloadedMethod", null);
            parent.methodCalled.Should().BeFalse();
            parent.parameter1Value.Should().Be(0);
            parent.parameter2Value.Should().Be(0);
            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("OverloadedMethod", 1, 2);
            parent.methodCalled.Should().BeFalse();
            parent.parameter1Value.Should().Be(1);
            parent.parameter2Value.Should().Be(2);
            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("OverloadedMethod", null, 2);
            parent.methodCalled.Should().BeFalse();
            parent.parameter1Value.Should().Be(0);
            parent.parameter2Value.Should().Be(2);
            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("OverloadedMethod", "test", 2);
            parent.methodCalled.Should().BeFalse();
            parent.parameter1Value.Should().Be(0);
            parent.parameter2Value.Should().Be(2);
            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("OverloadedMethod", 1);
            parent.methodCalled.Should().BeFalse();
            parent.parameter1Value.Should().Be(1);
            parent.parameter2Value.Should().Be(0);

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("PublicMethodNoParamatersNoReturn");
            parent.methodCalled.Should().BeTrue();

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("PublicMethodWith1Parameter", 1);
            parent.parameter1Value.Should().Be(1);

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("PublicMethodWith2Parameters", 1, 2);
            parent.parameter1Value.Should().Be(1);
            parent.parameter2Value.Should().Be(2);

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("PrivateMethodNoParamatersNoReturn");
            parent.methodCalled.Should().BeTrue();

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("PrivateMethodWith1Parameter", 1);
            parent.parameter1Value.Should().Be(1);

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("PrivateMethodWith2Parameters", 1, 2);
            parent.parameter1Value.Should().Be(1);
            parent.parameter2Value.Should().Be(2);

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("ProtectedMethodNoParamatersNoReturn");
            parent.methodCalled.Should().BeTrue();

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("ProtectedMethodWith1Parameter", 1);
            parent.parameter1Value.Should().Be(1);

            parent = new MockParent();
            parent.GetAccessor().InvokeMethod("ProtectedMethodWith2Parameters", 1, 2);
            parent.parameter1Value.Should().Be(1);
            parent.parameter2Value.Should().Be(2);

            // calling the parent methods through the child class
            //
            var child = new MockChild();
            child.GetAccessor().InvokeMethod("PublicMethodNoParamatersNoReturn");
            child.methodCalled.Should().BeTrue();

            child = new MockChild();
            child.GetAccessor().InvokeMethod("PublicMethodWith1Parameter", 1);
            child.parameter1Value.Should().Be(1);

            child = new MockChild();
            child.GetAccessor().InvokeMethod("PublicMethodWith2Parameters", 1, 2);
            child.parameter1Value.Should().Be(1);
            child.parameter2Value.Should().Be(2);

            child = new MockChild();
            child.GetAccessor().InvokeMethod("PrivateMethodNoParamatersNoReturn");
            child.methodCalled.Should().BeTrue();

            child = new MockChild();
            child.GetAccessor().InvokeMethod("PrivateMethodWith1Parameter", 1);
            child.parameter1Value.Should().Be(1);

            child = new MockChild();
            child.GetAccessor().InvokeMethod("PrivateMethodWith2Parameters", 1, 2);
            child.parameter1Value.Should().Be(1);
            child.parameter2Value.Should().Be(2);

            child = new MockChild();
            child.GetAccessor().InvokeMethod("ProtectedMethodNoParamatersNoReturn");
            child.childMethodCalled.Should().BeTrue();
            child.methodCalled.Should().BeFalse();

            child = new MockChild();
            child.GetAccessor().InvokeMethod("ProtectedMethodWith1Parameter", 1);
            child.childParameter1Value.Should().Be(1);
            child.parameter1Value.Should().Be(0);

            child = new MockChild();
            child.GetAccessor().InvokeMethod("ProtectedMethodWith2Parameters", 1, 2);
            child.childParameter1Value.Should().Be(1);
            child.childParameter2Value.Should().Be(2);
            child.parameter1Value.Should().Be(0);
            child.parameter2Value.Should().Be(0);
        }

        [Test]
        public void TestGetSetPropertyValue()
        {
            var parent = new MockParent();

            new Action(() => parent.GetAccessor().GetProperty<String>("PublicProperty")).ShouldThrow<InvalidCastException>();
            new Action(() => parent.GetAccessor().SetProperty("PublicProperty", "test")).ShouldThrow<InvalidCastException>();

            new Action(() => parent.GetAccessor().GetProperty<String>("NonExistentProperty")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => parent.GetAccessor().SetProperty("NonExistentProperty", "test")).ShouldThrow<ArgumentOutOfRangeException>();

            new Action(() => parent.GetAccessor().GetProperty<String>(null)).ShouldThrow<ArgumentNullException>();
            new Action(() => parent.GetAccessor().SetProperty(null, "test")).ShouldThrow<ArgumentNullException>();

            parent.PublicProperty = 1;
            parent.GetAccessor().GetProperty<int>("PublicProperty").Should().Be(1);
            parent.GetAccessor().SetProperty("PublicProperty", 2);
            parent.PublicProperty.Should().Be(2);

            parent.PublicOverriddenProperty = 1;
            parent.GetAccessor().GetProperty<int>("PublicOverriddenProperty").Should().Be(1);
            parent.GetAccessor().SetProperty("PublicOverriddenProperty", 2);
            parent.PublicOverriddenProperty.Should().Be(2);

            parent.privatePropertyValue = 1;
            parent.GetAccessor().GetProperty<int>("PrivateProperty").Should().Be(1);
            parent.GetAccessor().SetProperty("PrivateProperty", 2);
            parent.privatePropertyValue.Should().Be(2);

            parent.protectedOverriddenPropertyValueOfParent = 1;
            parent.GetAccessor().GetProperty<int>("ProtectedOverriddenProperty").Should().Be(1);
            parent.GetAccessor().SetProperty("ProtectedOverriddenProperty", 2);
            parent.protectedOverriddenPropertyValueOfParent.Should().Be(2);

            var child = new MockChild();

            child.PublicProperty = 1;
            child.GetAccessor().GetProperty<int>("PublicProperty").Should().Be(1);
            child.GetAccessor().SetProperty("PublicProperty", 2);
            child.PublicProperty.Should().Be(2);

            child.PublicOverriddenProperty = 1;
            child.GetAccessor().GetProperty<int>("PublicOverriddenProperty").Should().Be(1);
            child.GetAccessor().SetProperty("PublicOverriddenProperty", 2);
            child.PublicOverriddenProperty.Should().Be(2);
            child.publicOverriddenPropertyValue.Should().Be(2);

            child.privatePropertyValue = 1;
            child.GetAccessor().GetProperty<int>("PrivateProperty").Should().Be(1);
            child.GetAccessor().SetProperty("PrivateProperty", 2);
            child.privatePropertyValue.Should().Be(2);

            child.protectedOverriddenPropertyValueOfChild = 1;
            child.GetAccessor().GetProperty<int>("ProtectedOverriddenProperty").Should().Be(1);
            child.GetAccessor().SetProperty("ProtectedOverriddenProperty", 2);
            child.protectedOverriddenPropertyValueOfChild.Should().Be(2);
            child.protectedOverriddenPropertyValueOfParent.Should().Be(0);


            MockParent childAsParent = new MockChild();

            childAsParent.PublicProperty = 1;
            childAsParent.GetAccessor().GetProperty<int>("PublicProperty").Should().Be(1);
            childAsParent.GetAccessor().SetProperty("PublicProperty", 2);
            childAsParent.PublicProperty.Should().Be(2);

            childAsParent.PublicOverriddenProperty = 1;
            childAsParent.GetAccessor().GetProperty<int>("PublicOverriddenProperty").Should().Be(1);
            childAsParent.GetAccessor().SetProperty("PublicOverriddenProperty", 2);
            childAsParent.PublicOverriddenProperty.Should().Be(2);
            ((MockChild)childAsParent).publicOverriddenPropertyValue.Should().Be(2);

            childAsParent.privatePropertyValue = 1;
            childAsParent.GetAccessor().GetProperty<int>("PrivateProperty").Should().Be(1);
            childAsParent.GetAccessor().SetProperty("PrivateProperty", 2);
            childAsParent.privatePropertyValue.Should().Be(2);

            ((MockChild)childAsParent).protectedOverriddenPropertyValueOfChild = 1;
            childAsParent.GetAccessor().GetProperty<int>("ProtectedOverriddenProperty").Should().Be(1);
            childAsParent.GetAccessor().SetProperty("ProtectedOverriddenProperty", 2);
            ((MockChild)childAsParent).protectedOverriddenPropertyValueOfChild.Should().Be(2);
            childAsParent.protectedOverriddenPropertyValueOfParent.Should().Be(0);

        }


        [Test]
        public void TestGetSetFieldValue()
        {
            var parent = new MockParent();
            var parentAccessor = TypeAccessorFactory.GetDynamicTypeAccessor(parent.GetType());

            new Action(() => parent.GetAccessor().GetField<String>("PublicField")).ShouldThrow<InvalidCastException>();
            new Action(() => parent.GetAccessor().SetField("PublicField", "test")).ShouldThrow<InvalidCastException>();

            new Action(() => parent.GetAccessor().GetField<String>("NonExistentField")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => parent.GetAccessor().SetField("NonExistentField", "test")).ShouldThrow<ArgumentOutOfRangeException>();

            new Action(() => parent.GetAccessor().GetField<String>(null)).ShouldThrow<ArgumentNullException>();
            new Action(() => parent.GetAccessor().SetField(null, "test")).ShouldThrow<ArgumentNullException>();

            parent.PublicField = 1;
            parent.GetAccessor().GetField<int>("PublicField").Should().Be(1);
            parent.GetAccessor().SetField("PublicField", 2);
            parent.PublicField.Should().Be(2);

            parent.GetAccessor().GetField<int>("PrivateField").Should().Be(0);
            parent.GetAccessor().SetField("PrivateField", 1);
            parent.GetAccessor().GetField<int>("PrivateField").Should().Be(1);

            parent.GetAccessor().GetField<int>("ProtectedField").Should().Be(0);
            parent.GetAccessor().SetField("ProtectedField", 1);
            parent.GetAccessor().GetField<int>("ProtectedField").Should().Be(1);

            var child = new MockChild();

            child.PublicField = 1;
            child.GetAccessor().GetField<int>("PublicField").Should().Be(1);
            child.GetAccessor().SetField("PublicField", 2);
            child.PublicField.Should().Be(2);

            child.GetAccessor().GetField<int>("PrivateField").Should().Be(0);
            child.GetAccessor().SetField("PrivateField", 1);
            child.GetAccessor().GetField<int>("PrivateField").Should().Be(1);
            parentAccessor.GetFieldValue<int>(child, "PrivateField").Should().Be(0);

            child.GetAccessor().GetField<int>("ProtectedField").Should().Be(0);
            child.GetAccessor().SetField("ProtectedField", 1);
            child.GetAccessor().GetField<int>("ProtectedField").Should().Be(1);

            MockParent childAsParent = new MockChild();

            childAsParent.PublicField = 1;
            childAsParent.GetAccessor().GetField<int>("PublicField").Should().Be(1);
            childAsParent.GetAccessor().SetField("PublicField", 2);
            child.PublicField.Should().Be(2);

            childAsParent.GetAccessor().GetField<int>("PrivateField").Should().Be(0);
            childAsParent.GetAccessor().SetField("PrivateField", 1);
            childAsParent.GetAccessor().GetField<int>("PrivateField").Should().Be(1);
            parentAccessor.GetFieldValue<int>(childAsParent, "PrivateField").Should().Be(0);

            childAsParent.GetAccessor().GetField<int>("ProtectedField").Should().Be(0);
            childAsParent.GetAccessor().SetField("ProtectedField", 1);
            childAsParent.GetAccessor().GetField<int>("ProtectedField").Should().Be(1);

        }
    }
}
