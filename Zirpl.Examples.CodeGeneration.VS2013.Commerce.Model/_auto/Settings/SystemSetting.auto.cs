using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings
{
    public partial class SystemSetting : AuditableBase<int>
    {
		public virtual string Value { get; set; }
    }
}

