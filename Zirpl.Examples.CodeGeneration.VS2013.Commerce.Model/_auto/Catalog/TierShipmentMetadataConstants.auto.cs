using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class TierShipmentMetadataConstants : MetadataConstantsBase
    {
        public const string DisplayProduct_Name = "DisplayProduct";
		public const bool DisplayProduct_IsRequired = true;

		public const string DisplayProductId_Name = "DisplayProductId";
		public const bool DisplayProductId_IsRequired = true;

        public const string Quantity_Name = "Quantity";
		public const bool Quantity_IsRequired = true;
		public const int Quantity_MinValue = 1;
        public const int Quantity_MaxValue = int.MaxValue;

        public const string BaseWeightInOunces_Name = "BaseWeightInOunces";
		public const bool BaseWeightInOunces_IsRequired = true;
		public const decimal BaseWeightInOunces_MinValue = 0m;
        public const decimal BaseWeightInOunces_MaxValue = decimal.MaxValue;
		public const double BaseWeightInOunces_MinValue_Double = 0D;
        public const double BaseWeightInOunces_MaxValue_Double = double.MaxValue;

        public const string WeightInOuncesEach_Name = "WeightInOuncesEach";
		public const bool WeightInOuncesEach_IsRequired = true;
		public const decimal WeightInOuncesEach_MinValue = 0m;
        public const decimal WeightInOuncesEach_MaxValue = decimal.MaxValue;
		public const double WeightInOuncesEach_MinValue_Double = 0D;
        public const double WeightInOuncesEach_MaxValue_Double = double.MaxValue;

        public const string RequiresManualShipmentHandling_Name = "RequiresManualShipmentHandling";
		public const bool RequiresManualShipmentHandling_IsRequired = true;

	}
}
