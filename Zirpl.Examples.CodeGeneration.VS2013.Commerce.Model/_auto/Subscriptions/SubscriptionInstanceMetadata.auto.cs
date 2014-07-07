using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class SubscriptionInstanceMetadata : MetadataBase
    {
        public const string StartDate_Name = "StartDate";
		public const bool StartDate_IsRequired = true;

        public const string TotalShipments_Name = "TotalShipments";
		public const bool TotalShipments_IsRequired = true;
		public const int TotalShipments_MinValue = 1;
        public const int TotalShipments_MaxValue = int.MaxValue;

        public const string ShipmentsRemaining_Name = "ShipmentsRemaining";
		public const bool ShipmentsRemaining_IsRequired = true;
		public const int ShipmentsRemaining_MinValue = 0;
        public const int ShipmentsRemaining_MaxValue = int.MaxValue;

        public const string Subscription_Name = "Subscription";
		public const bool Subscription_IsRequired = true;

		public const string SubscriptionId_Name = "SubscriptionId";
		public const bool SubscriptionId_IsRequired = true;

        public const string CreatedByOrderItem_Name = "CreatedByOrderItem";
		public const bool CreatedByOrderItem_IsRequired = true;

        public const string PendingSubscriptionChange_Name = "PendingSubscriptionChange";
		public const bool PendingSubscriptionChange_IsRequired = false;

		public const string PendingSubscriptionChangeId_Name = "PendingSubscriptionChangeId";
		public const bool PendingSubscriptionChangeId_IsRequired = false;

	}
}
