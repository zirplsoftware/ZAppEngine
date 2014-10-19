using System;
using FluentAssertions;
using NUnit.Framework;
using Telerik.JustMock;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Validation;

namespace Zirpl.AppEngine.Tests.Service
{

    [TestFixture]
    public class AbstractedSupportsImplServiceTests
    {
        public class PersistableEntityMock : EntityBase<int>
        {
        }

        private ICompleteDataService<PersistableEntityMock, int> completeDataServiceMock;
        private IDataService<PersistableEntityMock, int> notSupportedDataService; 
        private IValidationHelper validationHelperMock;
        private AbstractedSupportsImplService<PersistableEntityMock, int> service;
        
        [SetUp]
        public void SetUp()
        {
            completeDataServiceMock = Mock.Create<ICompleteDataService<PersistableEntityMock, int>>();
            notSupportedDataService = Mock.Create<IDataService<PersistableEntityMock, int>>();
            validationHelperMock = Mock.Create<IValidationHelper>();

            service = new AbstractedSupportsImplService<PersistableEntityMock, int>();
            service.DataService = completeDataServiceMock;
            service.ValidationHelper = validationHelperMock;
        }

        [Test]
        public void Create()
        {
            var entity = new PersistableEntityMock();
            Mock.Arrange(() => completeDataServiceMock.Create()).Returns(entity);

            service.Create().Should().BeSameAs(entity);
            Mock.Assert(() => validationHelperMock.AssertValid(), Occurs.Never());
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_NotSupported()
        {
            service.DataService = notSupportedDataService;
            service.Create();
        }

        [Test]
        public void Create_generic()
        {
            var entity = new PersistableEntityMock();
            Mock.Arrange(() => completeDataServiceMock.Create<PersistableEntityMock>()).Returns(entity);

            service.Create<PersistableEntityMock>().Should().BeSameAs(entity);
            Mock.Assert(() => validationHelperMock.AssertValid(), Occurs.Never());
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_generic_NotSupported()
        {
            service.DataService = notSupportedDataService;

            service.Create<PersistableEntityMock>();
        }

        [Test]
        public void Reload()
        {
            var entity = new PersistableEntityMock();
            entity.Id = 1;
            var called = false;
            Mock.Arrange(() => completeDataServiceMock.Reload(entity)).DoInstead(() => called = true);

            service.Reload(entity);
            called.Should().BeTrue();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Reload_Null()
        {
            service.Reload(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Reload_NotPersisted()
        {
            var entity = new PersistableEntityMock();
            service.Reload(entity);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Reload_NotSupported()
        {
            service.DataService = notSupportedDataService;

            var entity = new PersistableEntityMock();
            entity.Id = 1;
            service.Reload(entity);
        }
    }
}
