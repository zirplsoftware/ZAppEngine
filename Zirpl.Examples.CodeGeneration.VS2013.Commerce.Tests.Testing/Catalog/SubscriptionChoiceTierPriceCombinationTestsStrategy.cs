using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class SubscriptionChoiceTierPriceCombinationEntityWrapper
    {
        public DisplayProduct DisplayProduct;
        public SubscriptionChoice SubscriptionChoice;
        public TierPrice TierPrice;
    }

    public partial class SubscriptionChoiceTierPriceCombinationTestsStrategy
    {
        public virtual void SetUpWrapper(SubscriptionChoiceTierPriceCombinationEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(SubscriptionChoiceTierPriceCombinationEntityWrapper wrapper)
        {
            wrapper.DisplayProduct = this.Data.CreateDisplayProduct();
            wrapper.SubscriptionChoice = this.Data.CreateSubscriptionChoice(wrapper.DisplayProduct, this.Data.CreateSubscriptionPeriod());
            wrapper.TierPrice = this.Data.CreateTierPrice(wrapper.DisplayProduct);
            wrapper.Entity = this.Data.CreateSubscriptionChoiceTierPriceCombination(wrapper.DisplayProduct, wrapper.SubscriptionChoice, wrapper.TierPrice);
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(SubscriptionChoiceTierPriceCombinationEntityWrapper entityWrapper, SubscriptionChoiceTierPriceCombination entity, SubscriptionChoiceTierPriceCombination entityFromDb)
        {
            entityFromDb.PriceEach.Should().Be(entity.PriceEach);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.SubscriptionChoice, o => o.SubscriptionChoiceId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.TierPrice, o => o.TierPriceId);
            entityFromDb.DisplayProduct.Should().Be(entityWrapper.DisplayProduct);
            entityFromDb.SubscriptionChoice.Should().Be(entityWrapper.SubscriptionChoice);
            entityFromDb.TierPrice.Should().Be(entityWrapper.TierPrice);
        }

        public virtual void OnAssertExpectationsAfterInsert(SubscriptionChoiceTierPriceCombinationEntityWrapper entityWrapper, SubscriptionChoiceTierPriceCombination entity, SubscriptionChoiceTierPriceCombination entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(SubscriptionChoiceTierPriceCombinationEntityWrapper entityWrapper, SubscriptionChoiceTierPriceCombination entity, SubscriptionChoiceTierPriceCombination entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(SubscriptionChoiceTierPriceCombinationEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ISubscriptionChoiceDataService>().Get(entityWrapper.SubscriptionChoice.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<ITierPriceDataService>().Get(entityWrapper.TierPrice.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(SubscriptionChoiceTierPriceCombinationEntityWrapper entityWrapper, SubscriptionChoiceTierPriceCombination entity)
        {
            entity.PriceEach += entity.PriceEach;
        }

        public virtual void ChangePropertyValuesToFailValidation(SubscriptionChoiceTierPriceCombinationEntityWrapper entityWrapper, SubscriptionChoiceTierPriceCombination entity)
        {
            entity.PriceEach *= -1;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
