﻿using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders
{
    public partial class StripeChargeMetadataConstants : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders.ChargeMetadataConstants
    {
        public const string StripeChargeId_Name = "StripeChargeId";
		public const bool StripeChargeId_IsRequired = true;
		public const bool StripeChargeId_IsMaxLength = false;
        public const int StripeChargeId_MinLength = 1;
		public const int StripeChargeId_MaxLength = 64;

        public const string StripeFee_Name = "StripeFee";
		public const bool StripeFee_IsRequired = true;
		public const decimal StripeFee_MinValue = decimal.MinValue;
        public const decimal StripeFee_MaxValue = decimal.MaxValue;
		public const double StripeFee_MinValue_Double = double.MinValue;
        public const double StripeFee_MaxValue_Double = double.MaxValue;

	}
}
