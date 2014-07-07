using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class StripeChargeMapping : CoreEntityMappingBase<StripeCharge, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.StripeChargeId).IsRequired(StripeChargeMetadata.StripeChargeId_IsRequired).HasMaxLength(StripeChargeMetadata.StripeChargeId_MaxLength, StripeChargeMetadata.StripeChargeId_IsMaxLength);
			this.Property(o => o.StripeFee).IsRequired(StripeChargeMetadata.StripeFee_IsRequired).IsCurrency();

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
