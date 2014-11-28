using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Subscriptions
{
    public partial class SubscriptionMetadataConstants : MetadataConstantsBase
    {
        public const string StartDate_Name = "StartDate";
		public const bool StartDate_IsRequired = true;

        public const string StatusType_Name = "StatusType";
		public const bool StatusType_IsRequired = true;

		public const string StatusTypeId_Name = "StatusTypeId";
		public const bool StatusTypeId_IsRequired = true;

        public const string NextShipmentDate_Name = "NextShipmentDate";
		public const bool NextShipmentDate_IsRequired = true;

        public const string NextChargeDate_Name = "NextChargeDate";
		public const bool NextChargeDate_IsRequired = true;

        public const string AutoRenew_Name = "AutoRenew";
		public const bool AutoRenew_IsRequired = true;

        public const string Customer_Name = "Customer";
		public const bool Customer_IsRequired = true;

		public const string CustomerId_Name = "CustomerId";
		public const bool CustomerId_IsRequired = true;

        public const string ShippingAddress_Name = "ShippingAddress";
		public const bool ShippingAddress_IsRequired = true;

		public const string ShippingAddressId_Name = "ShippingAddressId";
		public const bool ShippingAddressId_IsRequired = true;

        public const string CustomerChargeOption_Name = "CustomerChargeOption";
		public const bool CustomerChargeOption_IsRequired = true;

		public const string CustomerChargeOptionId_Name = "CustomerChargeOptionId";
		public const bool CustomerChargeOptionId_IsRequired = true;

        public const string CurrentSubscriptionInstance_Name = "CurrentSubscriptionInstance";
		public const bool CurrentSubscriptionInstance_IsRequired = false;

		public const string CurrentSubscriptionInstanceId_Name = "CurrentSubscriptionInstanceId";
		public const bool CurrentSubscriptionInstanceId_IsRequired = false;

	}
}
