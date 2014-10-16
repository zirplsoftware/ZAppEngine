using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class BrandEntityWrapper
    {
    }

    [TestFixture]
    public partial class BrandTestsStrategy 
    {
        public virtual void SetUpWrapper(BrandEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(BrandEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateBrand();
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(BrandEntityWrapper entityWrapper, Brand entity, Brand entityFromDb)
        {
            entityFromDb.Description.Should().Be(entity.Description);
            entityFromDb.Name.Should().Be(entity.Name);
            entityFromDb.SeoId.Should().Be(entity.SeoId);
        }

        public virtual void OnAssertExpectationsAfterInsert(BrandEntityWrapper entityWrapper, Brand entity, Brand entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(BrandEntityWrapper entityWrapper, Brand entity, Brand entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(BrandEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(BrandEntityWrapper entityWrapper, Brand entity)
        {
            entity.Name = Guid.NewGuid().ToString();
            entity.Description = Guid.NewGuid().ToString();
            entity.SeoId = Guid.NewGuid().ToString();
        }

        public virtual void ChangePropertyValuesToFailValidation(BrandEntityWrapper entityWrapper, Brand entity)
        {
            entity.Name = null;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
