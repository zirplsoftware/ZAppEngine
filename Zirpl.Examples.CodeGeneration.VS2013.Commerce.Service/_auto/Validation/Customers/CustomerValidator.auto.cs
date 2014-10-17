
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public partial class CustomerValidator  : DbEntityValidatorBase<Customer>
		
    {
        public CustomerValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Visitor, o => o.VisitorId,
                CustomerMetadataConstants.Visitor_Name, CustomerMetadataConstants.VisitorId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                CustomerMetadataConstants.PromoCode_Name, CustomerMetadataConstants.PromoCodeId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.CurrentShippingAddress, o => o.CurrentShippingAddressId,
                CustomerMetadataConstants.CurrentShippingAddress_Name, CustomerMetadataConstants.CurrentShippingAddressId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.CurrentCustomerChargeOption, o => o.CurrentCustomerChargeOptionId,
                CustomerMetadataConstants.CurrentCustomerChargeOption_Name, CustomerMetadataConstants.CurrentCustomerChargeOptionId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

