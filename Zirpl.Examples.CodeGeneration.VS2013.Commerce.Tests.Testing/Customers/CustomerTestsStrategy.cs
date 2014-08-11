using System;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Customers
{
    public partial class CustomerEntityWrapper : EntityWrapper<Customer>
    {
        public Visitor Visitor { get; set; }
        public UpdatableProperty<Address> MostRecentlyUsedShippingAddress = new UpdatableProperty<Address>();
    }

    public partial class CustomerTestsStrategy
    {

        public virtual void SetUpWrapper(CustomerEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(CustomerEntityWrapper wrapper)
        {
            wrapper.MostRecentlyUsedShippingAddress.Original = this.Data.CreateAddress(this.Data.GetExistingStateProvince());
            wrapper.Entity = this.Data.CreateCustomer(this.Data.CreatePromoCode(),
                visitor: wrapper.Visitor, shippingAddress: wrapper.MostRecentlyUsedShippingAddress.Original);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(CustomerEntityWrapper entityWrapper, Customer entity, Customer entityFromDb)
        {
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.CurrentShippingAddress, o => o.CurrentShippingAddressId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Visitor, o => o.VisitorId);
            entityFromDb.Visitor.Id.Should().Be(entityWrapper.Visitor.Id);
        }

        public virtual void OnAssertExpectationsAfterInsert(CustomerEntityWrapper entityWrapper, Customer entity, Customer entityFromDb)
        {
            entityFromDb.CurrentShippingAddress.Id.Should().Be(entityWrapper.MostRecentlyUsedShippingAddress.Original.Id);
        }

        public virtual void OnAssertExpectationsAfterUpdate(CustomerEntityWrapper entityWrapper, Customer entity, Customer entityFromDb)
        {
            entityFromDb.CurrentShippingAddress.Id.Should().Be(entityWrapper.MostRecentlyUsedShippingAddress.Updated.Id);
        }

        public virtual void OnAssertExpectationsAfterDelete(CustomerEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IVisitorDataService>().Get(entityWrapper.Visitor.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IAddressDataService>().Get(entityWrapper.MostRecentlyUsedShippingAddress.Original.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<IAddressDataService>().Get(entityWrapper.MostRecentlyUsedShippingAddress.Updated.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(CustomerEntityWrapper entityWrapper, Customer entity)
        {
            entity.CurrentShippingAddress = this.Data.CreateAddress(this.Data.GetExistingStateProvince());

            entityWrapper.MostRecentlyUsedShippingAddress.Updated = entity.CurrentShippingAddress;
        }

        public virtual void ChangePropertyValuesToFailValidation(CustomerEntityWrapper entityWrapper, Customer entity)
        {
            entity.CurrentShippingAddress.LastName = null;
        }
    }
}
