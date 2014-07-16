
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public partial class CustomerValidator  : DbEntityValidatorBase<Customer>
		
    {
        public CustomerValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Visitor, o => o.VisitorId,
                CustomerMetadata.Visitor_Name, CustomerMetadata.VisitorId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                CustomerMetadata.PromoCode_Name, CustomerMetadata.PromoCodeId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.CurrentShippingAddress, o => o.CurrentShippingAddressId,
                CustomerMetadata.CurrentShippingAddress_Name, CustomerMetadata.CurrentShippingAddressId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.CurrentCustomerChargeOption, o => o.CurrentCustomerChargeOptionId,
                CustomerMetadata.CurrentCustomerChargeOption_Name, CustomerMetadata.CurrentCustomerChargeOptionId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

