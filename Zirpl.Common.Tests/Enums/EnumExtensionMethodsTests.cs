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
        public void TestRemoveFlag()
        {
            (Flags.None).RemoveFlag(Flags.None).HasFlag(Flags.None).Should().BeTrue();
            (Flags.None).RemoveFlag(Flags.B).HasFlag(Flags.None).Should().BeTrue();
            (Flags.None).RemoveFlag(Flags.B).HasFlag(Flags.B).Should().BeFalse();

            (Flags.A).RemoveFlag(Flags.None).HasFlag(Flags.None).Should().BeTrue();
            (Flags.A).RemoveFlag(Flags.None).HasFlag(Flags.A).Should().BeTrue();
            (Flags.A).RemoveFlag(Flags.A).HasFlag(Flags.A).Should().BeFalse();
            (Flags.A).RemoveFlag(Flags.B).HasFlag(Flags.B).Should().BeFalse();
            (Flags.A).RemoveFlag(Flags.B).HasFlag(Flags.A).Should().BeTrue();

            (Flags.A | Flags.B).RemoveFlag(Flags.A).HasFlag(Flags.A).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A).HasFlag(Flags.B).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.C).HasFlag(Flags.A).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.C).HasFlag(Flags.A | Flags.B).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).HasFlag(Flags.A).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).HasFlag(Flags.B).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).HasFlag(Flags.C).Should().BeFalse();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B).HasFlag(Flags.None).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B | Flags.C).HasFlag(Flags.None).Should().BeTrue();
            (Flags.A | Flags.B).RemoveFlag(Flags.A | Flags.B | Flags.C).HasFlag(Flags.A).Should().BeFalse();
        }
    }
}
