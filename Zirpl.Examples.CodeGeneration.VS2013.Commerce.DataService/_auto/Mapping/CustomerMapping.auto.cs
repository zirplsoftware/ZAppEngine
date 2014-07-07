using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers
{
    public partial class CustomerMapping : CoreEntityMappingBase<Customer, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Visitor,
                                        o => o.VisitorId,
                                        CustomerMetadata.Visitor_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.PromoCode,
                                        o => o.PromoCodeId,
                                        CustomerMetadata.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CurrentShippingAddress,
                                        o => o.CurrentShippingAddressId,
                                        CustomerMetadata.CurrentShippingAddress_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CurrentCustomerChargeOption,
                                        o => o.CurrentCustomerChargeOptionId,
                                        CustomerMetadata.CurrentCustomerChargeOption_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
