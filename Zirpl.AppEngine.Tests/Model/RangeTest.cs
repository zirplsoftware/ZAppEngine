using NUnit.Framework;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Tests.Model
{
    [TestFixture]
    [Category("Common")]
    public class RangeTest
    {
        [Test]
        public void TestIRangeOfIntsPropertiesTest()
        {
            IRange<int> range = new Range<int>(int.MinValue, int.MaxValue);
            Assert.AreEqual(int.MaxValue, range.Maximum);
            Assert.AreEqual(int.MinValue, range.Minimum);
            Assert.IsTrue(range.IsValid);

            range.Minimum = 2;
            range.Maximum = 1;
            Assert.IsFalse(range.IsValid);
        }

        [Test]
        public void TestIntRangeEquals()
        {
            Range<int> range1 = new Range<int>(0, 1);
            Range<int> range2 = new Range<int>(2, 3);

            Assert.IsFalse(range1.Equals(range2));

            range1.Minimum = 2;
            range1.Maximum = 3;

            Assert.IsTrue(range1.Equals(range2));
        }

        [Test]
        public void TestIntRangeCompareTo()
        {
            Range<int> range1 = new Range<int>(0, 1);
            Range<int> range2 = new Range<int>(2,3);

            Assert.AreEqual(-1, range1.CompareTo(range2));
            Assert.AreEqual(1, range2.CompareTo(range1));

            range1.Minimum = 2;
            range1.Maximum = 3;

            Assert.AreEqual(0, range1.CompareTo(range2));
            Assert.AreEqual(0, range2.CompareTo(range1));

            range1.Maximum = 4;
            Assert.AreEqual(1, range1.CompareTo(range2));
            Assert.AreEqual(-1, range2.CompareTo(range1));
        }

        [Test]
        [ExpectedException(typeof(InvalidRangeException))]
        public void TestCompareToWithInvalidIntRange1()
        {
            Range<int> range1 = new Range<int>(1, 0);
            Range<int> range2 = new Range<int>(2, 3);

            range1.CompareTo(range2);
        }

        [Test]
        [ExpectedException(typeof(InvalidRangeException))]
        public void TestCompareToWithInvalidIntRange2()
        {
            Range<int> range1 = new Range<int>(1, 0);
            Range<int> range2 = new Range<int>(2, 3);

            range2.CompareTo(range1);
        }


        [Test]
        public void TestEqualsWithInvalidIntRange1()
        {
            // equals method should not throw an exception if
            // invalid
            Range<int> range1 = new Range<int>(1, 0);
            Range<int> range2 = new Range<int>(0, 0);

            range1.Equals(range2);
        }

        [Test]
        public void TestEqualsWithInvalidIntRange2()
        {
            // equals method should not throw an exception if
            // invalid
            Range<int> range1 = new Range<int>(0, 0);
            Range<int> range2 = new Range<int>(1, 0);

            range1.Equals(range2);
        }

        [Test]
        public void TestDefaultConstructor()
        {
            Range<int> range = new Range<int>();
            Assert.AreEqual(0, range.Minimum);
            Assert.AreEqual(0, range.Maximum);
        }

        [Test]
        public void TestEqualsAndCompareToWithDifferentDataTypes()
        {
            Range<int> range1 = new Range<int>(1, 0);
            Range<float> range2 = new Range<float>(1, 0);

            Assert.IsFalse(range1.Equals(range2));
        }
    }
}
/* $Log: RangeTest.cs,v $
/* Revision 1.2  2006/04/16 17:00:30  nathan
/* added NumericalFieldMetadataTest
/* finished DateFieldMetadataTest
/* removed all overloaded constructors from fieldMetadata classes
/* changed Min and Max lengths to be nullable in StringFieldMetadata
/* added documentation to PowerClasses classes
/* added log entry to all classes without it
/* fixed bugs in NumericalFieldMetadata and DateTimeFieldMetadata
/*
/* Revision 1.1  2006/04/14 22:30:13  nathan
/* Added PowerClasses project
/* Adding unit tests
/*
/* Revision 1.1  2006/03/31 04:14:03  nathan
/* Adding to CVS for first time
/*
 */