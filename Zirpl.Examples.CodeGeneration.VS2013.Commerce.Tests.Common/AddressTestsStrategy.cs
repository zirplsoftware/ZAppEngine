using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests
{
    public partial class AddressEntityWrapper
    {
        public UpdatableProperty<StateProvinceType> StateProvinceType = new UpdatableProperty<StateProvinceType>();
    }

    public partial class AddressTestsStrategy
    {
        public virtual void CreateEntity(AddressEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateAddress(this.Data.GetExistingStateProvince());
            wrapper.StateProvinceType.Original = wrapper.Entity.StateProvinceType;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            AddressEntityWrapper entityWrapper, Address entity, Address entityFromDb)
        {
            entityFromDb.FirstName.Should().Be(entity.FirstName);
            entityFromDb.LastName.Should().Be(entity.LastName);
            entityFromDb.CompanyName.Should().Be(entity.CompanyName);
            entityFromDb.StreetLine1.Should().Be(entity.StreetLine1);
            entityFromDb.StreetLine2.Should().Be(entity.StreetLine2);
            entityFromDb.City.Should().Be(entity.City);
            entityFromDb.PostalCode.Should().Be(entity.PostalCode);
            entityFromDb.PhoneNumber.Should().Be(entity.PhoneNumber);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.StateProvinceType, o => o.StateProvinceTypeId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            AddressEntityWrapper entityWrapper, Address entity, Address entityFromDb)
        {
            entityFromDb.StateProvinceType.Id.Should().Be(entityWrapper.StateProvinceType.Original.Id);
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            AddressEntityWrapper entityWrapper, Address entity, Address entityFromDb)
        {
            entityFromDb.StateProvinceType.Id.Should().Be(entityWrapper.StateProvinceType.Updated.Id);
        }

        public virtual void OnAssertExpectationsAfterDelete(
            AddressEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IStateProvinceTypeDataService>().Get(entityWrapper.StateProvinceType.Original.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<IStateProvinceTypeDataService>().Get(entityWrapper.StateProvinceType.Updated.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(
            AddressEntityWrapper entityWrapper, Address entity)
        {
            entity.FirstName = Guid.NewGuid().ToString();
            entity.LastName = Guid.NewGuid().ToString();
            entity.CompanyName = Guid.NewGuid().ToString();
            entity.StreetLine1 = Guid.NewGuid().ToString();
            entity.StreetLine2 = Guid.NewGuid().ToString();
            entity.City = Guid.NewGuid().ToString();
            entity.PostalCode = "22345";
            entity.PhoneNumber = "555-852-3312";

            entity.StateProvinceType = this.Data.GetExistingStateProvince();
            entityWrapper.StateProvinceType.Updated = entity.StateProvinceType;
        }

        public virtual void ChangePropertyValuesToFailValidation(AddressEntityWrapper entityWrapper, Address entity)
        {
            entity.City = this.CreateStringLongerThan(AddressMetadataConstants.City_MaxLength);
        }

        public virtual void SetUpWrapper(AddressEntityWrapper wrapper)
        {
        }
    }
}
