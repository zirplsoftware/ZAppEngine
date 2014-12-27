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
    public class FieldQueryTests
    {
        public class A { }
        public class B : A { }
        public class C : B { }
        public class Mock
        {
            public B publicField;
            private B privateField;
            protected B protectedField;
            internal B internalField;
            protected internal B protectedInternalField;

            public static B publicStaticField;
            private static B privateStaticField;
            protected static B protectedStaticField;
            internal static B internalStaticField;
            protected internal static B protectedInternalStaticField;

            public static readonly int readonlyStaticInt;
            public readonly int readonlyInt;
        }
        public class MockDerived : Mock
        {
            
        }

        [Test]
        public void TestAssumptions()
        {
            // instance fields directly on type, no flags
            typeof(Mock).GetField("publicField").Should().NotBeNull();
            typeof(Mock).GetField("privateField").Should().BeNull();
            typeof(Mock).GetField("protectedField").Should().BeNull();
            typeof(Mock).GetField("internalField").Should().BeNull();
            typeof(Mock).GetField("protectedInternalField").Should().BeNull();

            // static fields directly on type, no flags
            typeof(Mock).GetField("publicStaticField").Should().NotBeNull();
            typeof(Mock).GetField("privateStaticField").Should().BeNull();
            typeof(Mock).GetField("protectedStaticField").Should().BeNull();
            typeof(Mock).GetField("internalStaticField").Should().BeNull();
            typeof(Mock).GetField("protectedInternalStaticField").Should().BeNull();

            // you can't have just the instance flag right?
            typeof(Mock).GetField("publicField", BindingFlags.Instance).Should().BeNull();
            // you can't have just the static flag right?
            typeof(Mock).GetField("publicStaticField", BindingFlags.Static).Should().BeNull();
            // you can't have just the public flag right?
            typeof(Mock).GetField("publicField", BindingFlags.Public).Should().BeNull();
            // you can't have just the nonpublic flag right?
            typeof(Mock).GetField("publicField", BindingFlags.NonPublic).Should().BeNull();

            // everything directly on the class is returned with the correct flag right?

            // instance fields directly on type, no flags
            typeof(Mock).GetField("publicField", BindingFlags.Public | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetField("privateField", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetField("protectedField", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetField("internalField", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(Mock).GetField("protectedInternalField", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();

            // static fields directly on type, no flags
            typeof(Mock).GetField("publicStaticField", BindingFlags.Public | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetField("privateStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetField("protectedStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetField("internalStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();
            typeof(Mock).GetField("protectedInternalStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().NotBeNull();

            // we can set readonly fields, right?
            var mock = new Mock();
            typeof(Mock).GetField("readonlyInt").SetValue(mock, 1);
            mock.readonlyInt.Should().Be(1);
            typeof(Mock).GetField("readonlyStaticInt").SetValue(null, 1);
            Mock.readonlyStaticInt.Should().Be(1);

            // now we're testing how derived members are handled so we always use the correct flags

            // public instance fields in the base class are returned with correct binding flags
            // non-private fields in the base class are also returned with correct binding flags
            // private fields in the base class are NEVER returned
            //
            typeof(MockDerived).GetField("publicField", BindingFlags.Public | BindingFlags.Instance).Should().NotBeNull();
            typeof(MockDerived).GetField("privateField", BindingFlags.NonPublic | BindingFlags.Instance).Should().BeNull();
            typeof(MockDerived).GetField("protectedField", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(MockDerived).GetField("internalField", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();
            typeof(MockDerived).GetField("protectedInternalField", BindingFlags.NonPublic | BindingFlags.Instance).Should().NotBeNull();

            // static fields in the base class are never returned...
            typeof(MockDerived).GetField("publicStaticField", BindingFlags.Public | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetField("privateStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetField("protectedStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetField("internalStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            typeof(MockDerived).GetField("protectedInternalStaticField", BindingFlags.NonPublic | BindingFlags.Static).Should().BeNull();
            // except with the flatten heirarchy
            // but even then, private fields are not returned
            typeof(MockDerived).GetField("publicStaticField", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();
            typeof(MockDerived).GetField("privateStaticField", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().BeNull();
            typeof(MockDerived).GetField("protectedStaticField", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();
            typeof(MockDerived).GetField("internalStaticField", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();
            typeof(MockDerived).GetField("protectedInternalStaticField", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy).Should().NotBeNull();

            // other randoms
            typeof(Mock).GetField("publicStaticField", BindingFlags.Public | BindingFlags.Instance).Should().BeNull();

            typeof (Mock).Fluent().Methods.OfAccessibility().Protected().All().OfScope().Instance().DeclaredOnBaseTypes().And().OfReturnType().Void();
        }

        //[Test]
        //public void Test_NonExistent()
        //{
        //    var fluentType = typeof(Mock).Fluent().Properties.;

        //    fluentType.Field("123").Exists.Should().BeFalse();
        //    fluentType.Field("123").IncludeNonPublic().Exists.Should().BeFalse();
        //    fluentType.Field("123").OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("123").OfType<A>().Exists.Should().BeFalse();
        //}

        //[Test]
        //public void Test_Default()
        //{
        //    var fluentType = typeof(Mock).Fluent();

        //    fluentType.Field("publicField").Exists.Should().BeTrue();
        //    fluentType.Field("privateField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedField").Exists.Should().BeFalse();
        //    fluentType.Field("internalField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalField").Exists.Should().BeFalse();

        //    fluentType.Field("publicStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("privateStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("internalStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalStaticField").Exists.Should().BeFalse();

        //    fluentType = typeof(MockDerived).Fluent();

        //    fluentType.Field("publicField").Exists.Should().BeTrue();
        //    fluentType.Field("privateField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedField").Exists.Should().BeFalse();
        //    fluentType.Field("internalField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalField").Exists.Should().BeFalse();

        //    fluentType.Field("publicStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("privateStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("internalStaticField").Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalStaticField").Exists.Should().BeFalse();
        //}

        //[Test]
        //public void TestIncludeNonPublic()
        //{
        //    var fluentType = typeof(Mock).Fluent();

        //    fluentType.Field("publicField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("privateField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("protectedField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("internalField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("protectedInternalField").IncludeNonPublic().Exists.Should().BeTrue();

        //    fluentType.Field("publicStaticField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("privateStaticField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("protectedStaticField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("internalStaticField").IncludeNonPublic().Exists.Should().BeTrue();
        //    fluentType.Field("protectedInternalStaticField").IncludeNonPublic().Exists.Should().BeTrue();
        //}

        //[Test]
        //public void TestOfType_ActualType()
        //{
        //    var fluentType = typeof(Mock).Fluent();

        //    fluentType.Field("publicField").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("privateField").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("protectedField").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("internalField").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("protectedInternalField").IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();

        //    fluentType.Field("publicStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("privateStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("protectedStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("internalStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();
        //    fluentType.Field("protectedInternalStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(B)).Exists.Should().BeTrue();

        //    fluentType.Field("publicField").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("privateField").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("protectedField").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("internalField").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("protectedInternalField").IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();

        //    fluentType.Field("publicStaticField").ExcludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("privateStaticField").ExcludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("protectedStaticField").ExcludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("internalStaticField").ExcludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //    fluentType.Field("protectedInternalStaticField").ExcludeStatic().IncludeNonPublic().OfType<B>().Exists.Should().BeTrue();
        //}

        //[Test]
        //public void TestOfType_BaseType()
        //{
        //    var fluentType = typeof(Mock).Fluent();

        //    fluentType.Field("publicField").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("privateField").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedField").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("internalField").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalField").IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();

        //    fluentType.Field("publicStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("privateStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("internalStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(A)).Exists.Should().BeFalse();

        //    fluentType.Field("publicField").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("privateField").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedField").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("internalField").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalField").IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();

        //    fluentType.Field("publicStaticField").ExcludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("privateStaticField").ExcludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedStaticField").ExcludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("internalStaticField").ExcludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalStaticField").ExcludeStatic().IncludeNonPublic().OfType<A>().Exists.Should().BeFalse();
        //}

        //[Test]
        //public void TestOfType_DerivedType()
        //{
        //    var fluentType = typeof(Mock).Fluent();

        //    fluentType.Field("publicField").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("privateField").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedField").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("internalField").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalField").IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();

        //    fluentType.Field("publicStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("privateStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("internalStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalStaticField").ExcludeStatic().IncludeNonPublic().OfType(typeof(C)).Exists.Should().BeFalse();

        //    fluentType.Field("publicField").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("privateField").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedField").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("internalField").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalField").IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();

        //    fluentType.Field("publicStaticField").ExcludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("privateStaticField").ExcludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedStaticField").ExcludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("internalStaticField").ExcludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //    fluentType.Field("protectedInternalStaticField").ExcludeStatic().IncludeNonPublic().OfType<C>().Exists.Should().BeFalse();
        //}
    }
}
