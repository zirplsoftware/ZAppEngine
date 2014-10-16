//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FluentAssertions;
//using NUnit.Framework;
//using Zirpl.Linq;

//namespace Zirpl.Common.Tests.Linq
//{
//    [TestFixture]
//    public class QueryableWrapperTests
//    {
//        [Test]
//        public void TestUsingQueryableWrapperJustBeforeIteration()
//        {
//            var entities = new TestEntity[]
//            {
//                new TestEntity() {Name = "Nathan"}, 
//                new TestEntity() {Name = "Nathaniel"},
//                new TestEntity() {Name = "James"},
//                new TestEntity() {Name = "Mark"},
//                new TestEntity() {Name = "Greg"}
//            };

//            var count = 0;

//            var query = entities.AsQueryable().Where(o => o.Name.StartsWith("Nathan"));
//            var wrapper = new QueryableWrapper<TestEntity>(query, entity => count++);
//            foreach (var entity in wrapper)
//            {
                
//            }
//            count.Should().Be(2);
//        }

//        [Test]
//        public void TestAddingExpressionsToQueryableWrapper()
//        {
//            var entities = new TestEntity[]
//            {
//                new TestEntity() {Name = "Nathan"}, 
//                new TestEntity() {Name = "Nathaniel"},
//                new TestEntity() {Name = "James"},
//                new TestEntity() {Name = "Mark"},
//                new TestEntity() {Name = "Greg"}
//            };

//            var count = 0;

//            var query = entities.AsQueryable();
//            IQueryable<TestEntity> wrapper = new QueryableWrapper<TestEntity>(query, entity => count++);
//            query = wrapper.Where(o => o.Name.StartsWith("Nathan"));
//            foreach (var entity in query)
//            {

//            }
//            count.Should().Be(2);
//        }

//        public class TestEntity
//        {
//            public String Name { get; set; }
//        }
//    }
//}
