		
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.AppEngine.Validation;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Service
{
	[TestFixture]
    public partial class NameSuffixTypeServiceTests : NameSuffixTypeDataServiceTestsBase
    {
	}

    [TestFixture]
    public abstract partial class NameSuffixTypeDataServiceTestsBase : DictionaryEntityLayerTestFixtureBase<NameSuffixType, byte, NameSuffixTypeEnum>
    {
		protected INameSuffixTypeService Service {get { return this.DependencyResolver.Resolve<INameSuffixTypeService>(); } }
		protected ServicesProvider Services {get { return this.DependencyResolver.Resolve<ServicesProvider>(); } }
		
		protected override TSupports GetLayer<TSupports>()
        {
            return this.DependencyResolver.Resolve<INameSuffixTypeService>() as TSupports;
        }		
        protected override AppEngine.DataService.EntityFramework.DbContextBase CreateNewDbContext()
        {
            return new CommerceDataContext();
        }
		// AUTOGENERATE CODE NOTE: the tests are overridden here so that they will be easily identified when run as relating to this domain type. 

        [Test]
        public override void TestGet()
        {
			base.TestGet();
        }

		[Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestGet_NonPersistedId()
        {
			base.TestGet_NonPersistedId();
        }

        [Test]
        public override void TestGet_NonExistentId()
        {
			base.TestGet_NonExistentId();
        }

        [Test]
        public override void TestExists()
        {
			base.TestExists();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestExists_NonPersistedId()
        {
			base.TestExists_NonPersistedId();
        }

        [Test]
        public override void TestExists_NonExistentId()
        {
			base.TestExists_NonExistentId();
        }

        [Test]
        public override void TestGetAll()
        {
			base.TestGetAll();
        }

        [Test]
        public override void TestGetQueryable()
        {
			base.TestGetQueryable();
        }

        [Test]
        public override void TestGetTotalCount()
        {
			base.TestGetTotalCount();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestGetTotalCount_Null()
        {
			base.TestGetTotalCount_Null();
        }

        [Test]
        public override void TestSearch()
        {
			base.TestSearch();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestSearch_Null()
        {
			base.TestSearch_Null();
        }

        [Test]
        public override void TestSearchUnique()
        {
			base.TestSearchUnique();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestSearchUnique_Null()
        {
			base.TestSearchUnique_Null();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public override void TestSearchUnique_NotUnique()
        {
			base.TestSearchUnique_NotUnique();
        }
    }
}
