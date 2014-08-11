using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class TierPriceEntityWrapper
    {
        public DisplayProduct DisplayProduct;
    }

    public partial class TierPriceTestsStrategy
    {
        public virtual void SetUpWrapper(TierPriceEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(TierPriceEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateTierPrice(this.Data.CreateDisplayProduct());
            wrapper.DisplayProduct = wrapper.Entity.DisplayProduct;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(TierPriceEntityWrapper entityWrapper, TierPrice entity, TierPrice entityFromDb)
        {
            entityFromDb.PriceEach.Should().Be(entity.PriceEach);
            entityFromDb.Quantity.Should().Be(entity.Quantity);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
        }

        public virtual void OnAssertExpectationsAfterInsert(TierPriceEntityWrapper entityWrapper, TierPrice entity, TierPrice entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(TierPriceEntityWrapper entityWrapper, TierPrice entity, TierPrice entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(TierPriceEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(TierPriceEntityWrapper entityWrapper, TierPrice entity)
        {
            entity.PriceEach += entity.PriceEach;
            entity.Quantity += entity.Quantity;
        }

        public virtual void ChangePropertyValuesToFailValidation(TierPriceEntityWrapper entityWrapper, TierPrice entity)
        {
            entity.PriceEach *= -1;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
