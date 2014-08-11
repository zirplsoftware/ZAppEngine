using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Settings
{
    public partial class SystemSettingEntityWrapper
    {
    }

    public partial class SystemSettingTestsStrategy
    {
        public virtual void SetUpWrapper(SystemSettingEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(SystemSettingEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateSystemSetting();
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(SystemSettingEntityWrapper entityWrapper, SystemSetting entity, SystemSetting entityFromDb)
        {
            entityFromDb.Value.Should().Be(entity.Value);
            entityFromDb.Name.Should().Be(entity.Name);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            SystemSettingEntityWrapper entityWrapper, SystemSetting entity, SystemSetting entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            SystemSettingEntityWrapper entityWrapper, SystemSetting entity, SystemSetting entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(
            SystemSettingEntityWrapper entityWrapper)
        {
        }

        public virtual void ChangePropertyValues(SystemSettingEntityWrapper entityWrapper, SystemSetting entity)
        {
            entityWrapper.Entity.Name = Guid.NewGuid().ToString();
            entityWrapper.Entity.Value = Guid.NewGuid().ToString();
        }

        public virtual void ChangePropertyValuesToFailValidation(SystemSettingEntityWrapper entityWrapper, SystemSetting entity)
        {
            entity.Name = null;
        }
    }
}
