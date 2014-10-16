using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions
{
    public partial class DiscountMetadataConstants : MetadataConstantsBase
    {
        public const string Name_Name = "Name";
		public const bool Name_IsRequired = true;
		public const bool Name_IsMaxLength = false;
        public const int Name_MinLength = 1;
		public const int Name_MaxLength = 256;

        public const string PromoCode_Name = "PromoCode";
		public const bool PromoCode_IsRequired = true;

		public const string PromoCodeId_Name = "PromoCodeId";
		public const bool PromoCodeId_IsRequired = true;

        public const string DiscountApplicabilityType_Name = "DiscountApplicabilityType";
		public const bool DiscountApplicabilityType_IsRequired = true;

		public const string DiscountApplicabilityTypeId_Name = "DiscountApplicabilityTypeId";
		public const bool DiscountApplicabilityTypeId_IsRequired = true;

        public const string Amount_Name = "Amount";
		public const bool Amount_IsRequired = true;
		public const decimal Amount_MinValue = 0m;
        public const decimal Amount_MaxValue = decimal.MaxValue;
		public const double Amount_MinValue_Double = 0D;
        public const double Amount_MaxValue_Double = double.MaxValue;

        public const string DiscountAmountType_Name = "DiscountAmountType";
		public const bool DiscountAmountType_IsRequired = true;

		public const string DiscountAmountTypeId_Name = "DiscountAmountTypeId";
		public const bool DiscountAmountTypeId_IsRequired = true;

        public const string DiscountUsageRestrictionType_Name = "DiscountUsageRestrictionType";
		public const bool DiscountUsageRestrictionType_IsRequired = true;

		public const string DiscountUsageRestrictionTypeId_Name = "DiscountUsageRestrictionTypeId";
		public const bool DiscountUsageRestrictionTypeId_IsRequired = true;

        public const string DiscountUsageRestrictionQuantity_Name = "DiscountUsageRestrictionQuantity";
		public const bool DiscountUsageRestrictionQuantity_IsRequired = false;
		public const int DiscountUsageRestrictionQuantity_MinValue = int.MinValue;
        public const int DiscountUsageRestrictionQuantity_MaxValue = int.MaxValue;

        public const string StopAfterChargeCyles_Name = "StopAfterChargeCyles";
		public const bool StopAfterChargeCyles_IsRequired = false;
		public const int StopAfterChargeCyles_MinValue = 1;
        public const int StopAfterChargeCyles_MaxValue = int.MaxValue;

        public const string StartDate_Name = "StartDate";
		public const bool StartDate_IsRequired = false;

        public const string EndDate_Name = "EndDate";
		public const bool EndDate_IsRequired = false;

        public const string Published_Name = "Published";
		public const bool Published_IsRequired = true;

        public const string AppliesToDisplayProducts_Name = "AppliesToDisplayProducts";
		public const bool AppliesToDisplayProducts_IsRequired = false;

	}
}
