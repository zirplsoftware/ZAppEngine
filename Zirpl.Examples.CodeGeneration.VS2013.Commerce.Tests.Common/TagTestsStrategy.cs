using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests
{
    public partial class TagEntityWrapper
    {
    }

    [TestFixture]
    public partial class TagTestsStrategy 
    {
        public virtual void SetUpWrapper(TagEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(TagEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateTag();
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(TagEntityWrapper entityWrapper, Tag entity, Tag entityFromDb)
        {
            entityFromDb.Description.Should().Be(entity.Description);
            entityFromDb.Name.Should().Be(entity.Name);
            entityFromDb.SeoId.Should().Be(entity.SeoId);
        }

        public virtual void OnAssertExpectationsAfterInsert(TagEntityWrapper entityWrapper, Tag entity, Tag entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(TagEntityWrapper entityWrapper, Tag entity, Tag entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(TagEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(TagEntityWrapper entityWrapper, Tag entity)
        {
            entity.Name = Guid.NewGuid().ToString();
            entity.Description = Guid.NewGuid().ToString();
            entity.SeoId = Guid.NewGuid().ToString();
        }

        public virtual void ChangePropertyValuesToFailValidation(TagEntityWrapper entityWrapper, Tag entity)
        {
            entity.Name = null;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
