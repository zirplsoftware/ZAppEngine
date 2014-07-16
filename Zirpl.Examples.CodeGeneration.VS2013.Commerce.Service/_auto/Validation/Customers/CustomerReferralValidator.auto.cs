
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public partial class CustomerReferralValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Promotions.ReferralValidator<CustomerReferral>
		
    {
        public CustomerReferralValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.ReferringCustomer, o => o.ReferringCustomerId,
                CustomerReferralMetadata.ReferringCustomer_Name, CustomerReferralMetadata.ReferringCustomerId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferringCustomerDiscountAward, o => o.ReferringCustomerDiscountAwardId,
                CustomerReferralMetadata.ReferringCustomerDiscountAward_Name, CustomerReferralMetadata.ReferringCustomerDiscountAwardId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferringCustomerDiscountAwardUsage, o => o.ReferringCustomerDiscountAwardUsageId,
                CustomerReferralMetadata.ReferringCustomerDiscountAwardUsage_Name, CustomerReferralMetadata.ReferringCustomerDiscountAwardUsageId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

