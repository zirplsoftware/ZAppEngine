using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Zirpl.Common.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void TestFromNow()
        {
            new DateTime(1999).FromNow().TotalDays.Should().BeGreaterOrEqualTo(1000);
            new DateTime(3015).FromNow().TotalDays.Should().BeGreaterOrEqualTo(1000);
        }
    }
}
