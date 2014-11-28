using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers
{
    public partial class CustomerMapping : EntityMappingBase<Customer, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Visitor,
                                        o => o.VisitorId,
                                        CustomerMetadataConstants.Visitor_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.PromoCode,
                                        o => o.PromoCodeId,
                                        CustomerMetadataConstants.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CurrentShippingAddress,
                                        o => o.CurrentShippingAddressId,
                                        CustomerMetadataConstants.CurrentShippingAddress_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CurrentCustomerChargeOption,
                                        o => o.CurrentCustomerChargeOptionId,
                                        CustomerMetadataConstants.CurrentCustomerChargeOption_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
