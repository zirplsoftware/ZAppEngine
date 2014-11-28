using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Catalog
{
    public partial class TierPriceMetadataConstants : MetadataConstantsBase
    {
        public const string DisplayProduct_Name = "DisplayProduct";
		public const bool DisplayProduct_IsRequired = true;

		public const string DisplayProductId_Name = "DisplayProductId";
		public const bool DisplayProductId_IsRequired = true;

        public const string Quantity_Name = "Quantity";
		public const bool Quantity_IsRequired = true;
		public const int Quantity_MinValue = 1;
        public const int Quantity_MaxValue = int.MaxValue;

        public const string PriceEach_Name = "PriceEach";
		public const bool PriceEach_IsRequired = true;
		public const decimal PriceEach_MinValue = 0.01m;
        public const decimal PriceEach_MaxValue = decimal.MaxValue;
		public const double PriceEach_MinValue_Double = 0.01D;
        public const double PriceEach_MaxValue_Double = double.MaxValue;

	}
}
