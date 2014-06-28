using System;
using NUnit.Framework;
using Zirpl.Text.TextCrawling;

namespace Zirpl.Common.Tests.Text.TextCrawling
{
    [TestFixture]
    public class TextCrawlerTests
    {
        #region Constructors

        [Test]
        public void TestConstructor()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            Assert.AreEqual(TextPositionType.Start, crawler.CurrentPosition.PositionType);
            Assert.AreEqual("ABCD", crawler.Text);
        }
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructor_EmptyString()
        {
            TextCrawler crawler = new TextCrawler("");
        }
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructor_Null()
        {
            TextCrawler crawler = new TextCrawler(null);
        }

        #endregion

        #region Can methods

        [Test]
        public void TestCanJumpTo()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            
            // While at start
            Assert.IsTrue(crawler.CanJumpTo(0));
            Assert.IsTrue(crawler.CanJumpTo(3));
            Assert.IsFalse(crawler.CanJumpTo(-1));
            Assert.IsFalse(crawler.CanJumpTo(4));


            // While at 0
            crawler.JumpTo(0);
            Assert.IsTrue(crawler.CanJumpTo(0));
            Assert.IsTrue(crawler.CanJumpTo(3));
            Assert.IsFalse(crawler.CanJumpTo(-1));
            Assert.IsFalse(crawler.CanJumpTo(4));


            // While in middle
            crawler.JumpTo(2);
            Assert.IsTrue(crawler.CanJumpTo(0));
            Assert.IsTrue(crawler.CanJumpTo(3));
            Assert.IsFalse(crawler.CanJumpTo(-1));
            Assert.IsFalse(crawler.CanJumpTo(4));


            // While on last
            crawler.JumpTo(3);
            Assert.IsTrue(crawler.CanJumpTo(0));
            Assert.IsTrue(crawler.CanJumpTo(3));
            Assert.IsFalse(crawler.CanJumpTo(-1));
            Assert.IsFalse(crawler.CanJumpTo(4));

