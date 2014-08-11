using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Membership
{
    public partial class VisitorEntityWrapper
    {
    }

    public partial class VisitorTestsStrategy
    {
        public virtual void SetUpWrapper(VisitorEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(VisitorEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateVisitor();
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(VisitorEntityWrapper entityWrapper, Visitor entity, Visitor entityFromDb)
        {
            entityFromDb.IsAbandoned.Should().Be(entity.IsAbandoned);
            entityFromDb.IsAnonymous.Should().Be(entity.IsAnonymous);
            entityFromDb.LastActivityDate.Should().Be(entity.LastActivityDate);
            entityFromDb.Token.Should().Be(entity.Token);

            entityFromDb.AssertNavigationPropertyIsNull(entity, o => o.User, o => o.UserId, null);
        }

        public virtual void OnAssertExpectationsAfterInsert(VisitorEntityWrapper entityWrapper, Visitor entity, Visitor entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(VisitorEntityWrapper entityWrapper, Visitor entity, Visitor entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(VisitorEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(VisitorEntityWrapper entityWrapper, Visitor entity)
        {
            entity.IsAbandoned = true;
            entity.IsAnonymous = true;
        }

        public virtual void ChangePropertyValuesToFailValidation(VisitorEntityWrapper entityWrapper, Visitor entity)
        {
            entity.Token = Guid.Empty;
        }
    }
}
