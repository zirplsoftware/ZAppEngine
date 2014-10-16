using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Membership
{
    public partial class PasswordResetLinkEntityWrapper
    {
        public User User { get; set; }
    }

    public partial class PasswordResetLinkTestsStrategy
    {

        public virtual void SetUpWrapper(PasswordResetLinkEntityWrapper wrapper)
        {
            wrapper.User = this.Data.GenerateUser();
        }

        public virtual void CreateEntity(PasswordResetLinkEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreatePasswordResetLink(wrapper.User);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(PasswordResetLinkEntityWrapper entityWrapper, PasswordResetLink entity, PasswordResetLink entityFromDb)
        {
            entityFromDb.Expires.Should().Be(entity.Expires);
            entityFromDb.Token.Should().Be(entity.Token);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.User, o => o.UserId);
        }

        public virtual void OnAssertExpectationsAfterInsert(PasswordResetLinkEntityWrapper entityWrapper, PasswordResetLink entity, PasswordResetLink entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(PasswordResetLinkEntityWrapper entityWrapper, PasswordResetLink entity, PasswordResetLink entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(PasswordResetLinkEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(PasswordResetLinkEntityWrapper entityWrapper, PasswordResetLink entity)
        {
            entity.Token = Guid.NewGuid();
        }

        public virtual void ChangePropertyValuesToFailValidation(PasswordResetLinkEntityWrapper entityWrapper, PasswordResetLink entity)
        {
            entity.Expires = DateTime.Now.AddDays(3);
        }
    }
}
