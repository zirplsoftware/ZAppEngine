using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Catalog
{
    public partial class SubscriptionChoiceTierPriceCombinationMetadataConstants : MetadataConstantsBase
    {
        public const string DisplayProduct_Name = "DisplayProduct";
		public const bool DisplayProduct_IsRequired = true;

		public const string DisplayProductId_Name = "DisplayProductId";
		public const bool DisplayProductId_IsRequired = true;

        public const string SubscriptionChoice_Name = "SubscriptionChoice";
		public const bool SubscriptionChoice_IsRequired = false;

		public const string SubscriptionChoiceId_Name = "SubscriptionChoiceId";
		public const bool SubscriptionChoiceId_IsRequired = false;

        public const string TierPrice_Name = "TierPrice";
		public const bool TierPrice_IsRequired = false;

		public const string TierPriceId_Name = "TierPriceId";
		public const bool TierPriceId_IsRequired = false;

        public const string PriceEach_Name = "PriceEach";
		public const bool PriceEach_IsRequired = true;
		public const decimal PriceEach_MinValue = 0.01m;
        public const decimal PriceEach_MaxValue = decimal.MaxValue;
		public const double PriceEach_MinValue_Double = 0.01D;
        public const double PriceEach_MaxValue_Double = double.MaxValue;

	}
}
