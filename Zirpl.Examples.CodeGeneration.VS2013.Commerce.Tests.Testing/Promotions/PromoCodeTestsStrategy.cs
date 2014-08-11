using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Promotions
{
    public partial class PromoCodeEntityWrapper
    {
    }

    public partial class PromoCodeTestsStrategy
    {
        public virtual void SetUpWrapper(PromoCodeEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(PromoCodeEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreatePromoCode();
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
        public virtual void OnAssertCommonPersistedEntityExpectations(
            PromoCodeEntityWrapper entityWrapper, PromoCode entity, PromoCode entityFromDb)
        {
            entityFromDb.Code.Should().Be(entity.Code);
            entityFromDb.Code.Should().Be(entity.Code);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            PromoCodeEntityWrapper entityWrapper, PromoCode entity, PromoCode entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            PromoCodeEntityWrapper entityWrapper, PromoCode entity, PromoCode entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(
            PromoCodeEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(PromoCodeEntityWrapper entityWrapper, PromoCode entity)
        {
            entityWrapper.Entity.Code = Guid.NewGuid().ToString();
        }

        public virtual void ChangePropertyValuesToFailValidation(PromoCodeEntityWrapper entityWrapper, PromoCode entity)
        {
            entity.Code = null;
        }
    }
}