            // While on end
            crawler.ToEnd();
            Assert.IsTrue(crawler.CanJumpTo(0));
            Assert.IsTrue(crawler.CanJumpTo(3));
            Assert.IsFalse(crawler.CanJumpTo(-1));
            Assert.IsFalse(crawler.CanJumpTo(4));
        }

        [Test]
        public void TestCanForwardBy()
        {
            TextCrawler crawler = new TextCrawler("ABCD");


            // While at start
            Assert.IsFalse(crawler.CanForwardBy(0));
            Assert.IsTrue(crawler.CanForwardBy(1));
            Assert.IsTrue(crawler.CanForwardBy(2));
            Assert.IsTrue(crawler.CanForwardBy(3));
            Assert.IsTrue(crawler.CanForwardBy(4));
            Assert.IsFalse(crawler.CanForwardBy(5));


            // While at 0
            crawler.JumpTo(0);
            Assert.IsFalse(crawler.CanForwardBy(0));
            Assert.IsTrue(crawler.CanForwardBy(1));
            Assert.IsTrue(crawler.CanForwardBy(2));
            Assert.IsTrue(crawler.CanForwardBy(3));
            Assert.IsFalse(crawler.CanForwardBy(4));


            // While in middle
            crawler.JumpTo(2);
            Assert.IsFalse(crawler.CanForwardBy(0));
            Assert.IsTrue(crawler.CanForwardBy(1));
            Assert.IsFalse(crawler.CanForwardBy(2));


            // While on last
            crawler.JumpTo(3);
            Assert.IsFalse(crawler.CanForwardBy(0));
            Assert.IsFalse(crawler.CanForwardBy(1));

            // While on end
            crawler.ToEnd();
            Assert.IsFalse(crawler.CanForwardBy(0));
            Assert.IsFalse(crawler.CanForwardBy(1));
        }

        [Test]
        public void TestBackBy()
        {
            TextCrawler crawler = new TextCrawler("ABCD");


            // While at start
            Assert.IsFalse(crawler.CanBackBy(0));
            Assert.IsFalse(crawler.CanBackBy(1));


            // While at 0
            crawler.JumpTo(0);
            Assert.IsFalse(crawler.CanBackBy(0));
            Assert.IsFalse(crawler.CanBackBy(1));


            // While in middle
            crawler.JumpTo(2);
            Assert.IsFalse(crawler.CanBackBy(0));
            Assert.IsTrue(crawler.CanBackBy(1));
            Assert.IsTrue(crawler.CanBackBy(2));
            Assert.IsFalse(crawler.CanBackBy(3));


            // While on last
            crawler.JumpTo(3);
            Assert.IsFalse(crawler.CanBackBy(0));
            Assert.IsTrue(crawler.CanBackBy(1));
            Assert.IsTrue(crawler.CanBackBy(2));
            Assert.IsTrue(crawler.CanBackBy(3));
            Assert.IsFalse(crawler.CanBackBy(4));

            // While on end
            crawler.ToEnd();
            Assert.IsFalse(crawler.CanBackBy(0));
            Assert.IsTrue(crawler.CanBackBy(1));
            Assert.IsTrue(crawler.CanBackBy(2));
            Assert.IsTrue(crawler.CanBackBy(3));
            Assert.IsTrue(crawler.CanBackBy(4));
            Assert.IsFalse(crawler.CanBackBy(5));
        }

        #endregion

        #region Jump methods
        [Test]
        public void TestJumpTo()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(0);
            Assert.AreEqual(0, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);


            crawler.JumpTo(1);
            Assert.AreEqual(1, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);


            crawler.JumpTo(3);
            Assert.AreEqual(3, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestJumpTo_Negative()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(-1);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestJumpTo_GreatThanLength()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.JumpTo(4);
        }

        [Test]
        public void TestJumpToStart()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.ToStart();
            Assert.IsNull(crawler.CurrentPosition.Position);
            Assert.AreEqual(TextPositionType.Start, crawler.CurrentPosition.PositionType);
            Assert.IsTrue(crawler.CurrentPosition.AtStart);
            Assert.IsFalse(crawler.CurrentPosition.AtEnd);
        }
        [Test]
        public void TestJumpToEnd()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.ToEnd();
            Assert.IsNull(crawler.CurrentPosition.Position);
            Assert.AreEqual(TextPositionType.End, crawler.CurrentPosition.PositionType);
            Assert.IsFalse(crawler.CurrentPosition.AtStart);
            Assert.IsTrue(crawler.CurrentPosition.AtEnd);
        }
        [Test]
        public void TestJumpToFirst()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.ToFirst();
            Assert.AreEqual(0, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
            Assert.IsFalse(crawler.CurrentPosition.AtStart);
            Assert.IsFalse(crawler.CurrentPosition.AtEnd);
        }
        [Test]
        public void TestJumpToLast()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.ToLast();
            Assert.AreEqual(3, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
            Assert.IsFalse(crawler.CurrentPosition.AtStart);
            Assert.IsFalse(crawler.CurrentPosition.AtEnd);
        }
        #endregion

        #region Forward By Methods

        [Test]
        public void TestForwardBy_FromStart()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.ForwardBy(1);
            Assert.AreEqual(0, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);

            crawler.ToStart();

            crawler.ForwardBy(2);
            Assert.AreEqual(1, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);

            crawler.ToStart();

            crawler.ForwardBy(4);
            Assert.AreEqual(3, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
        }
        [Test]
        public void TestForwardBy_FromFirst()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(0);

            crawler.ForwardBy(1);
            Assert.AreEqual(1, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
        }
        [Test]
        public void TestForwardBy_FromMiddle()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            
            crawler.JumpTo(2);

            crawler.ForwardBy(1);
            Assert.AreEqual(3, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestForwardBy_FromLast()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(3);

            crawler.ForwardBy(1);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestForwardBy_Negative()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.ForwardBy(-1);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestForwardBy_Zero()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.ForwardBy(0);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestForwardBy_TooMany_FromStart()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.ForwardBy(5);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestForwardBy_TooMany_FromMiddle()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.JumpTo(1);
            crawler.ForwardBy(3);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestForwardBy_TooMany_FromLast()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.ToLast();
            crawler.ForwardBy(1);
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestForwardBy_FromEnd()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.ToEnd();
            crawler.ForwardBy(1);
        }

        #endregion

        #region Back By Methods

        [Test]
        public void TestBackBy_FromEnd()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.ToEnd();

            crawler.BackBy(1);
            Assert.AreEqual(3, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);

            crawler.ToEnd();

            crawler.BackBy(2);
            Assert.AreEqual(2, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);

            crawler.ToEnd();

            crawler.BackBy(4);
            Assert.AreEqual(0, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
        }
        [Test]
        public void TestBackBy_FromLast()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(3);

            crawler.BackBy(1);
            Assert.AreEqual(2, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
        }
        [Test]
        public void TestBackBy_FromMiddle()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(2);

            crawler.BackBy(1);
            Assert.AreEqual(1, crawler.CurrentPosition.Position.Value);
            Assert.AreEqual(TextPositionType.Indexed, crawler.CurrentPosition.PositionType);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBackBy_FromFirst()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(0);

            crawler.BackBy(1);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBackBy_Negative()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(1);
            crawler.BackBy(-1);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBackBy_Zero()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.JumpTo(1);
            crawler.BackBy(0);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBackBy_TooMany_FromEnd()
        {
            TextCrawler crawler = new TextCrawler("ABCD");

            crawler.ToEnd();
            crawler.BackBy(5);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBackBy_TooMany_FromMiddle()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.JumpTo(2);
            crawler.BackBy(3);
        }
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestForwardBy_TooMany_FromFirst()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.ToFirst();
            crawler.BackBy(1);
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestBackBy_FromStart()
        {
            TextCrawler crawler = new TextCrawler("ABCD");
            crawler.BackBy(1);
        }

        #endregion

        //#region Back To Methods

        //[Test]
        //public void TestBackTo()
        //{
        //    TextCrawler2 crawler = new TextCrawler2("ABCD");
        //    crawler.ToLast();
        //    crawler.ToLast("BC");
        //    Assert.AreEqual(1, crawler.CurrentPosition.Position.Value);
        //}

        //#endregion
    }
}
