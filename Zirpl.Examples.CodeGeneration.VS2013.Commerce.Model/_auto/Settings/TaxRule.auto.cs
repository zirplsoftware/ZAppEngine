using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings
{
    public partial class TaxRule : AuditableBase<int>
    {
		public virtual decimal Rate { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings.StateProvinceType StateProvinceType { get; set; }
		public virtual int StateProvinceTypeId { get; set; }
    }
}

