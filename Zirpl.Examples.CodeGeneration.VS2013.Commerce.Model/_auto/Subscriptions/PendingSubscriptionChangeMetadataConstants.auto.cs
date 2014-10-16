using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class PendingSubscriptionChangeMetadataConstants : MetadataConstantsBase
    {
        public const string Quantity_Name = "Quantity";
		public const bool Quantity_IsRequired = true;
		public const int Quantity_MinValue = 1;
        public const int Quantity_MaxValue = int.MaxValue;

        public const string SubscriptionChoice_Name = "SubscriptionChoice";
		public const bool SubscriptionChoice_IsRequired = true;

		public const string SubscriptionChoiceId_Name = "SubscriptionChoiceId";
		public const bool SubscriptionChoiceId_IsRequired = true;

	}
}
