using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions
{
    public partial class DiscountMapping : CoreEntityMappingBase<Discount, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(DiscountMetadataConstants.Name_IsRequired).HasMaxLength(DiscountMetadataConstants.Name_MaxLength, DiscountMetadataConstants.Name_IsMaxLength);

            this.HasNavigationProperty(o => o.PromoCode,
                                        o => o.PromoCodeId,
                                        DiscountMetadataConstants.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.DiscountApplicabilityType,
                                        o => o.DiscountApplicabilityTypeId,
                                        DiscountMetadataConstants.DiscountApplicabilityType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Amount).IsRequired(DiscountMetadataConstants.Amount_IsRequired).HasPrecision(18,4);

            this.HasNavigationProperty(o => o.DiscountAmountType,
                                        o => o.DiscountAmountTypeId,
                                        DiscountMetadataConstants.DiscountAmountType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.DiscountUsageRestrictionType,
                                        o => o.DiscountUsageRestrictionTypeId,
                                        DiscountMetadataConstants.DiscountUsageRestrictionType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.DiscountUsageRestrictionQuantity).IsRequired(DiscountMetadataConstants.DiscountUsageRestrictionQuantity_IsRequired);
			this.Property(o => o.StopAfterChargeCyles).IsRequired(DiscountMetadataConstants.StopAfterChargeCyles_IsRequired);
			this.Property(o => o.StartDate).IsRequired(DiscountMetadataConstants.StartDate_IsRequired).IsDateTime();
			this.Property(o => o.EndDate).IsRequired(DiscountMetadataConstants.EndDate_IsRequired).IsDateTime();
			this.Property(o => o.Published).IsRequired(DiscountMetadataConstants.Published_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
