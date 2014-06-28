using FluentAssertions;
using NUnit.Framework;

namespace Zirpl.Common.Tests
{
    [TestFixture]
    public class NumberExtensionsTests
    {
        [Test]
        public void TestIsWholeNumber()
        {
            ((decimal)12.3).IsWholeNumber().Should().BeFalse();
            ((decimal) 12.000).IsWholeNumber().Should().BeTrue();
            ((decimal)12).IsWholeNumber().Should().BeTrue();
            ((decimal)0).IsWholeNumber().Should().BeTrue();
            ((decimal)-1).IsWholeNumber().Should().BeTrue();
            ((decimal)-0.25).IsWholeNumber().Should().BeFalse();
            ((decimal)0.25).IsWholeNumber().Should().BeFalse();
        }

        [Test]
        public void TestCountDecimalPlaces()
        {
            ((decimal)12.3).DecimalPlacesCount().Should().Be(1);
            ((decimal)12.30).DecimalPlacesCount().Should().Be(1);
            ((decimal)12.03).DecimalPlacesCount().Should().Be(2);
            ((decimal)120).DecimalPlacesCount().Should().Be(0);
            ((decimal)120.00).DecimalPlacesCount().Should().Be(0);
            ((decimal)0).DecimalPlacesCount().Should().Be(0);
            ((decimal)0.0).DecimalPlacesCount().Should().Be(0);
            (new decimal(2)).DecimalPlacesCount().Should().Be(0);
            (new decimal(2.0000)).DecimalPlacesCount().Should().Be(0);


            ((float)12.3).DecimalPlacesCount().Should().Be(1);
            ((float)12.30).DecimalPlacesCount().Should().Be(1);
            ((float)12.03).DecimalPlacesCount().Should().Be(2);
            ((float)120).DecimalPlacesCount().Should().Be(0);
            ((float)120.00).DecimalPlacesCount().Should().Be(0);
            ((float)0).DecimalPlacesCount().Should().Be(0);
            ((float)0.0).DecimalPlacesCount().Should().Be(0);


            ((double)12.3).DecimalPlacesCount().Should().Be(1);
            ((double)12.30).DecimalPlacesCount().Should().Be(1);
            ((double)12.03).DecimalPlacesCount().Should().Be(2);
            ((double)120).DecimalPlacesCount().Should().Be(0);
            ((double)120.00).DecimalPlacesCount().Should().Be(0);
            ((double)0).DecimalPlacesCount().Should().Be(0);
            ((double)0.0).DecimalPlacesCount().Should().Be(0);
        }

        [Test]
        public void TestRoundUpCurrency()
        {
            new decimal(12.110).RoundUpCurrency().Should().Be((decimal)12.11);
            new decimal(12.111).RoundUpCurrency().Should().Be((decimal)12.12);
            new decimal(12.114).RoundUpCurrency().Should().Be((decimal)12.12);
            new decimal(12.115).RoundUpCurrency().Should().Be((decimal)12.12);
            new decimal(12.116).RoundUpCurrency().Should().Be((decimal)12.12);
            new decimal(12.119).RoundUpCurrency().Should().Be((decimal)12.12);
        }

        [Test]
        public void TestRoundDownCurrency()
        {
            new decimal(12.110).RoundDownCurrency().Should().Be((decimal)12.11);
            new decimal(12.111).RoundDownCurrency().Should().Be((decimal)12.11);
            new decimal(12.114).RoundDownCurrency().Should().Be((decimal)12.11);
            new decimal(12.115).RoundDownCurrency().Should().Be((decimal)12.11);
            new decimal(12.116).RoundDownCurrency().Should().Be((decimal)12.11);
            new decimal(12.119).RoundDownCurrency().Should().Be((decimal)12.11);
        }

        [Test]
        public void TestToCents()
        {
            new decimal(12.110).ToCents().Should().Be(1211);
            new decimal(12.111).ToCents().Should().Be(1211);
            new decimal(12.114).ToCents().Should().Be(1211);
            new decimal(12.115).ToCents().Should().Be(1212);
            new decimal(12.116).ToCents().Should().Be(1212);
            new decimal(12.119).ToCents().Should().Be(1212);
            new decimal(0.0).ToCents().Should().Be(0);
            new decimal(12).ToCents().Should().Be(1200);
        }

        [Test]
        public void TestFromCentsToCurrency()
        {
            1211.FromCentsToCurrency().Should().Be((decimal)12.11);
            0.FromCentsToCurrency().Should().Be((decimal)0);
            1.FromCentsToCurrency().Should().Be((decimal)0.01);;
        }
    }
}
