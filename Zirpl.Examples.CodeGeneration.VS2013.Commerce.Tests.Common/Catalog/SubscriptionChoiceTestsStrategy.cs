using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class SubscriptionChoiceEntityWrapper
    {
        public DisplayProduct DisplayProduct;
        public SubscriptionPeriod SubscriptionPeriod;
    }

    public partial class SubscriptionChoiceTestsStrategy
    {

        public virtual void SetUpWrapper(SubscriptionChoiceEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(SubscriptionChoiceEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateSubscriptionChoice(this.Data.CreateDisplayProduct(), this.Data.CreateSubscriptionPeriod());
            wrapper.DisplayProduct = wrapper.Entity.DisplayProduct;
            wrapper.SubscriptionPeriod = wrapper.Entity.SubscriptionPeriod;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(SubscriptionChoiceEntityWrapper entityWrapper, SubscriptionChoice entity, SubscriptionChoice entityFromDb)
        {
            entityFromDb.PriceEach.Should().Be(entity.PriceEach);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.SubscriptionPeriod, o => o.SubscriptionPeriodId);
            entityFromDb.DisplayProduct.Should().Be(entityWrapper.DisplayProduct);
            entityFromDb.SubscriptionPeriod.Should().Be(entityWrapper.SubscriptionPeriod);
        }

        public virtual void OnAssertExpectationsAfterInsert(SubscriptionChoiceEntityWrapper entityWrapper, SubscriptionChoice entity, SubscriptionChoice entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(SubscriptionChoiceEntityWrapper entityWrapper, SubscriptionChoice entity, SubscriptionChoice entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(SubscriptionChoiceEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ISubscriptionPeriodDataService>().Get(entityWrapper.SubscriptionPeriod.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(SubscriptionChoiceEntityWrapper entityWrapper, SubscriptionChoice entity)
        {
            entity.PriceEach += entity.PriceEach;
        }

        public virtual void ChangePropertyValuesToFailValidation(SubscriptionChoiceEntityWrapper entityWrapper, SubscriptionChoice entity)
        {
            entity.PriceEach *= -1;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
