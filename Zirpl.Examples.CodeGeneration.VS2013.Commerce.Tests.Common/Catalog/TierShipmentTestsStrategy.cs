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
    public partial class TierShipmentEntityWrapper
    {
        public DisplayProduct DisplayProduct;
    }

    public partial class TierShipmentTestsStrategy
    {
        public virtual void SetUpWrapper(TierShipmentEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(TierShipmentEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateTierShipment(this.Data.CreateDisplayProduct());
            wrapper.DisplayProduct = wrapper.Entity.DisplayProduct;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(TierShipmentEntityWrapper entityWrapper, TierShipment entity, TierShipment entityFromDb)
        {
            entityFromDb.BaseWeightInOunces.Should().Be(entity.BaseWeightInOunces);
            entityFromDb.RequiresManualShipmentHandling.Should().Be(entity.RequiresManualShipmentHandling);
            entityFromDb.Quantity.Should().Be(entity.Quantity);
            entityFromDb.WeightInOuncesEach.Should().Be(entity.WeightInOuncesEach);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
        }

        public virtual void OnAssertExpectationsAfterInsert(TierShipmentEntityWrapper entityWrapper, TierShipment entity, TierShipment entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(TierShipmentEntityWrapper entityWrapper, TierShipment entity, TierShipment entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(TierShipmentEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(TierShipmentEntityWrapper entityWrapper, TierShipment entity)
        {
            entity.Quantity += entity.Quantity;
        }

        public virtual void ChangePropertyValuesToFailValidation(TierShipmentEntityWrapper entityWrapper, TierShipment entity)
        {
            entity.Quantity = -1;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
