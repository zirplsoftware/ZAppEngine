using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers
{
    public partial class CustomerReferralMapping : EntityMappingBase<CustomerReferral, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.ReferringCustomer,
                                        o => o.ReferringCustomerId,
                                        CustomerReferralMetadataConstants.ReferringCustomer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferringCustomerDiscountAward,
                                        o => o.ReferringCustomerDiscountAwardId,
                                        CustomerReferralMetadataConstants.ReferringCustomerDiscountAward_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferringCustomerDiscountAwardUsage,
                                        o => o.ReferringCustomerDiscountAwardUsageId,
                                        CustomerReferralMetadataConstants.ReferringCustomerDiscountAwardUsage_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
        protected override bool MapEntityBaseProperties
        {
            get
            {
                return false;
            }
        }
    }
}
