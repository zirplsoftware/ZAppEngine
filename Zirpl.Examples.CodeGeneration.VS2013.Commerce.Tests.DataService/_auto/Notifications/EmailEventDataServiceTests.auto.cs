		
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.AppEngine.Validation;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.DataService.Notifications
{	
	[TestFixture]
    public partial class EmailEventDataServiceTests : EmailEventDataServiceTestsBase
    {
	}

    [TestFixture]
    public abstract partial class EmailEventDataServiceTestsBase : EntityLayerTestFixtureBase<EmailEvent, int, EmailEventEntityWrapper, EmailEventTestsStrategy>
    {
		protected IEmailEventDataService DataService {get { return this.DependencyResolver.Resolve<IEmailEventDataService>(); } }
		protected DataServicesProvider DataServices {get { return this.DependencyResolver.Resolve<DataServicesProvider>(); } }
		
		protected override TSupports GetLayer<TSupports>()
        {
            return this.DependencyResolver.Resolve<IEmailEventDataService>() as TSupports;
        }		
        protected override AppEngine.DataService.EntityFramework.DbContextBase CreateNewDbContext()
        {
            return new CommerceDataContext();
        }

		// AUTOGENERATE CODE NOTE: the tests are overridden here so that they will be easily identified when run as relating to this domain type. 

		[Test]
        [ExpectedException(typeof(ValidationException))]
        public override void TestInsert_ValidationGetsCalled()
        {
			base.TestInsert_ValidationGetsCalled();
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public override void TestUpdate_ValidationGetsCalled()
        {
			base.TestUpdate_ValidationGetsCalled();
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public override void TestSave_Insert_ValidationGetsCalled()
        {
			base.TestSave_Insert_ValidationGetsCalled();
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public override void TestSave_Update_ValidationGetsCalled()
        {
			base.TestSave_Update_ValidationGetsCalled();
        }

        [Test]
        public override void TestDelete_ValidationDoesNotGetCalled()
        {
			base.TestDelete_ValidationDoesNotGetCalled();
        }

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
        public override void TestInsert()
        {
			base.TestInsert();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestInsert_Null()
        {
			base.TestInsert_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestInsert_Persisted()
        {
			base.TestInsert_Persisted();
        }

        [Test]
        public override void TestInsert_List()
        {
			base.TestInsert_List();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestInsert_List_Null()
        {
			base.TestInsert_List_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestInsert_List_Empty()
        {
			base.TestInsert_List_Empty();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestInsert_List_WithPersisted()
        {
			base.TestInsert_List_WithPersisted();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestInsert_List_WithNull()
        {
			base.TestInsert_List_WithNull();
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
        public override void TestUpdate()
        {
			base.TestUpdate();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestUpdate_Null()
        {
			base.TestUpdate_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestUpdate_NotPersisted()
        {
			base.TestUpdate_NotPersisted();
        }

        [Test]
        public override void TestUpdate_List()
        {
			base.TestUpdate_List();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestUpdate_List_Null()
        {
			base.TestUpdate_List_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestUpdate_List_Empty()
        {
			base.TestUpdate_List_Empty();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestUpdate_List_WithNonPersisted()
        {
			base.TestUpdate_List_WithNonPersisted();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestUpdate_List_WithNull()
        {
			base.TestUpdate_List_WithNull();
        }

        [Test]
        public override void TestDelete()
        {
			base.TestDelete();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestDelete_NotPersisted()
        {
			base.TestDelete_NotPersisted();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestDelete_Null()
        {
			base.TestDelete_Null();
        }

        [Test]
        public override void TestDelete_List()
        {
			base.TestDelete_List();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestDelete_List_Null()
        {
			base.TestDelete_List_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestDelete_List_Empty()
        {
			base.TestDelete_List_Empty();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestDelete_List_WithNonPersisted()
        {
			base.TestDelete_List_WithNonPersisted();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestDelete_List_WithNull()
        {
			base.TestDelete_List_WithNull();
        }

        [Test]
        public override void TestDelete_Search()
        {
			base.TestDelete_Search();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestDelete_Search_Null()
        {
			base.TestDelete_Search_Null();
        }

        [Test]
        public override void TestDeleteById()
        {
			base.TestDeleteById();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestDeleteById_NonPersisted()
        {
			base.TestDeleteById_NonPersisted();
        }

        [Test]
        public override void TestDeleteById_NonExistent()
        {
			base.TestDeleteById_NonExistent();
        }

        [Test]
        public override void TestDeleteById_List()
        {
			base.TestDeleteById_List();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestDeleteById_List_Null()
        {
			base.TestDeleteById_List_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestDeleteById_List_Empty()
        {
			base.TestDeleteById_List_Empty();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestDeleteById_List_WithNonPersisted()
        {
			base.TestDeleteById_List_WithNonPersisted();
        }

        [Test]
        public override void TestDeleteById_List_WithNonExistent()
        {
			base.TestDeleteById_List_WithNonExistent();
        }

        [Test]
        public override void TestSave_Insert()
        {
			base.TestSave_Insert();
        }

        [Test]
        public override void TestSave_List_Insert()
        {
			base.TestSave_List_Insert();
        }

        [Test]
        public override void TestSave_List_InsertAndUpdate()
        {
			base.TestSave_List_InsertAndUpdate();
        }

        [Test]
        public override void TestSave_Update()
        {
			base.TestSave_Update();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestSave_Null()
        {
			base.TestSave_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public override void TestSave_List_Null()
        {
			base.TestSave_List_Null();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestSave_List_Empty()
        {
			base.TestSave_List_Empty();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public override void TestSave_List_WithNull()
        {
			base.TestSave_List_WithNull();
        }

        [Test]
        public override void TestSave_List_Update()
        {
			base.TestSave_List_Update();
        }

        [Test]
        public override void TestCreate()
        {
			base.TestCreate();
        }

        [Test]
        public override void TestCreate_Generic()
        {
			base.TestCreate_Generic();
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
