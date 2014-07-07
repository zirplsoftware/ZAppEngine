using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerReferralCouponRequestStatusTypeMapping : DictionaryEntityMapping<PartnerReferralCouponRequestStatusType, byte, PartnerReferralCouponRequestStatusTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(PartnerReferralCouponRequestStatusTypeMetadata.Name_IsRequired).HasMaxLength(PartnerReferralCouponRequestStatusTypeMetadata.Name_MaxLength, PartnerReferralCouponRequestStatusTypeMetadata.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
