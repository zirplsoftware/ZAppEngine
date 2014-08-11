using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Notifications
{
    public partial class EmailEventEntityWrapper
    {
    }

    public partial class EmailEventTestsStrategy
    {
        public virtual void SetUpWrapper(EmailEventEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(EmailEventEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateEmailEvent();
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
        public virtual void OnAssertCommonPersistedEntityExpectations(EmailEventEntityWrapper entityWrapper, EmailEvent entity, EmailEvent entityFromDb)
        {
            entityFromDb.FromEmail.Should().Be(entity.FromEmail);
            entityFromDb.FromName.Should().Be(entity.FromName);
            entityFromDb.Bcc.Should().Be(entity.Bcc);
            entityFromDb.Body.Should().Be(entity.Body);
            entityFromDb.Cc.Should().Be(entity.Cc);
            entityFromDb.SentDate.Should().Be(entity.SentDate);
            entityFromDb.ResentDate.Should().Be(entity.ResentDate);
            entityFromDb.Subject.Should().Be(entity.Subject);
            entityFromDb.To.Should().Be(entity.To);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.EmailEventType, o => o.EmailEventTypeId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            EmailEventEntityWrapper entityWrapper, EmailEvent entity, EmailEvent entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            EmailEventEntityWrapper entityWrapper, EmailEvent entity, EmailEvent entityFromDb)
        {
            entity.ResentSucceeded.Should().BeTrue();
        }

        public virtual void OnAssertExpectationsAfterDelete(
            EmailEventEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(EmailEventEntityWrapper entityWrapper, EmailEvent entity)
        {
            entity.ResentDate = entity.SentDate.AddDays(100);
            entity.ResentSucceeded = true;
        }

        public virtual void ChangePropertyValuesToFailValidation(EmailEventEntityWrapper entityWrapper, EmailEvent entity)
        {
            entity.To = null;
        }
    }
}
