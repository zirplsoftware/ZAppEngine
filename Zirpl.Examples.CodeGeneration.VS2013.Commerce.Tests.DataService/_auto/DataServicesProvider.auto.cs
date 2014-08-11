using System;
using Zirpl.AppEngine.DataService;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.DataService
{
    public partial class DataServicesProvider
    {
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.IAddressDataService AddressDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.IBrandDataService BrandDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.ICategoryDataService CategoryDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.IDisplayProductDataService DisplayProductDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.IProductReviewDataService ProductReviewDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.ISubscriptionChoiceDataService SubscriptionChoiceDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.ISubscriptionChoiceTierPriceCombinationDataService SubscriptionChoiceTierPriceCombinationDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.ITierPriceDataService TierPriceDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog.ITierShipmentDataService TierShipmentDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers.ICustomerDataService CustomerDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers.ICustomerChargeOptionDataService CustomerChargeOptionDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers.ICustomerReferralDataService CustomerReferralDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers.ICustomerTagDataService CustomerTagDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Membership.IPasswordResetLinkDataService PasswordResetLinkDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Membership.IVisitorDataService VisitorDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.INamePrefixTypeDataService NamePrefixTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.INameSuffixTypeDataService NameSuffixTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Notifications.IEmailEventDataService EmailEventDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Notifications.IEmailEventTypeDataService EmailEventTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IChargeDataService ChargeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IChargeMethodTypeDataService ChargeMethodTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IChargeStatusTypeDataService ChargeStatusTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IChargeTypeDataService ChargeTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IDiscountUsageDataService DiscountUsageDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IDiscountUsageRestrictionTypeDataService DiscountUsageRestrictionTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IOrderDataService OrderDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IOrderChargeStatusTypeDataService OrderChargeStatusTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IOrderItemDataService OrderItemDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IOrderStatusTypeDataService OrderStatusTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IShoppingCartItemDataService ShoppingCartItemDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IStripeChargeDataService StripeChargeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.IStripeCustomerChargeOptionDataService StripeCustomerChargeOptionDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.ISubscriptionOrderItemDataService SubscriptionOrderItemDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders.ISubscriptionOrderItemTypeDataService SubscriptionOrderItemTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners.IPartnerDataService PartnerDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners.IPartnerReferralDataService PartnerReferralDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners.IPartnerReferralCouponRequestDataService PartnerReferralCouponRequestDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners.IPartnerReferralCouponRequestStatusTypeDataService PartnerReferralCouponRequestStatusTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners.IPartnerReferralPlanDataService PartnerReferralPlanDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions.IDiscountDataService DiscountDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions.IDiscountAmountTypeDataService DiscountAmountTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions.IDiscountApplicabilityTypeDataService DiscountApplicabilityTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions.IPromoCodeDataService PromoCodeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions.IReferralDataService ReferralDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Settings.IStateProvinceTypeDataService StateProvinceTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Settings.ISystemSettingDataService SystemSettingDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Settings.ITaxRuleDataService TaxRuleDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions.IPendingSubscriptionChangeDataService PendingSubscriptionChangeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions.ISubscriptionDataService SubscriptionDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions.ISubscriptionInstanceDataService SubscriptionInstanceDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions.ISubscriptionPeriodDataService SubscriptionPeriodDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions.ISubscriptionPeriodTypeDataService SubscriptionPeriodTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions.ISubscriptionStatusTypeDataService SubscriptionStatusTypeDataService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.ITagDataService TagDataService { get; set; }
    }
}