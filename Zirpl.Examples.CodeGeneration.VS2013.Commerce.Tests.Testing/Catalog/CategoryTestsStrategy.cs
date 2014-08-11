using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class CategoryEntityWrapper
    {
    }

    [TestFixture]
    public partial class CategoryTestsStrategy 
    {
        public virtual void SetUpWrapper(CategoryEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(CategoryEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateCategory(this.Data.CreateCategory());
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(CategoryEntityWrapper entityWrapper, Category entity, Category entityFromDb)
        {
            entityFromDb.Description.Should().Be(entity.Description);
            entityFromDb.Name.Should().Be(entity.Name);
            entityFromDb.SeoId.Should().Be(entity.SeoId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Parent, o => o.ParentId);
        }

        public virtual void OnAssertExpectationsAfterInsert(CategoryEntityWrapper entityWrapper, Category entity, Category entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(CategoryEntityWrapper entityWrapper, Category entity, Category entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(CategoryEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(CategoryEntityWrapper entityWrapper, Category entity)
        {
            entity.Name = Guid.NewGuid().ToString();
            entity.Description = Guid.NewGuid().ToString();
            entity.SeoId = Guid.NewGuid().ToString();
        }

        public virtual void ChangePropertyValuesToFailValidation(CategoryEntityWrapper entityWrapper, Category entity)
        {
            entity.Name = null;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
