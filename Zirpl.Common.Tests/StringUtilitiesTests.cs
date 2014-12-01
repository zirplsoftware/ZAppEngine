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
            "AbcAad".SubstringUntilFirstInstanceOf(".").Should().Be("AbcAad");
            "AbcAad".SubstringUntilFirstInstanceOf("A").Should().Be("");
            "AbcAad".SubstringUntilFirstInstanceOf("a").Should().Be("AbcA");
            "AbcAad".SubstringUntilFirstInstanceOf("B").Should().Be("AbcAad");
            "AbcAad".SubstringUntilFirstInstanceOf("b").Should().Be("A");
            "AbcAad".SubstringUntilFirstInstanceOf("d").Should().Be("AbcAa");

            "AbcAad".SubstringUntilFirstInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringUntilFirstInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringUntilFirstInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringUntilFirstInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringUntilFirstInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringUntilFirstInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
        }
        [Test]
        public void TestSubstringThroughFirstInstanceOf()
        {
            "AbcAad".SubstringThroughFirstInstanceOf(".").Should().Be("AbcAad");
            "AbcAad".SubstringThroughFirstInstanceOf("A").Should().Be("A");
            "AbcAad".SubstringThroughFirstInstanceOf("a").Should().Be("AbcAa");
            "AbcAad".SubstringThroughFirstInstanceOf("B").Should().Be("AbcAad");
            "AbcAad".SubstringThroughFirstInstanceOf("b").Should().Be("Ab");
            "AbcAad".SubstringThroughFirstInstanceOf("d").Should().Be("AbcAad");

            "AbcAad".SubstringThroughFirstInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringThroughFirstInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringThroughFirstInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringThroughFirstInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("Ab");
            "AbcAad".SubstringThroughFirstInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("Ab");
            "AbcAad".SubstringThroughFirstInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
        }
        [Test]
        public void TestSubstringFromFirstInstanceOf()
        {
            "AbcAad".SubstringFromFirstInstanceOf(".").Should().Be("");
            "AbcAad".SubstringFromFirstInstanceOf("A").Should().Be("AbcAad");
            "AbcAad".SubstringFromFirstInstanceOf("a").Should().Be("ad");
            "AbcAad".SubstringFromFirstInstanceOf("B").Should().Be("");
            "AbcAad".SubstringFromFirstInstanceOf("b").Should().Be("bcAad");
            "AbcAad".SubstringFromFirstInstanceOf("d").Should().Be("d");

            "AbcAad".SubstringFromFirstInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringFromFirstInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringFromFirstInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringFromFirstInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringFromFirstInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringFromFirstInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("d");
        }
        [Test]
        public void TestSubstringAfterFirstInstanceOf()
        {
            "AbcAad".SubstringAfterFirstInstanceOf(".").Should().Be("");
            "AbcAad".SubstringAfterFirstInstanceOf("A").Should().Be("bcAad");
            "AbcAad".SubstringAfterFirstInstanceOf("a").Should().Be("d");
            "AbcAad".SubstringAfterFirstInstanceOf("B").Should().Be("");
            "AbcAad".SubstringAfterFirstInstanceOf("b").Should().Be("cAad");
            "AbcAad".SubstringAfterFirstInstanceOf("d").Should().Be("");

            "AbcAad".SubstringAfterFirstInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringAfterFirstInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringAfterFirstInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringAfterFirstInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("cAad");
            "AbcAad".SubstringAfterFirstInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("cAad");
            "AbcAad".SubstringAfterFirstInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        }

        [Test]
        public void TestSubstringUntilLastInstanceOf()
        {
            "AbcAad".SubstringUntilLastInstanceOf(".").Should().Be("AbcAad");
            "AbcAad".SubstringUntilLastInstanceOf("A").Should().Be("Abc");
            "AbcAad".SubstringUntilLastInstanceOf("a").Should().Be("AbcA");
            "AbcAad".SubstringUntilLastInstanceOf("B").Should().Be("AbcAad");
            "AbcAad".SubstringUntilLastInstanceOf("b").Should().Be("A");
            "AbcAad".SubstringUntilLastInstanceOf("d").Should().Be("AbcAa");

            "AbcAad".SubstringUntilLastInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringUntilLastInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
            "AbcAad".SubstringUntilLastInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
            "AbcAad".SubstringUntilLastInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringUntilLastInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringUntilLastInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
        }
        [Test]
        public void TestSubstringThroughLastInstanceOf()
        {
            "AbcAad".SubstringThroughLastInstanceOf(".").Should().Be("AbcAad");
            "AbcAad".SubstringThroughLastInstanceOf("A").Should().Be("AbcA");
            "AbcAad".SubstringThroughLastInstanceOf("a").Should().Be("AbcAa");
            "AbcAad".SubstringThroughLastInstanceOf("B").Should().Be("AbcAad");
            "AbcAad".SubstringThroughLastInstanceOf("b").Should().Be("Ab");
            "AbcAad".SubstringThroughLastInstanceOf("d").Should().Be("AbcAad");

            "AbcAad".SubstringThroughLastInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringThroughLastInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
            "AbcAad".SubstringThroughLastInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
            "AbcAad".SubstringThroughLastInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("Ab");
            "AbcAad".SubstringThroughLastInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("Ab");
            "AbcAad".SubstringThroughLastInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
        }
        [Test]
        public void TestSubstringFromLastInstanceOf()
        {
            "AbcAad".SubstringFromLastInstanceOf(".").Should().Be("");
            "AbcAad".SubstringFromLastInstanceOf("A").Should().Be("Aad");
            "AbcAad".SubstringFromLastInstanceOf("a").Should().Be("ad");
            "AbcAad".SubstringFromLastInstanceOf("B").Should().Be("");
            "AbcAad".SubstringFromLastInstanceOf("b").Should().Be("bcAad");
            "AbcAad".SubstringFromLastInstanceOf("d").Should().Be("d");

            "AbcAad".SubstringFromLastInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringFromLastInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("ad");
            "AbcAad".SubstringFromLastInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("ad");
            "AbcAad".SubstringFromLastInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringFromLastInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringFromLastInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("d");
        }
        [Test]
        public void TestSubstringAfterLastInstanceOf()
        {
            "AbcAad".SubstringAfterLastInstanceOf(".").Should().Be("");
            "AbcAad".SubstringAfterLastInstanceOf("A").Should().Be("ad");
            "AbcAad".SubstringAfterLastInstanceOf("a").Should().Be("d");
            "AbcAad".SubstringAfterLastInstanceOf("B").Should().Be("");
            "AbcAad".SubstringAfterLastInstanceOf("b").Should().Be("cAad");
            "AbcAad".SubstringAfterLastInstanceOf("d").Should().Be("");

            "AbcAad".SubstringAfterLastInstanceOf(".", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringAfterLastInstanceOf("A", StringComparison.InvariantCultureIgnoreCase).Should().Be("d");
            "AbcAad".SubstringAfterLastInstanceOf("a", StringComparison.InvariantCultureIgnoreCase).Should().Be("d");
            "AbcAad".SubstringAfterLastInstanceOf("B", StringComparison.InvariantCultureIgnoreCase).Should().Be("cAad");
            "AbcAad".SubstringAfterLastInstanceOf("b", StringComparison.InvariantCultureIgnoreCase).Should().Be("cAad");
            "AbcAad".SubstringAfterLastInstanceOf("d", StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        }

        [Test]
        public void TestSubstringUntilNthInstanceOf()
        {
            "AbcAad".SubstringUntilNthInstanceOf(".", 1).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf(".", 2).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("A", 1).Should().Be("");
            "AbcAad".SubstringUntilNthInstanceOf("A", 2).Should().Be("Abc");
            "AbcAad".SubstringUntilNthInstanceOf("A", 3).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("a", 1).Should().Be("AbcA");
            "AbcAad".SubstringUntilNthInstanceOf("a", 2).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("a", 3).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("B", 1).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("B", 2).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("b", 1).Should().Be("A");
            "AbcAad".SubstringUntilNthInstanceOf("b", 2).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("d", 1).Should().Be("AbcAa");
            "AbcAad".SubstringUntilNthInstanceOf("d", 2).Should().Be("AbcAad");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 1).Should().Be("");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 2).Should().Be("A");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 3).Should().Be("AAAaaa");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 1).Should().Be("AAA");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 2).Should().Be("AAAa");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 3).Should().Be("AAAaaa");


            "AbcAad".SubstringUntilNthInstanceOf(".", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf(".", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("A", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringUntilNthInstanceOf("A", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("Abc");
            "AbcAad".SubstringUntilNthInstanceOf("A", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
            "AbcAad".SubstringUntilNthInstanceOf("a", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringUntilNthInstanceOf("a", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("Abc");
            "AbcAad".SubstringUntilNthInstanceOf("a", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
            "AbcAad".SubstringUntilNthInstanceOf("B", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringUntilNthInstanceOf("B", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("b", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringUntilNthInstanceOf("b", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringUntilNthInstanceOf("d", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
            "AbcAad".SubstringUntilNthInstanceOf("d", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AA");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAA");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAa");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringUntilNthInstanceOf("AA", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AA");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAA");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAa");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringUntilNthInstanceOf("aa", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
        }



        [Test]
        public void TestSubstringThroughNthInstanceOf()
        {
            "AbcAad".SubstringThroughNthInstanceOf(".", 1).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf(".", 2).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("A", 1).Should().Be("A");
            "AbcAad".SubstringThroughNthInstanceOf("A", 2).Should().Be("AbcA");
            "AbcAad".SubstringThroughNthInstanceOf("A", 3).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("a", 1).Should().Be("AbcAa");
            "AbcAad".SubstringThroughNthInstanceOf("a", 2).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("a", 3).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("B", 1).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("B", 2).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("b", 1).Should().Be("Ab");
            "AbcAad".SubstringThroughNthInstanceOf("b", 2).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("d", 1).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("d", 2).Should().Be("AbcAad");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 1).Should().Be("AA");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 2).Should().Be("AAA");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 3).Should().Be("AAAaaa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 1).Should().Be("AAAaa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 2).Should().Be("AAAaaa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 3).Should().Be("AAAaaa");


            "AbcAad".SubstringThroughNthInstanceOf(".", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf(".", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("A", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringThroughNthInstanceOf("A", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
            "AbcAad".SubstringThroughNthInstanceOf("A", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
            "AbcAad".SubstringThroughNthInstanceOf("a", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
            "AbcAad".SubstringThroughNthInstanceOf("a", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
            "AbcAad".SubstringThroughNthInstanceOf("a", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
            "AbcAad".SubstringThroughNthInstanceOf("B", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("Ab");
            "AbcAad".SubstringThroughNthInstanceOf("B", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("b", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("Ab");
            "AbcAad".SubstringThroughNthInstanceOf("b", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("d", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringThroughNthInstanceOf("d", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AA");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAA");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAa");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaa");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringThroughNthInstanceOf("AA", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AA");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAA");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringThroughNthInstanceOf("aa", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
        }


        [Test]
        public void TestSubstringFromNthInstanceOf()
        {
            "AbcAad".SubstringFromNthInstanceOf(".", 1).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf(".", 2).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("A", 1).Should().Be("AbcAad");
            "AbcAad".SubstringFromNthInstanceOf("A", 2).Should().Be("Aad");
            "AbcAad".SubstringFromNthInstanceOf("A", 3).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("a", 1).Should().Be("ad");
            "AbcAad".SubstringFromNthInstanceOf("a", 2).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("a", 3).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("B", 1).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("B", 2).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("b", 1).Should().Be("bcAad");
            "AbcAad".SubstringFromNthInstanceOf("b", 2).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("d", 1).Should().Be("d");
            "AbcAad".SubstringFromNthInstanceOf("d", 2).Should().Be("");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 1).Should().Be("AAAaaa");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 2).Should().Be("AAaaa");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 3).Should().Be("");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 1).Should().Be("aaa");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 2).Should().Be("aa");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 3).Should().Be("");


            "AbcAad".SubstringFromNthInstanceOf(".", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf(".", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("A", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringFromNthInstanceOf("A", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("Aad");
            "AbcAad".SubstringFromNthInstanceOf("A", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("ad");
            "AbcAad".SubstringFromNthInstanceOf("a", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
            "AbcAad".SubstringFromNthInstanceOf("a", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("Aad");
            "AbcAad".SubstringFromNthInstanceOf("a", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("ad");
            "AbcAad".SubstringFromNthInstanceOf("B", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringFromNthInstanceOf("B", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("b", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringFromNthInstanceOf("b", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringFromNthInstanceOf("d", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("d");
            "AbcAad".SubstringFromNthInstanceOf("d", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAaaa");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("Aaaa");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("aaa");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("aa");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringFromNthInstanceOf("AA", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAaaa");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("Aaaa");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("aaa");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("aa");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringFromNthInstanceOf("aa", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        }

        [Test]
        public void TestSubstringAfterNthInstanceOf()
        {
            "AbcAad".SubstringAfterNthInstanceOf(".", 1).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf(".", 2).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("A", 1).Should().Be("bcAad");
            "AbcAad".SubstringAfterNthInstanceOf("A", 2).Should().Be("ad");
            "AbcAad".SubstringAfterNthInstanceOf("A", 3).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("a", 1).Should().Be("d");
            "AbcAad".SubstringAfterNthInstanceOf("a", 2).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("a", 3).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("B", 1).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("B", 2).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("b", 1).Should().Be("cAad");
            "AbcAad".SubstringAfterNthInstanceOf("b", 2).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("d", 1).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("d", 2).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 1).Should().Be("Aaaa");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 2).Should().Be("aaa");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 3).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 1).Should().Be("a");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 2).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 3).Should().Be("");


            "AbcAad".SubstringAfterNthInstanceOf(".", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf(".", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("A", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringAfterNthInstanceOf("A", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("ad");
            "AbcAad".SubstringAfterNthInstanceOf("A", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("d");
            "AbcAad".SubstringAfterNthInstanceOf("a", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("bcAad");
            "AbcAad".SubstringAfterNthInstanceOf("a", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("ad");
            "AbcAad".SubstringAfterNthInstanceOf("a", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("d");
            "AbcAad".SubstringAfterNthInstanceOf("B", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("cAad");
            "AbcAad".SubstringAfterNthInstanceOf("B", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("b", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("cAad");
            "AbcAad".SubstringAfterNthInstanceOf("b", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("d", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AbcAad".SubstringAfterNthInstanceOf("d", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("Aaaa");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("aaa");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("aa");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("a");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("AA", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("Aaaa");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("aaa");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("aa");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("a");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
            "AAAaaa".SubstringAfterNthInstanceOf("aa", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        }






        //[Test]
        //public void TestSubstringUntilLastNthInstanceOf()
        //{
        //    "AbcAad".SubstringUntilLastNthInstanceOf(".", 1).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf(".", 2).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("A", 1).Should().Be("Abc");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("A", 2).Should().Be("");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("A", 3).Should().Be("");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("a", 1).Should().Be("AbcA");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("a", 2).Should().Be("");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("a", 3).Should().Be("");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("B", 1).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("B", 2).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("b", 1).Should().Be("A");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("b", 2).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("d", 1).Should().Be("AbcAa");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("d", 2).Should().Be("AbcAad");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 1).Should().Be("");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 2).Should().Be("A");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 3).Should().Be("AAAaaa");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 1).Should().Be("AAA");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 2).Should().Be("AAAa");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 3).Should().Be("AAAaaa");


        //    "AbcAad".SubstringUntilLastNthInstanceOf(".", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf(".", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("A", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("A", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("Abc");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("A", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("a", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("a", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("Abc");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("a", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcA");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("B", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("B", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("b", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("b", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("d", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAa");
        //    "AbcAad".SubstringUntilLastNthInstanceOf("d", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("AbcAad");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AA");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAA");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAa");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("AA", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 1, StringComparison.InvariantCultureIgnoreCase).Should().Be("");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 2, StringComparison.InvariantCultureIgnoreCase).Should().Be("A");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 3, StringComparison.InvariantCultureIgnoreCase).Should().Be("AA");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 4, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAA");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 5, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAa");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 6, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
        //    "AAAaaa".SubstringUntilLastNthInstanceOf("aa", 7, StringComparison.InvariantCultureIgnoreCase).Should().Be("AAAaaa");
        //}
    }
}

