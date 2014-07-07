using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class StripeCustomerChargeOptionMapping : CoreEntityMappingBase<StripeCustomerChargeOption, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.StripeCustomerId).IsRequired(StripeCustomerChargeOptionMetadata.StripeCustomerId_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadata.StripeCustomerId_MaxLength, StripeCustomerChargeOptionMetadata.StripeCustomerId_IsMaxLength);
			this.Property(o => o.Last4OfCreditCard).IsRequired(StripeCustomerChargeOptionMetadata.Last4OfCreditCard_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadata.Last4OfCreditCard_MaxLength, StripeCustomerChargeOptionMetadata.Last4OfCreditCard_IsMaxLength);
			this.Property(o => o.ExpirationMonthOfCreditCard).IsRequired(StripeCustomerChargeOptionMetadata.ExpirationMonthOfCreditCard_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadata.ExpirationMonthOfCreditCard_MaxLength, StripeCustomerChargeOptionMetadata.ExpirationMonthOfCreditCard_IsMaxLength);
			this.Property(o => o.ExpirationYearOfCreditCard).IsRequired(StripeCustomerChargeOptionMetadata.ExpirationYearOfCreditCard_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadata.ExpirationYearOfCreditCard_MaxLength, StripeCustomerChargeOptionMetadata.ExpirationYearOfCreditCard_IsMaxLength);
			this.Property(o => o.CreditCardFingerPrint).IsRequired(StripeCustomerChargeOptionMetadata.CreditCardFingerPrint_IsRequired).HasMaxLength(StripeCustomerChargeOptionMetadata.CreditCardFingerPrint_MaxLength, StripeCustomerChargeOptionMetadata.CreditCardFingerPrint_IsMaxLength);

            this.HasNavigationProperty(o => o.BillingAddress,
                                        o => o.BillingAddressId,
                                        StripeCustomerChargeOptionMetadata.BillingAddress_IsRequired,
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
