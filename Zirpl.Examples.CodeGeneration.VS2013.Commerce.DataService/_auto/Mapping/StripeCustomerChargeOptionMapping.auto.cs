using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class StripeCustomerChargeOptionMapping : CoreEntityMappingBase<StripeCustomerChargeOption, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.StripeCustomerId).IsRequired(StripeCustomerChargeOptionMetadataConstants.StripeCustomerId_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadataConstants.StripeCustomerId_MaxLength, StripeCustomerChargeOptionMetadataConstants.StripeCustomerId_IsMaxLength);
			this.Property(o => o.Last4OfCreditCard).IsRequired(StripeCustomerChargeOptionMetadataConstants.Last4OfCreditCard_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadataConstants.Last4OfCreditCard_MaxLength, StripeCustomerChargeOptionMetadataConstants.Last4OfCreditCard_IsMaxLength);
			this.Property(o => o.ExpirationMonthOfCreditCard).IsRequired(StripeCustomerChargeOptionMetadataConstants.ExpirationMonthOfCreditCard_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadataConstants.ExpirationMonthOfCreditCard_MaxLength, StripeCustomerChargeOptionMetadataConstants.ExpirationMonthOfCreditCard_IsMaxLength);
			this.Property(o => o.ExpirationYearOfCreditCard).IsRequired(StripeCustomerChargeOptionMetadataConstants.ExpirationYearOfCreditCard_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadataConstants.ExpirationYearOfCreditCard_MaxLength, StripeCustomerChargeOptionMetadataConstants.ExpirationYearOfCreditCard_IsMaxLength);
			this.Property(o => o.CreditCardFingerPrint).IsRequired(StripeCustomerChargeOptionMetadataConstants.CreditCardFingerPrint_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadataConstants.CreditCardFingerPrint_MaxLength, StripeCustomerChargeOptionMetadataConstants.CreditCardFingerPrint_IsMaxLength);

            this.HasNavigationProperty(o => o.BillingAddress,
                                        o => o.BillingAddressId,
                                        StripeCustomerChargeOptionMetadataConstants.BillingAddress_IsRequired,
                                        CascadeOnDeleteOption.No);

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
