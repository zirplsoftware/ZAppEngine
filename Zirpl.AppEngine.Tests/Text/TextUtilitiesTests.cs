using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Validation;

namespace Zirpl.AppEngine.Tests.Text
{
    [TestFixture]
    public class TextUtilitiesTests
    {
        [Test]
        public void TestIsValidUSPostalCode()
        {
            String none = null;
            none.IsValidUSPostalCode().Should().BeFalse();
            "".IsValidUSPostalCode().Should().BeFalse();
            "123".IsValidUSPostalCode().Should().BeFalse();
            "1".IsValidUSPostalCode().Should().BeFalse();
            "1234A".IsValidUSPostalCode().Should().BeFalse();
            "123451234".IsValidUSPostalCode().Should().BeFalse();
            "12345.1234".IsValidUSPostalCode().Should().BeFalse();
            "12345-123A".IsValidUSPostalCode().Should().BeFalse();
            "12345".IsValidUSPostalCode().Should().BeTrue();
            "12345-1234".IsValidUSPostalCode().Should().BeTrue();
        }
    }
}
