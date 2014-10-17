using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class StripeChargeMapping : CoreEntityMappingBase<StripeCharge, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.StripeChargeId).IsRequired(StripeChargeMetadataConstants.StripeChargeId_IsRequired).HasMaxLength(StripeChargeMetadataConstants.StripeChargeId_MaxLength, StripeChargeMetadataConstants.StripeChargeId_IsMaxLength);
			this.Property(o => o.StripeFee).IsRequired(StripeChargeMetadataConstants.StripeFee_IsRequired).IsCurrency();

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
        protected override bool MapCoreEntityBaseProperties
        {
            get
            {
                return false;
            }
        }
    }
}
