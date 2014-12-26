using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Enums;

namespace Zirpl.Common.Tests.Enums
{
    [TestFixture]
    public class EnumExtensionMethodsTests
    {
        [FlagsAttribute]
        public enum Flags : int
        {
            None = 0,
            A = 2,
            B = 4,
            C = 8,
            All = 14
        }

        [Test]
        public void  TestContainsFlag()
        {
            var test = Flags.None;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeFalse();
            test.ContainsFlag(Flags.B).Should().BeFalse();
            test.ContainsFlag(Flags.C).Should().BeFalse();

            test = Flags.A;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeTrue();
            test.ContainsFlag(Flags.B).Should().BeFalse();
            test.ContainsFlag(Flags.C).Should().BeFalse();

            test = Flags.B;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeFalse();
            test.ContainsFlag(Flags.B).Should().BeTrue();
            test.ContainsFlag(Flags.C).Should().BeFalse();

            test = Flags.C;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeFalse();
            test.ContainsFlag(Flags.B).Should().BeFalse();
            test.ContainsFlag(Flags.C).Should().BeTrue();

            test = Flags.All;
            test.ContainsFlag(Flags.A).Should().BeTrue();
        }

        [Test]
        public void TestContainsFlag_MultipleOnLeft()
        {
            var test = Flags.None | Flags.None;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeFalse();
            test.ContainsFlag(Flags.B).Should().BeFalse();
            test.ContainsFlag(Flags.C).Should().BeFalse();

            test = Flags.None | Flags.A;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeTrue();
            test.ContainsFlag(Flags.B).Should().BeFalse();
            test.ContainsFlag(Flags.C).Should().BeFalse();

            test = Flags.A | Flags.B;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeTrue();
            test.ContainsFlag(Flags.B).Should().BeTrue();
            test.ContainsFlag(Flags.C).Should().BeFalse();

            test = Flags.C | Flags.C;
            test.ContainsFlag(Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A).Should().BeFalse();
            test.ContainsFlag(Flags.B).Should().BeFalse();
            test.ContainsFlag(Flags.C).Should().BeTrue();

            test = Flags.None | Flags.A | Flags.B | Flags.C;
            test.ContainsFlag(Flags.All).Should().BeTrue();
        }

        [Test]
        public void TestContainsFlag_MultipleOnRight()
        {
            var test = Flags.None;
            test.ContainsFlag(Flags.None | Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.None | Flags.A).Should().BeFalse();
            test.ContainsFlag(Flags.A | Flags.B).Should().BeFalse();

            test = Flags.A;
            test.ContainsFlag(Flags.None | Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A | Flags.A).Should().BeTrue();
            test.ContainsFlag(Flags.A | Flags.B).Should().BeFalse();


            test = Flags.All;
            test.ContainsFlag(Flags.A | Flags.B).Should().BeTrue();
        }

        [Test]
        public void TestContainsFlag_MultipleOnRightAndLeft()
        {
            var test = Flags.None | Flags.None;
            test.ContainsFlag(Flags.None | Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.None | Flags.A).Should().BeFalse();
            test.ContainsFlag(Flags.A | Flags.B).Should().BeFalse();

            test = Flags.None | Flags.A;
            test.ContainsFlag(Flags.None | Flags.None).Should().BeTrue();
            test.ContainsFlag(Flags.A | Flags.A).Should().BeTrue();
            test.ContainsFlag(Flags.None | Flags.A).Should().BeTrue();
            test.ContainsFlag(Flags.A | Flags.B).Should().BeFalse();

            test = Flags.A | Flags.B;
            test.ContainsFlag(Flags.A | Flags.A).Should().BeTrue();
            test.ContainsFlag(Flags.A | Flags.B).Should().BeTrue();
            test.ContainsFlag(Flags.A | Flags.C).Should().BeFalse();

            test.ContainsFlag(Flags.A | Flags.B | Flags.C).Should().BeFalse();

            test = Flags.A | Flags.B | Flags.C;
            test.ContainsFlag(Flags.A | Flags.C).Should().BeTrue();
        }

        [Test]
        public void TestAddFlag()
        {
            (Flags.None).AddFlag(Flags.None).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.None).AddFlag(Flags.A).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.None).AddFlag(Flags.A).ContainsFlag(Flags.A).Should().BeTrue();
            (Flags.None).AddFlag(Flags.A).ContainsFlag(Flags.B).Should().BeFalse();

            (Flags.A).AddFlag(Flags.None).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.A).AddFlag(Flags.A).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.A).AddFlag(Flags.A).ContainsFlag(Flags.A).Should().BeTrue();
            (Flags.A).AddFlag(Flags.A).ContainsFlag(Flags.B).Should().BeFalse();

            (Flags.A).AddFlag(Flags.None).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.A).AddFlag(Flags.B).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.A).AddFlag(Flags.B).ContainsFlag(Flags.A).Should().BeTrue();
            (Flags.A).AddFlag(Flags.B).ContainsFlag(Flags.B).Should().BeTrue();
            (Flags.A).AddFlag(Flags.B).ContainsFlag(Flags.A | Flags.B).Should().BeTrue();
            (Flags.A).AddFlag(Flags.B).ContainsFlag(Flags.A | Flags.C).Should().BeFalse();
        }

        [Test]
        public void TestRemoveFlag()
        {
            (Flags.None).RemoveFlag(Flags.None).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.None).RemoveFlag(Flags.B).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.None).RemoveFlag(Flags.B).ContainsFlag(Flags.B).Should().BeFalse();

            (Flags.A).RemoveFlag(Flags.None).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.A).RemoveFlag(Flags.None).ContainsFlag(Flags.A).Should().BeTrue();
            (Flags.A).RemoveFlag(Flags.A).ContainsFlag(Flags.A).Should().BeFalse();
            (Flags.A).RemoveFlag(Flags.B).ContainsFlag(Flags.B).Should().BeFalse();
            (Flags.A).RemoveFlag(Flags.B).ContainsFlag(Flags.A).Should().BeTrue();

            (Flags.A | Flags.B).RemoveFlag(Flags.A).ContainsFlag(Flags.A).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A).ContainsFlag(Flags.B).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.C).ContainsFlag(Flags.A).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.C).ContainsFlag(Flags.A | Flags.B).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).ContainsFlag(Flags.A).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).ContainsFlag(Flags.B).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).ContainsFlag(Flags.C).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B | Flags.C).ContainsFlag(Flags.None).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B | Flags.C).ContainsFlag(Flags.A).Should().BeFalse();
        }
    }
}
