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

			this.Property(o => o.Name).IsRequired(DiscountMetadata.Name_IsRequired).HasMaxLength(DiscountMetadata.Name_MaxLength, DiscountMetadata.Name_IsMaxLength);

            this.HasNavigationProperty(o => o.PromoCode,
                                        o => o.PromoCodeId,
                                        DiscountMetadata.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.DiscountApplicabilityType,
                                        o => o.DiscountApplicabilityTypeId,
                                        DiscountMetadata.DiscountApplicabilityType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Amount).IsRequired(DiscountMetadata.Amount_IsRequired).HasPrecision(18,4);

            this.HasNavigationProperty(o => o.DiscountAmountType,
                                        o => o.DiscountAmountTypeId,
                                        DiscountMetadata.DiscountAmountType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.DiscountUsageRestrictionType,
                                        o => o.DiscountUsageRestrictionTypeId,
                                        DiscountMetadata.DiscountUsageRestrictionType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.DiscountUsageRestrictionQuantity).IsRequired(DiscountMetadata.DiscountUsageRestrictionQuantity_IsRequired);
			this.Property(o => o.StopAfterChargeCyles).IsRequired(DiscountMetadata.StopAfterChargeCyles_IsRequired);
			this.Property(o => o.StartDate).IsRequired(DiscountMetadata.StartDate_IsRequired).IsDateTime();
			this.Property(o => o.EndDate).IsRequired(DiscountMetadata.EndDate_IsRequired).IsDateTime();
			this.Property(o => o.Published).IsRequired(DiscountMetadata.Published_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
