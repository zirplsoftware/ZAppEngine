using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Reflection.Fluent;

namespace Zirpl.Common.Tests.Reflection.Fluent
{
    [TestFixture]
    public class FluentPropertyTests
    {
        public class A { }
        public class B : A { }
        public class C : B { }
        public class Mock
        {
            public B publicProperty { get; set; }
            private B privateProperty { get; set; }
            protected B protectedProperty { get; set; }
            internal B internalProperty { get; set; }
            protected internal B protectedInternalProperty { get; set; }

            public static B publicStaticProperty { get; set; }
            private static B privateStaticProperty { get; set; }
            protected static B protectedStaticProperty { get; set; }
            internal static B internalStaticProperty { get; set; }
            protected internal static B protectedInternalStaticProperty { get; set; }

            //public static int readonlyStaticInt
            //{
            //    get { return 1; }
            //}

            //public int readonlyInt {
            //    get { return 1; }}
        }
        public class MockDerived : Mock
        {
            
        }

        [Test]
        public void TestAssumptions()
        {
            // instance fields directly on type, no flags
            typeof(Mock).GetProperty("publicProperty").Should().NotBeNull();
            typeof(Mock).GetProperty("privateProperty").Should().BeNull();
            typeof(Mock).GetProperty("protectedProperty").Should().BeNull();
            typeof(Mock).GetProperty("internalProperty").Should().BeNull();
            typeof(Mock).GetProperty("protectedInternalProperty").Should().BeNull();

            // static fields directly on type, no flags
            typeof(Mock).GetProperty("publicStaticProperty").Should().NotBeNull();
            typeof(Mock).GetProperty("privateStaticProperty").Should().BeNull();
            typeof(Mock).GetProperty("protectedStaticProperty").Should().BeNull();
            typeof(Mock).GetProperty("internalStaticProperty").Should().BeNull();
            typeof(Mock).GetProperty("protectedInternalStaticProperty").Should().BeNull();

            // you can't have just the instance flag right?
            typeof(Mock).GetProperty("publicProperty", BindingFlags.Instance).Should().BeNull();
            // you can't have just the static flag right?
            typeof(Mock).GetProperty("publicStaticProperty", BindingFlags.Static).Should().BeNull();
            // you can't have just the public flag right?
            typeof(Mock).GetProperty("publicProperty", BindingFlags.Public).Should().BeNull();
            // you can't have just the nonpublic flag right?
            typeof(Mock).GetProperty("publicProperty", BindingFlags.NonPublic).Should().BeNull();

            // everything directly on the class is returned with the correct flag right?

            // instance fields directly on type, no flags
            typeof(Mock).GetProperty("publicProperty", BindingFlags.Public | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetProperty("privateProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetProperty("protectedProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetProperty("internalProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetProperty("protectedInternalProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();

            // static fields directly on type, no flags
            typeof(Mock).GetProperty("publicStaticProperty", BindingFlags.Public | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetProperty("privateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetProperty("protectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetProperty("internalStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetProperty("protectedInternalStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();

            // now we're testing how derived members are handled so we always use the correct flags

            // public instance fields in the base class are returned with correct binding flags
            // non-private fields in the base class are also returned with correct binding flags
            // private fields in the base class are NEVER returned
            //
            typeof(MockDerived).GetProperty("publicProperty", BindingFlags.Public | BindingFlags.Instance).Should().NotBeNull();
            typeof(MockDerived).GetProperty("privateProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().BeNull();
            typeof(MockDerived).GetProperty("protectedProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(MockDerived).GetProperty("internalProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(MockDerived).GetProperty("protectedInternalProperty", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();

            // static fields in the base class are never returned...
            typeof(MockDerived).GetProperty("publicStaticProperty", BindingFlags.Public | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetProperty("privateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetProperty("protectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetProperty("internalStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetProperty("protectedInternalStaticProperty", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            // except with the flatten heirarchy
            // but even then, private fields are not returned
            typeof(MockDerived).GetProperty("publicStaticProperty", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();
            typeof(MockDerived).GetProperty("privateStaticProperty", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().BeNull();
            typeof(MockDerived).GetProperty("protectedStaticProperty", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();
            typeof(MockDerived).GetProperty("internalStaticProperty", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();
            typeof(MockDerived).GetProperty("protectedInternalStaticProperty", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();
        }
        [Test]
        public void Test_NonExistent()
        {
            var fluentType = typeof(Mock).Fluent();

            fluentType.Property("123").Exists.Should().BeFalse();
            fluentType.Property("123").IncludeNonPublic().Exists.Should().BeFalse();
            fluentType.Property("123").OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("123").OfType<A>().Exists.Should().BeFalse();
        }

        [Test]
        public void Test_Default()
        {
            var fluentType = typeof(Mock).Fluent();

            fluentType.Property("publicProperty").Exists.Should().BeTrue();
            fluentType.Property("privateProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedProperty").Exists.Should().BeFalse();
            fluentType.Property("internalProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedInternalProperty").Exists.Should().BeFalse();

            fluentType.Property("publicStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("privateStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("internalStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedInternalStaticProperty").Exists.Should().BeFalse();

            fluentType = typeof(MockDerived).Fluent();

            fluentType.Property("publicProperty").Exists.Should().BeTrue();
            fluentType.Property("privateProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedProperty").Exists.Should().BeFalse();
            fluentType.Property("internalProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedInternalProperty").Exists.Should().BeFalse();

            fluentType.Property("publicStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("privateStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("internalStaticProperty").Exists.Should().BeFalse();
            fluentType.Property("protectedInternalStaticProperty").Exists.Should().BeFalse();
        }

        [Test]
        public void TestIncludeNonPublic()
        {
            var fluentType = typeof(Mock).Fluent();

            fluentType.Property("publicProperty").IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("privateProperty").IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("protectedProperty").IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("internalProperty").IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().Exists.Should().BeTrue();

            fluentType.Property("publicStaticProperty").IncludeStatic().IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("privateStaticProperty").IncludeStatic().IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("protectedStaticProperty").IncludeStatic().IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("internalStaticProperty").IncludeStatic().IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("protectedInternalStaticProperty").IncludeStatic().IncludeNonPublic().Exists.Should().BeTrue();

            fluentType = typeof(MockDerived).Fluent();

            fluentType.Property("publicProperty").IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("privateProperty").IncludeNonPublic().Exists.Should().BeFalse();
            fluentType.Property("protectedProperty").IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("internalProperty").IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().Exists.Should().BeTrue();

            fluentType.Property("publicStaticProperty").IncludeStaticInBaseTypes().IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("privateStaticProperty").IncludeStaticInBaseTypes().IncludeNonPublic().Exists.Should().BeFalse();
            fluentType.Property("protectedStaticProperty").IncludeStaticInBaseTypes().IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("internalStaticProperty").IncludeStaticInBaseTypes().IncludeNonPublic().Exists.Should().BeTrue();
            fluentType.Property("protectedInternalStaticProperty").IncludeStaticInBaseTypes().IncludeNonPublic().Exists.Should().BeTrue();
        }

        [Test]
        public void TestOfType_ActualType()
        {
            var fluentType = typeof(Mock).Fluent();

            fluentType.Property("publicProperty").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("privateProperty").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("protectedProperty").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("internalProperty").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();

            fluentType.Property("publicStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("privateStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("protectedStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("internalStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
            fluentType.Property("protectedInternalStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();

            fluentType.Property("publicProperty").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("privateProperty").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("protectedProperty").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("internalProperty").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();

            fluentType.Property("publicStaticProperty").IncludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("privateStaticProperty").IncludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("protectedStaticProperty").IncludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("internalStaticProperty").IncludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
            fluentType.Property("protectedInternalStaticProperty").IncludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        }

        [Test]
        public void TestOfType_BaseType()
        {
            var fluentType = typeof(Mock).Fluent();

            fluentType.Property("publicProperty").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("privateProperty").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("protectedProperty").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("internalProperty").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();

            fluentType.Property("publicStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("privateStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("protectedStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("internalStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
            fluentType.Property("protectedInternalStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();

            fluentType.Property("publicProperty").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("privateProperty").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("protectedProperty").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("internalProperty").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();

            fluentType.Property("publicStaticProperty").IncludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("privateStaticProperty").IncludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("protectedStaticProperty").IncludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("internalStaticProperty").IncludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
            fluentType.Property("protectedInternalStaticProperty").IncludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        }

        [Test]
        public void TestOfType_DerivedType()
        {
            var fluentType = typeof(Mock).Fluent();

            fluentType.Property("publicProperty").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("privateProperty").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("protectedProperty").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("internalProperty").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();

            fluentType.Property("publicStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("privateStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("protectedStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("internalStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
            fluentType.Property("protectedInternalStaticProperty").IncludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();

            fluentType.Property("publicProperty").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("privateProperty").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("protectedProperty").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("internalProperty").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("protectedInternalProperty").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();

            fluentType.Property("publicStaticProperty").IncludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("privateStaticProperty").IncludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("protectedStaticProperty").IncludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("internalStaticProperty").IncludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
            fluentType.Property("protectedInternalStaticProperty").IncludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        }
    }
}
