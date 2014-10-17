using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerReferralCouponRequestStatusTypeMapping : DictionaryEntityMapping<PartnerReferralCouponRequestStatusType, byte, PartnerReferralCouponRequestStatusTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(PartnerReferralCouponRequestStatusTypeMetadataConstants.Name_IsRequired).HasMaxLength(PartnerReferralCouponRequestStatusTypeMetadataConstants.Name_MaxLength, PartnerReferralCouponRequestStatusTypeMetadataConstants.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
