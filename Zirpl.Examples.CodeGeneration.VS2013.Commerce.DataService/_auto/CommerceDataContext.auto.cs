using System;
using System.Data.Entity;
using System.Linq;
using Zirpl.AppEngine.DataService.EntityFramework;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService
{
    public partial class CommerceDataContext : DbContextBase
    {
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Address> Addresses {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.Brand> Brands {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.Category> Categories {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct> DisplayProducts {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.ProductReview> ProductReviews {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.SubscriptionChoice> SubscriptionChoices {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.SubscriptionChoiceTierPriceCombination> SubscriptionChoiceTierPriceCombinations {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.TierPrice> TierPrices {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.TierShipment> TierShipments {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.Customer> Customers {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.CustomerChargeOption> CustomerChargeOptions {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.CustomerReferral> CustomerReferrals {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.CustomerTag> CustomerTags {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.PasswordResetLink> PasswordResetLinks {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.User> Users {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.Visitor> Visitors {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.NamePrefixType> NamePrefixTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.NameSuffixType> NameSuffixTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications.EmailEvent> EmailEvents {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications.EmailEventType> EmailEventTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.Charge> Charges {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.ChargeMethodType> ChargeMethodTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.ChargeStatusType> ChargeStatusTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.ChargeType> ChargeTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsage> DiscountUsages {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsageRestrictionType> DiscountUsageRestrictionTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.Order> Orders {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderChargeStatusType> OrderChargeStatusTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderItem> OrderItems {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderStatusType> OrderStatusTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.ShoppingCartItem> ShoppingCartItems {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.StripeCharge> StripeCharges {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.StripeCustomerChargeOption> StripeCustomerChargeOptions {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.SubscriptionOrderItem> SubscriptionOrderItems {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.SubscriptionOrderItemType> SubscriptionOrderItemTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.Partner> Partners {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferral> PartnerReferrals {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralCouponRequest> PartnerReferralCouponRequests {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralCouponRequestStatusType> PartnerReferralCouponRequestStatusTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralPlan> PartnerReferralPlans {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount> Discounts {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.DiscountAmountType> DiscountAmountTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.DiscountApplicabilityType> DiscountApplicabilityTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.PromoCode> PromoCodes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Referral> Referrals {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings.StateProvinceType> StateProvinceTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings.SystemSetting> SystemSettings {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings.TaxRule> TaxRules {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.PendingSubscriptionChange> PendingSubscriptionChanges {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.Subscription> Subscriptions {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionInstance> SubscriptionInstances {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionPeriod> SubscriptionPeriods {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionPeriodType> SubscriptionPeriodTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions.SubscriptionStatusType> SubscriptionStatusTypes {get; set;}
		public DbSet<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Tag> Tags {get; set;}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {		
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.AddressMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.BrandMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.CategoryMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.DisplayProductMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.ProductReviewMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.SubscriptionChoiceMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.SubscriptionChoiceTierPriceCombinationMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.TierPriceMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog.TierShipmentMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers.CustomerMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers.CustomerChargeOptionMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers.CustomerReferralMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Customers.CustomerTagMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Membership.PasswordResetLinkMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Membership.VisitorMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.NamePrefixTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.NameSuffixTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Notifications.EmailEventMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Notifications.EmailEventTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.ChargeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.ChargeMethodTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.ChargeStatusTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.ChargeTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.DiscountUsageMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.DiscountUsageRestrictionTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.OrderMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.OrderChargeStatusTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.OrderItemMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.OrderStatusTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.ShoppingCartItemMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.StripeChargeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.StripeCustomerChargeOptionMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.SubscriptionOrderItemMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders.SubscriptionOrderItemTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners.PartnerMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners.PartnerReferralMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners.PartnerReferralCouponRequestMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners.PartnerReferralCouponRequestStatusTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners.PartnerReferralPlanMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions.DiscountMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions.DiscountAmountTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions.DiscountApplicabilityTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions.PromoCodeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions.ReferralMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Settings.StateProvinceTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Settings.SystemSettingMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Settings.TaxRuleMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions.PendingSubscriptionChangeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions.SubscriptionMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions.SubscriptionInstanceMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions.SubscriptionPeriodMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions.SubscriptionPeriodTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions.SubscriptionStatusTypeMapping());
			modelBuilder.Configurations.Add(new Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.TagMapping());
			
			this.OnCustomModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
		}

		partial void OnCustomModelCreating(DbModelBuilder modelBuilder);
    }
}
