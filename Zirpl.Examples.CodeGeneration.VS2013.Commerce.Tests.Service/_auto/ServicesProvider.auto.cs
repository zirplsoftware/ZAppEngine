using System;
using Zirpl.AppEngine.DataService;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Service
{
    public partial class ServicesProvider
    {
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.IAddressService AddressService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.IBrandService BrandService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.ICategoryService CategoryService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.IDisplayProductService DisplayProductService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.IProductReviewService ProductReviewService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.ISubscriptionChoiceService SubscriptionChoiceService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.ISubscriptionChoiceTierPriceCombinationService SubscriptionChoiceTierPriceCombinationService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.ITierPriceService TierPriceService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Catalog.ITierShipmentService TierShipmentService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Customers.ICustomerService CustomerService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Customers.ICustomerChargeOptionService CustomerChargeOptionService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Customers.ICustomerReferralService CustomerReferralService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Customers.ICustomerTagService CustomerTagService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Membership.IPasswordResetLinkService PasswordResetLinkService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Membership.IVisitorService VisitorService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.INamePrefixTypeService NamePrefixTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.INameSuffixTypeService NameSuffixTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Notifications.IEmailEventService EmailEventService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Notifications.IEmailEventTypeService EmailEventTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IChargeService ChargeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IChargeMethodTypeService ChargeMethodTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IChargeStatusTypeService ChargeStatusTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IChargeTypeService ChargeTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IDiscountUsageService DiscountUsageService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IDiscountUsageRestrictionTypeService DiscountUsageRestrictionTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IOrderService OrderService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IOrderChargeStatusTypeService OrderChargeStatusTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IOrderItemService OrderItemService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IOrderStatusTypeService OrderStatusTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IShoppingCartItemService ShoppingCartItemService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IStripeChargeService StripeChargeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.IStripeCustomerChargeOptionService StripeCustomerChargeOptionService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.ISubscriptionOrderItemService SubscriptionOrderItemService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Orders.ISubscriptionOrderItemTypeService SubscriptionOrderItemTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Partners.IPartnerService PartnerService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Partners.IPartnerReferralService PartnerReferralService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Partners.IPartnerReferralCouponRequestService PartnerReferralCouponRequestService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Partners.IPartnerReferralCouponRequestStatusTypeService PartnerReferralCouponRequestStatusTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Partners.IPartnerReferralPlanService PartnerReferralPlanService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Promotions.IDiscountService DiscountService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Promotions.IDiscountAmountTypeService DiscountAmountTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Promotions.IDiscountApplicabilityTypeService DiscountApplicabilityTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Promotions.IPromoCodeService PromoCodeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Promotions.IReferralService ReferralService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Settings.IStateProvinceTypeService StateProvinceTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Settings.ISystemSettingService SystemSettingService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Settings.ITaxRuleService TaxRuleService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions.IPendingSubscriptionChangeService PendingSubscriptionChangeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions.ISubscriptionService SubscriptionService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions.ISubscriptionInstanceService SubscriptionInstanceService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions.ISubscriptionPeriodService SubscriptionPeriodService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions.ISubscriptionPeriodTypeService SubscriptionPeriodTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Subscriptions.ISubscriptionStatusTypeService SubscriptionStatusTypeService { get; set; }
			public Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.ITagService TagService { get; set; }
    }
}