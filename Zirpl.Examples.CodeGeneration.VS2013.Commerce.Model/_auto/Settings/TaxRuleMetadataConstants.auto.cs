using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings
{
    public partial class TaxRuleMetadataConstants : MetadataConstantsBase
    {
        public const string Rate_Name = "Rate";
		public const bool Rate_IsRequired = true;
		public const decimal Rate_MinValue = 0.0001m;
        public const decimal Rate_MaxValue = decimal.MaxValue;
		public const double Rate_MinValue_Double = 0.0001D;
        public const double Rate_MaxValue_Double = double.MaxValue;

        public const string StateProvinceType_Name = "StateProvinceType";
		public const bool StateProvinceType_IsRequired = true;

		public const string StateProvinceTypeId_Name = "StateProvinceTypeId";
		public const bool StateProvinceTypeId_IsRequired = true;

	}
}
