using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Text;

namespace Zirpl.Common.Tests
{
    [TestFixture]
    public class StringUtilitiesTests
    {
        [Test]
        public void TestToEmailListString_Null()
        {
            List<String> emails = null;
            emails.ToEmailListString().Should().Be(String.Empty);
        }

        [Test]
        public void TestToEmailListString_One()
        {
            List<String> emails = new List<string>();
            emails.Add("test@zirpl.com");
            emails.ToEmailListString().Should().Be("test@zirpl.com");
        }

        [Test]
        public void TestToEmailListString_Many()
        {
            List<String> emails = new List<string>();
            emails.Add("test@zirpl.com");
            emails.Add("test2@zirpl.com");
            emails.ToEmailListString().Should().Be("test@zirpl.com, test2@zirpl.com");
        }

        [Test]
        public void TestToEmailListString_Many_SpecifySeparator()
        {
            List<String> emails = new List<string>();
            emails.Add("test@zirpl.com");
            emails.Add("test2@zirpl.com");
            emails.ToEmailListString("999 ").Should().Be("test@zirpl.com999 test2@zirpl.com");
        }

        [Test]
        public void TestToEmailList_Null()
        {
            String email = null;
            var list = email.ToEmailList();
            list.Should().NotBeNull();
            list.Should().BeEmpty();
        }

        [Test]
        public void TestToEmailList_One()
        {
            String email = "test@zirpl.com";
            var list = email.ToEmailList();
            list.Should().NotBeNull();
            list.Should().NotBeEmpty();
            list.Count().Should().Be(1);
            var returnedEmail = list.First();
            returnedEmail.Should().Be(email);
        }

        [Test]
        public void TestToEmailList_Many()
        {
            String email = "test@zirpl.com, test2@zirpl.com";
            var list = email.ToEmailList();
            list.Should().NotBeNull();
            list.Should().NotBeEmpty();
            list.Count().Should().Be(2);
            var returnedEmail1 = list.First();
            returnedEmail1.Should().Be("test@zirpl.com");
            var returnedEmail2 = list.Skip(1).First();
            returnedEmail2.Should().Be("test2@zirpl.com");
        }

        [Test]
        public void TestToEmailList_Many_SpecifySeparator()
        {
            String email = "test@zirpl.com999 test2@zirpl.com";
            var list = email.ToEmailList("999 ");
            list.Should().NotBeNull();
            list.Should().NotBeEmpty();
            list.Count().Should().Be(2);
            var returnedEmail1 = list.First();
            returnedEmail1.Should().Be("test@zirpl.com");
            var returnedEmail2 = list.Skip(1).First();
            returnedEmail2.Should().Be("test2@zirpl.com");
        }

        [Test]
        public void TestLastXSubstring()
        {
            String text = "abcdefgh";
            text.LastXSubstring(4).Should().Be("efgh");
            text.LastXSubstring(5).Should().Be("defgh");
            text.LastXSubstring(0).Should().Be("");
            text.LastXSubstring(8).Should().Be("abcdefgh");
            "".LastXSubstring(0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLastXSubstring_Null()
        {
            String text = null;
            text.LastXSubstring(0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestLastXSubstring_TooLong()
        {
            String text = "abc";
            text.LastXSubstring(4);
        }

        [Test]
        public void TestSubstringUntilFirstInstanceOf()
        {
            "abcad".SubstringUntilFirstInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringUntilFirstInstanceOf("a").Should().Be("");
            "abcad".SubstringUntilFirstInstanceOf("b").Should().Be("a");
            "abcad".SubstringUntilFirstInstanceOf("d").Should().Be("abca");
        }
        [Test]
        public void TestSubstringThroughFirstInstanceOf()
        {
            "abcad".SubstringThroughFirstInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringThroughFirstInstanceOf("a").Should().Be("a");
            "abcad".SubstringThroughFirstInstanceOf("b").Should().Be("ab");
            "abcad".SubstringThroughFirstInstanceOf("d").Should().Be("abcad");
        }
        [Test]
        public void TestSubstringFromFirstInstanceOf()
        {
            "abcad".SubstringFromFirstInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringFromFirstInstanceOf("a").Should().Be("abcad");
            "abcad".SubstringFromFirstInstanceOf("b").Should().Be("bcad");
            "abcad".SubstringFromFirstInstanceOf("d").Should().Be("d");
        }
        [Test]
        public void TestSubstringAfterFirstInstanceOf()
        {
            "abcad".SubstringAfterFirstInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringAfterFirstInstanceOf("a").Should().Be("bcad");
            "abcad".SubstringAfterFirstInstanceOf("b").Should().Be("cad");
            "abcad".SubstringAfterFirstInstanceOf("d").Should().Be("");
        }

        [Test]
        public void TestSubstringUntilLastInstanceOf()
        {
            "abcad".SubstringUntilLastInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringUntilLastInstanceOf("a").Should().Be("abc");
            "abcad".SubstringUntilLastInstanceOf("b").Should().Be("a");
            "abcad".SubstringUntilLastInstanceOf("d").Should().Be("abca");
        }
        [Test]
        public void TestSubstringThroughLastInstanceOf()
        {
            "abcad".SubstringThroughLastInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringThroughLastInstanceOf("a").Should().Be("abca");
            "abcad".SubstringThroughLastInstanceOf("b").Should().Be("ab");
            "abcad".SubstringThroughLastInstanceOf("d").Should().Be("abcad");
        }
        [Test]
        public void TestSubstringFromLastInstanceOf()
        {
            "abcad".SubstringFromLastInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringFromLastInstanceOf("a").Should().Be("ad");
            "abcad".SubstringFromLastInstanceOf("b").Should().Be("bcad");
            "abcad".SubstringFromLastInstanceOf("d").Should().Be("d");
        }
        [Test]
        public void TestSubstringAfterLastInstanceOf()
        {
            "abcad".SubstringAfterLastInstanceOf(".").Should().Be("abcad");
            "abcad".SubstringAfterLastInstanceOf("a").Should().Be("d");
            "abcad".SubstringAfterLastInstanceOf("b").Should().Be("cad");
            "abcad".SubstringAfterLastInstanceOf("d").Should().Be("");
        }
    }
}

