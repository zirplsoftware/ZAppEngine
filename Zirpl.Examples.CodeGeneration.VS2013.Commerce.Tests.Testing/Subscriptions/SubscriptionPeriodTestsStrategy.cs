using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Subscriptions
{
    public partial class SubscriptionPeriodEntityWrapper
    {
    }

    public partial class SubscriptionPeriodTestsStrategy
    {
        public virtual void SetUpWrapper(SubscriptionPeriodEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(SubscriptionPeriodEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateSubscriptionPeriod();
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            SubscriptionPeriodEntityWrapper entityWrapper, SubscriptionPeriod entity, SubscriptionPeriod entityFromDb)
        {
            entityFromDb.ChargePeriod.Should().Be(entity.ChargePeriod);
            entityFromDb.ShipmentPeriod.Should().Be(entity.ShipmentPeriod);

            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ChargePeriodType, o => o.ChargePeriodTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ShipmentPeriodType, o => o.ShipmentPeriodTypeId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            SubscriptionPeriodEntityWrapper entityWrapper, SubscriptionPeriod entity, SubscriptionPeriod entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            SubscriptionPeriodEntityWrapper entityWrapper, SubscriptionPeriod entity, SubscriptionPeriod entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(
            SubscriptionPeriodEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(
            SubscriptionPeriodEntityWrapper entityWrapper, SubscriptionPeriod entity)
        {
            entity.ChargePeriod *= 2;
            entity.ShipmentPeriod *= 2;
        }

        public virtual void ChangePropertyValuesToFailValidation(SubscriptionPeriodEntityWrapper entityWrapper, SubscriptionPeriod entity)
        {
            entity.ChargePeriod = 0;
        }
    }
}
