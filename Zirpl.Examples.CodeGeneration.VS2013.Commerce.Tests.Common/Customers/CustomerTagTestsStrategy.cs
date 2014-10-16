using System;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Customers
{
    public partial class CustomerTagEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Customer Customer { get; set; }
    }

    public partial class CustomerTagTestsStrategy
    {
        public virtual void SetUpWrapper(CustomerTagEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }
        public virtual void CreateEntity(CustomerTagEntityWrapper wrapper)
        {
            wrapper.Customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Entity = this.Data.CreateCustomerTag(wrapper.Customer, this.Data.CreateTag());
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            CustomerTagEntityWrapper entityWrapper, CustomerTag entity, CustomerTag entityFromDb)
        {
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Customer, o => o.CustomerId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Tag, o => o.TagId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            CustomerTagEntityWrapper entityWrapper, CustomerTag entity, CustomerTag entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            CustomerTagEntityWrapper entityWrapper, CustomerTag entity, CustomerTag entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(CustomerTagEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.Customer.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(CustomerTagEntityWrapper entityWrapper, CustomerTag entity)
        {
        }

        public virtual void ChangePropertyValuesToFailValidation(CustomerTagEntityWrapper entityWrapper, CustomerTag entity)
        {
            entity.Customer = null;
        }
    }
}
