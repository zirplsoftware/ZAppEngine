using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Settings
{
    public partial class TaxRuleEntityWrapper
    {
        public StateProvinceType StateProvinceType;
        //public StateProvince StateProvince2;
    }

    public partial class TaxRuleTestsStrategy
    {
        private bool nextIsEntity2;
        public virtual void SetUpWrapper(TaxRuleEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(TaxRuleEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateTaxRule(
                nextIsEntity2 
                ? IocUtils.DependencyResolver.Resolve<IStateProvinceTypeDataService>().Get((int)StateProvinceTypeEnum.UnitedStatesOfAmerica_Alabama)
                : IocUtils.DependencyResolver.Resolve<IStateProvinceTypeDataService>().Get((int)StateProvinceTypeEnum.UnitedStatesOfAmerica_Alaska));
            wrapper.StateProvinceType = wrapper.Entity.StateProvinceType;

            if (!nextIsEntity2)
            {
                nextIsEntity2 = true;
            }
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            TaxRuleEntityWrapper entityWrapper, TaxRule entity, TaxRule entityFromDb)
        {
            entityFromDb.Rate.Should().Be(entity.Rate);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.StateProvinceType, o => o.StateProvinceTypeId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            TaxRuleEntityWrapper entityWrapper, TaxRule entity, TaxRule entityFromDb)
        {
            entityFromDb.StateProvinceType.Id.Should().Be(entityWrapper.StateProvinceType.Id);
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            TaxRuleEntityWrapper entityWrapper, TaxRule entity, TaxRule entityFromDb)
        {
            //entityFromDb.StateProvince.Id.Should().Be(entityWrapper.StateProvince2.Id);
        }

        public virtual void OnAssertExpectationsAfterDelete(
            TaxRuleEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IStateProvinceTypeDataService>().Get(entityWrapper.StateProvinceType.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                //this.DataServices.StateProvinceDataService.Get(entityWrapper.StateProvince2.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(
            TaxRuleEntityWrapper entityWrapper, TaxRule entity)
        {
            entity.Rate += 2;
            //entity.StateProvince = this.Data.CreateStateProvince();
            //entityWrapper.StateProvince2 = entity.StateProvince;
            //this.EntitiesToDelete.Add(entityWrapper.StateProvince2);
        }

        public virtual void ChangePropertyValuesToFailValidation(TaxRuleEntityWrapper entityWrapper, TaxRule entity)
        {
            entity.Rate = entity.Rate*-1;
        }
    }
}
