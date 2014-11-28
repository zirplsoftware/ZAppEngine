using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders
{
    public partial class SubscriptionOrderItemMetadataConstants : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders.OrderItemMetadataConstants
    {
        public const string SubscriptionOrderItemType_Name = "SubscriptionOrderItemType";
		public const bool SubscriptionOrderItemType_IsRequired = true;

		public const string SubscriptionOrderItemTypeId_Name = "SubscriptionOrderItemTypeId";
		public const bool SubscriptionOrderItemTypeId_IsRequired = true;

        public const string SubscriptionPeriod_Name = "SubscriptionPeriod";
		public const bool SubscriptionPeriod_IsRequired = false;

		public const string SubscriptionPeriodId_Name = "SubscriptionPeriodId";
		public const bool SubscriptionPeriodId_IsRequired = false;

        public const string TriggeredBySubscriptionInstance_Name = "TriggeredBySubscriptionInstance";
		public const bool TriggeredBySubscriptionInstance_IsRequired = false;

		public const string TriggeredBySubscriptionInstanceId_Name = "TriggeredBySubscriptionInstanceId";
		public const bool TriggeredBySubscriptionInstanceId_IsRequired = false;

        public const string ResultingSubscriptionInstance_Name = "ResultingSubscriptionInstance";
		public const bool ResultingSubscriptionInstance_IsRequired = false;

	}
}
