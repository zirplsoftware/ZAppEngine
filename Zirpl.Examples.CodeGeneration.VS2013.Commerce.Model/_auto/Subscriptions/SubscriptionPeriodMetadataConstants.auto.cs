using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class SubscriptionPeriodMetadataConstants : MetadataConstantsBase
    {
        public const string ChargePeriod_Name = "ChargePeriod";
		public const bool ChargePeriod_IsRequired = true;
		public const int ChargePeriod_MinValue = 1;
        public const int ChargePeriod_MaxValue = int.MaxValue;

        public const string ChargePeriodType_Name = "ChargePeriodType";
		public const bool ChargePeriodType_IsRequired = true;

		public const string ChargePeriodTypeId_Name = "ChargePeriodTypeId";
		public const bool ChargePeriodTypeId_IsRequired = true;

        public const string ShipmentPeriod_Name = "ShipmentPeriod";
		public const bool ShipmentPeriod_IsRequired = true;
		public const int ShipmentPeriod_MinValue = 1;
        public const int ShipmentPeriod_MaxValue = int.MaxValue;

        public const string ShipmentPeriodType_Name = "ShipmentPeriodType";
		public const bool ShipmentPeriodType_IsRequired = true;

		public const string ShipmentPeriodTypeId_Name = "ShipmentPeriodTypeId";
		public const bool ShipmentPeriodTypeId_IsRequired = true;

	}
}
