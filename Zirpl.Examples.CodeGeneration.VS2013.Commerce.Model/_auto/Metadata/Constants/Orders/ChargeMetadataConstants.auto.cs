using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders
{
    public abstract partial class ChargeMetadataConstants : MetadataConstantsBase
    {
        public const string Date_Name = "Date";
		public const bool Date_IsRequired = true;

        public const string Amount_Name = "Amount";
		public const bool Amount_IsRequired = true;
		public const decimal Amount_MinValue = decimal.MinValue;
        public const decimal Amount_MaxValue = decimal.MaxValue;
		public const double Amount_MinValue_Double = double.MinValue;
        public const double Amount_MaxValue_Double = double.MaxValue;

        public const string ChargeType_Name = "ChargeType";
		public const bool ChargeType_IsRequired = true;

		public const string ChargeTypeId_Name = "ChargeTypeId";
		public const bool ChargeTypeId_IsRequired = true;

        public const string ChargeMethodType_Name = "ChargeMethodType";
		public const bool ChargeMethodType_IsRequired = true;

		public const string ChargeMethodTypeId_Name = "ChargeMethodTypeId";
		public const bool ChargeMethodTypeId_IsRequired = true;

        public const string ChargeStatusType_Name = "ChargeStatusType";
		public const bool ChargeStatusType_IsRequired = true;

		public const string ChargeStatusTypeId_Name = "ChargeStatusTypeId";
		public const bool ChargeStatusTypeId_IsRequired = true;

        public const string Order_Name = "Order";
		public const bool Order_IsRequired = true;

		public const string OrderId_Name = "OrderId";
		public const bool OrderId_IsRequired = true;

	}
}
