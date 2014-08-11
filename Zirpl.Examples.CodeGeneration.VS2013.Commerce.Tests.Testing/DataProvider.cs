using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.Membership;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests
{
    public class DataProvider
    {
        private IDependencyResolver DependencyResolver { get; set; }
        private Random Random { get; set; }
        private ITransactionalUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        public DataProvider()
        {
            this.DependencyResolver = IocUtils.DependencyResolver;
            this.Random = new Random();
        }




        protected virtual MembershipUser CreateAndGetUser(String emailAddress, String password)
        {
            System.Web.Security.MembershipCreateStatus createStatus;
            System.Web.Security.Membership.CreateUser(emailAddress, password, emailAddress, null, null, true, null, out createStatus);
            if (createStatus != MembershipCreateStatus.Success)
            {
                throw new Exception("Could not create user");
            }
            return System.Web.Security.Membership.GetUser(emailAddress);
        }

        protected MembershipUser CreateAndGetUser()
        {
            return CreateAndGetUser(Guid.NewGuid().ToString() + "@zirpl.com", System.Web.Security.Membership.GeneratePassword(8, 0));
        }




        public virtual Brand CreateBrand()
        {
            var brand = this.DependencyResolver.Resolve<ISupportsCreate<Brand>>().Create();
            brand.Description = Guid.NewGuid().ToString();
            brand.Name = Guid.NewGuid().ToString();
            brand.SeoId = Guid.NewGuid().ToString();
            return brand;
        }

        public virtual Tag CreateTag()
        {
            var tag = this.DependencyResolver.Resolve<ISupportsCreate<Tag>>().Create();
            tag.Description = Guid.NewGuid().ToString();
            tag.Name = Guid.NewGuid().ToString();
            tag.SeoId = Guid.NewGuid().ToString();
            return tag;
        }

        public virtual Category CreateCategory(Category parent = null)
        {
            var category = this.DependencyResolver.Resolve<ISupportsCreate<Category>>().Create();
            category.Parent = parent;
            category.Description = Guid.NewGuid().ToString();
            category.Name = Guid.NewGuid().ToString();
            category.SeoId = Guid.NewGuid().ToString();
            return category;
        }

        public virtual CustomerTag CreateCustomerTag(Customer customer, Tag tag)
        {
            var customerTag = this.DependencyResolver.Resolve<ISupportsCreate<CustomerTag>>().Create();
            customerTag.Tag = tag;
            customerTag.Customer = customer;
            return customerTag;
        }

        public virtual ProductReview CreateProductReview(DisplayProduct displayProduct)
        {
            var review = this.DependencyResolver.Resolve<ISupportsCreate<ProductReview>>().Create();
            review.DisplayProduct = displayProduct;
            review.Date = DateTime.Now;
            review.ReviewerLocation = Guid.NewGuid().ToString(); ;
            review.ReviewerName = Guid.NewGuid().ToString(); ;
            review.Stars = 4;
            review.Text = Guid.NewGuid().ToString();
            return review;
        }

        public virtual SupportUserRegistrationRequest CreateSupportUserRegistrationRequest()
        {
            var supportUserRegistrationRequest = new SupportUserRegistrationRequest();
            supportUserRegistrationRequest.EmailAddress = this.GenerateEmail();
            supportUserRegistrationRequest.Password = this.DependencyResolver.Resolve<IMembershipService>().GeneratePassword();
            return supportUserRegistrationRequest;
        }

        public virtual PasswordResetLink CreatePasswordResetLink(User user)
        {
            var link = this.DependencyResolver.Resolve<ISupportsCreate<PasswordResetLink>>().Create();
            link.User = user;
            link.Token = Guid.NewGuid();
            link.Expires = DateTime.Now.AddDays(1);
            return link;
        }

        public virtual Discount CreateDiscount(PromoCode promoCode)
        {
            var discount = this.DependencyResolver.Resolve<ISupportsCreate<Discount>>().Create();
            discount.Name = System.Guid.NewGuid().ToString();
            discount.DiscountAmountType = this.DependencyResolver.Resolve<ISupportsGetById<DiscountAmountType, byte>>().Get((byte)DiscountAmountTypeEnum.ItemQuantityFree);
            discount.DiscountUsageRestrictionType = this.DependencyResolver.Resolve<ISupportsGetById<DiscountUsageRestrictionType, byte>>().Get((byte)DiscountUsageRestrictionTypeEnum.NTimesPerCustomer);
            discount.DiscountApplicabilityType = this.DependencyResolver.Resolve<ISupportsGetById<DiscountApplicabilityType, byte>>().Get((byte)DiscountApplicabilityTypeEnum.Item);
            discount.Amount = 2;
            discount.DiscountUsageRestrictionQuantity = 5;
            discount.StartDate = DateTime.Today;
            discount.EndDate = DateTime.Today.AddDays(3);
            discount.StopAfterChargeCyles = 5;
            discount.PromoCode = promoCode;
            discount.Published = true;

            return discount;
        }

        public virtual ShoppingCartItem CreateShoppingCartItem(Visitor visitor, DisplayProduct displayProduct, SubscriptionChoice subscriptionChoice = null)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<ShoppingCartItem>>().Create();
            entity.Quantity = 2;
            entity.Visitor = visitor;
            entity.DisplayProduct = displayProduct;
            entity.SubscriptionChoice = subscriptionChoice;
            return entity;
        }

        public virtual Order CreateOrder(Customer customer, CustomerChargeOption customerChargeOptionOverride = null, Address shippingAddressOverride = null)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<Order>>().Create();
            entity.Customer = customer;
            entity.ShippingAddress = shippingAddressOverride ?? (customer == null ? null : customer.CurrentShippingAddress);
            entity.CustomerChargeOption = customerChargeOptionOverride ?? (customer == null ? null : customer.CurrentCustomerChargeOption);
            entity.Date = DateTime.Now;
            entity.OrderChargeStatusType = this.DependencyResolver.Resolve<ISupportsGetById<OrderChargeStatusType, byte>>().Get((byte)OrderChargeStatusTypeEnum.Processing);
            entity.OrderStatusType = this.DependencyResolver.Resolve<ISupportsGetById<OrderStatusType, byte>>().Get((byte)OrderStatusTypeEnum.PaymentProcessing);
            //entity.OriginalShippingAmount = 0;
            entity.OriginalSubtotalAmount = 0;
            entity.OriginalTaxAmount = 0;
            entity.OriginalTotalAmount = 0;
            //entity.ShippingAmountBeforeDiscount = 0;
            entity.SubtotalAmountBeforeDiscount = 0;
            //entity.OrderItems
            return entity;
        }

        public virtual OrderItem CreateOrderItem(Order order, DisplayProduct displayProduct)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<OrderItem>>().Create();
            entity.DisplayProduct = displayProduct;
            entity.ItemName = displayProduct == null ? null : displayProduct.Name;
            entity.Order = order;
            //entity.ShippingAmountBeforeDiscount = 0;
            entity.ItemAmountBeforeDiscount = 10;
            entity.OriginalItemAmount = 10;
            //entity.OriginalShippingAmount = 0;
            entity.Quantity = 2;
            return entity;
        }

        public virtual SubscriptionOrderItem CreateSubscriptionInitialOrderItem(Order order, DisplayProduct displayProduct, SubscriptionPeriod subscriptionPeriod)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SubscriptionOrderItem>>().Create();
            entity.SubscriptionOrderItemType = this.DependencyResolver.Resolve<ISupportsGetById<SubscriptionOrderItemType, byte>>().Get((byte)SubscriptionOrderItemTypeEnum.Initial);
            entity.DisplayProduct = displayProduct;
            entity.ItemName = displayProduct == null ? null : displayProduct.Name;
            entity.Order = order;
            //entity.ShippingAmountBeforeDiscount = 0;
            entity.ItemAmountBeforeDiscount = 10;
            entity.OriginalItemAmount = 10;
            //entity.OriginalShippingAmount = 0;
            entity.Quantity = 2;
            entity.SubscriptionPeriod = subscriptionPeriod;
            return entity;
        }

        public virtual SubscriptionOrderItem CreateSubscriptionRenewalOrderItem(Order order, SubscriptionInstance triggeringSubscriptionInstance, SubscriptionPeriod subscriptionPeriod = null)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SubscriptionOrderItem>>().Create();
            entity.SubscriptionOrderItemType = this.DependencyResolver.Resolve<ISupportsGetById<SubscriptionOrderItemType, byte>>().Get((byte)SubscriptionOrderItemTypeEnum.Renewal);
            entity.DisplayProduct = triggeringSubscriptionInstance == null ? null : triggeringSubscriptionInstance.CreatedByOrderItem.DisplayProduct;
            entity.ItemName = triggeringSubscriptionInstance == null ? null : triggeringSubscriptionInstance.CreatedByOrderItem.ItemName;
            entity.Order = order;
            entity.ItemAmountBeforeDiscount = triggeringSubscriptionInstance == null ? 0 : triggeringSubscriptionInstance.CreatedByOrderItem.ItemAmountBeforeDiscount;
            entity.OriginalItemAmount = triggeringSubscriptionInstance == null ? 0 : triggeringSubscriptionInstance.CreatedByOrderItem.OriginalItemAmount;
            entity.Quantity = triggeringSubscriptionInstance == null ? 0 : triggeringSubscriptionInstance.CreatedByOrderItem.Quantity;
            entity.TriggeredBySubscriptionInstance = triggeringSubscriptionInstance;
            entity.SubscriptionPeriod = subscriptionPeriod ?? (triggeringSubscriptionInstance == null
                                            ? null
                                            : triggeringSubscriptionInstance.CreatedByOrderItem.SubscriptionPeriod);
            return entity;
        }

        public virtual SubscriptionOrderItem CreateSubscriptionShipmentOrderItem(Order order, SubscriptionInstance triggeringSubscriptionInstance)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SubscriptionOrderItem>>().Create();
            entity.SubscriptionOrderItemType = this.DependencyResolver.Resolve<ISupportsGetById<SubscriptionOrderItemType, byte>>().Get((byte)SubscriptionOrderItemTypeEnum.Shipment);
            entity.DisplayProduct = triggeringSubscriptionInstance == null ? null : triggeringSubscriptionInstance.CreatedByOrderItem.DisplayProduct;
            entity.ItemName = triggeringSubscriptionInstance == null ? null : triggeringSubscriptionInstance.CreatedByOrderItem.ItemName;
            entity.Order = order;
            entity.ItemAmountBeforeDiscount = triggeringSubscriptionInstance == null ? 0 : triggeringSubscriptionInstance.CreatedByOrderItem.ItemAmountBeforeDiscount;
            entity.OriginalItemAmount = triggeringSubscriptionInstance == null ? 0 : triggeringSubscriptionInstance.CreatedByOrderItem.OriginalItemAmount;
            entity.Quantity = triggeringSubscriptionInstance == null ? 0 : triggeringSubscriptionInstance.CreatedByOrderItem.Quantity;
            entity.TriggeredBySubscriptionInstance = triggeringSubscriptionInstance;
            return entity;
        }

        public virtual DiscountUsage CreateDiscountUsage(Order order, Discount discount, OrderItem orderItem = null)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<DiscountUsage>>().Create();
            entity.DateUsed = DateTime.Now;
            entity.Discount = discount;
            entity.Order = order;
            entity.OrderItem = orderItem;
            return entity;
        }

        public virtual StripeCharge CreateStripeCharge(Order order)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<StripeCharge>>().Create();
            entity.Order = order;
            entity.Amount = order == null ? 0 : order.OriginalTotalAmount;
            entity.ChargeMethodType = this.DependencyResolver.Resolve<ISupportsGetById<ChargeMethodType, byte>>().Get((byte)ChargeMethodTypeEnum.Stripe);
            entity.ChargeStatusType = this.DependencyResolver.Resolve<ISupportsGetById<ChargeStatusType, byte>>().Get((byte)ChargeStatusTypeEnum.Succeeded);
            entity.ChargeType = this.DependencyResolver.Resolve<ISupportsGetById<ChargeType, byte>>().Get((byte)ChargeTypeEnum.AuthorizationAndCapture);
            entity.Date = DateTime.Now;
            entity.StripeChargeId = Guid.NewGuid().ToString();
            entity.StripeFee = 0;
            return entity;
        }

        public virtual StripeCustomerChargeOption CreateStripeCustomerChargeOption(Customer customer, Address billingAddress)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<StripeCustomerChargeOption>>().Create();
            entity.Customer = customer;
            entity.StripeCustomerId = Guid.NewGuid().ToString();
            entity.CreditCardFingerPrint = Guid.NewGuid().ToString();
            entity.Last4OfCreditCard = "4242";
            entity.ExpirationMonthOfCreditCard = "12";
            entity.ExpirationYearOfCreditCard = "2017";
            entity.BillingAddress = billingAddress;
            return entity;
        }

        public virtual PromoCode CreatePromoCode()
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<PromoCode>>().Create();
            entity.Code = Guid.NewGuid().ToString();
            return entity;
        }

        public virtual SubscriptionPeriod CreateSubscriptionPeriod()
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SubscriptionPeriod>>().Create();
            entity.ChargePeriod = 20;
            entity.ChargePeriodType = this.DependencyResolver.Resolve<ISupportsGetById<SubscriptionPeriodType, byte>>().Get((byte)SubscriptionPeriodTypeEnum.Day);
            entity.ShipmentPeriod = 10;
            entity.ShipmentPeriodType = this.DependencyResolver.Resolve<ISupportsGetById<SubscriptionPeriodType, byte>>().Get((byte)SubscriptionPeriodTypeEnum.Day);
            return entity;
        }

        public virtual Subscription CreateSubscription(SubscriptionOrderItem initialSubscriptionOrderItem, Address shippingAddress, CustomerChargeOption customerChargeOption)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<Subscription>>().Create();
            entity.StartDate = DateTime.Now;
            entity.ShippingAddress = shippingAddress;
            entity.Customer = customerChargeOption.Customer;
            entity.CustomerChargeOption = customerChargeOption;
            entity.NextChargeDate = initialSubscriptionOrderItem == null || initialSubscriptionOrderItem.SubscriptionPeriod == null
                                        ? DateTime.Now
                                        : initialSubscriptionOrderItem.SubscriptionPeriod.GetNextChargeDate(DateTime.Now);
            entity.NextShipmentDate = initialSubscriptionOrderItem == null || initialSubscriptionOrderItem.SubscriptionPeriod == null
                                        ? DateTime.Now
                                        : initialSubscriptionOrderItem.SubscriptionPeriod.GetNextShipmentDate(DateTime.Now);
            entity.StatusType = this.DependencyResolver.Resolve<ISupportsGetById<SubscriptionStatusType, byte>>().Get((byte)SubscriptionStatusTypeEnum.Active);
            entity.AutoRenew = true;
            return entity;
        }

        public virtual PendingSubscriptionChange CreatePendingSubscriptionChange(SubscriptionChoice subscriptionChoice)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<PendingSubscriptionChange>>().Create();
            entity.Quantity = 1;
            entity.SubscriptionChoice = subscriptionChoice;

            return entity;
        }

        public virtual SubscriptionInstance CreateSubscriptionInstance(Subscription subscription, SubscriptionOrderItem createdBySubscriptionOrderItem)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SubscriptionInstance>>().Create();
            entity.StartDate = DateTime.Now;
            entity.TotalShipments = 1;
            entity.ShipmentsRemaining = 1;
            entity.Subscription = subscription;
            entity.CreatedByOrderItem = createdBySubscriptionOrderItem;
            //subscription.CurrentSubscriptionInstance = entity;
            return entity;
        }

        public virtual Address CreateAddress(StateProvinceType stateProvince = null)
        {
            var address = this.DependencyResolver.Resolve<ISupportsCreate<Address>>().Create();
            address.FirstName = "Joe";
            address.LastName = "Smith";
            address.StateProvinceType = stateProvince;
            address.City = "Falls Church";
            address.PostalCode = "22042";
            address.CompanyName = "Joe's Burgers";
            address.StreetLine1 = "6637 Arlington Blvd.";
            address.PhoneNumber = "703.538.6926";
            return address;
        }

        public virtual Customer CreateCustomer(PromoCode promoCode, Visitor visitor = null, Address shippingAddress = null, CustomerChargeOption customerChargeOption = null)
        {
            visitor = visitor ?? this.GenerateVisitorWithUser();
            var customer = this.DependencyResolver.Resolve<ISupportsCreate<Customer>>().Create();
            customer.Visitor = visitor;
            customer.CurrentShippingAddress = shippingAddress;
            customer.PromoCode = promoCode;
            customer.CurrentCustomerChargeOption = customerChargeOption;

            return customer;
        }

        public virtual PartnerReferralPlan CreatePartnerReferralPlan(Discount discount)
        {
            var plan = this.DependencyResolver.Resolve<ISupportsCreate<PartnerReferralPlan>>().Create();
            plan.Name = System.Guid.NewGuid().ToString();
            plan.Amount = 1;
            plan.ReferredCustomerAwardDiscount = discount;

            return plan;
        }

        public virtual Partner CreatePartner(PartnerReferralPlan plan, Address address = null, Visitor visitor = null, PromoCode promoCode = null)
        {
            address = address ?? this.CreateAddress(this.GetExistingStateProvince());
            visitor = visitor ?? this.GenerateVisitorWithUser();

            var partner = this.DependencyResolver.Resolve<ISupportsCreate<Partner>>().Create();
            partner.Address = address;
            partner.PromoCode = promoCode;
            partner.Visitor = visitor;
            partner.ReferralPlan = plan;

            return partner;
        }

        public virtual StateProvinceType GetExistingStateProvince()
        {
            return this.DependencyResolver.Resolve<ISupportsQueryable<StateProvinceType>>().GetQueryable().First();
        }

        public virtual DisplayProduct CreateDisplayProduct()
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<DisplayProduct>>().Create();
            entity.AdminComment = System.Guid.NewGuid().ToString();
            entity.Name = System.Guid.NewGuid().ToString();
            return entity;
        }

        public virtual Visitor GenerateVisitor()
        {
            Visitor visitor = null;
            using (var uow = this.UnitOfWorkFactory.CreateRequired())
            {
                visitor = this.CreateVisitor();
                this.DependencyResolver.Resolve<ISupportsSave<Visitor>>().Save(visitor);

                uow.Commit();
            }
            return visitor;
        }

        public virtual Visitor CreateVisitor()
        {
            Visitor visitor = null;
            visitor = this.DependencyResolver.Resolve<ISupportsCreate<Visitor>>().Create();
            visitor.Token = Guid.NewGuid();
            visitor.LastActivityDate = DateTime.Now;
            return visitor;
        }

        public virtual Visitor GenerateVisitorWithUser()
        {
            var visitor = this.CreateVisitor();
            this.GenerateUser(visitor);

            return visitor;
        }

        public virtual User GenerateUser(Visitor visitor = null)
        {
            User user = null;

            using (var uow = this.UnitOfWorkFactory.CreateRequired())
            {
                CustomerRegistrationRequest request = new CustomerRegistrationRequest();
                // this wont create a customer, dont worry
                request.AcceptTermsOfUse = true;
                request.EmailAddress = this.GenerateEmail();
                request.Password = this.DependencyResolver.Resolve<IMembershipService>().GeneratePassword();

                this.DependencyResolver.Resolve<IMembershipService>().Register(request);

                user = (from o in this.DependencyResolver.Resolve<ISupportsQueryable<User>>().GetQueryable()
                        where o.UserName == request.EmailAddress
                        select o).Single();

                if (visitor != null)
                {
                    visitor.User = user;
                    this.DependencyResolver.Resolve<ISupportsSave<Visitor>>().Save(visitor);
                }

                uow.Commit();
            }
            return user;
        }


        public virtual String GenerateEmail()
        {
            return System.Guid.NewGuid().ToString() + "@zirpl.com";
        }

        public virtual SystemSetting CreateSystemSetting()
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SystemSetting>>().Create();
            entity.Name = Guid.NewGuid().ToString();
            entity.Value = "1";
            return entity;
        }

        public virtual EmailEvent CreateEmailEvent()
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<EmailEvent>>().Create();
            entity.Body = "test body";
            entity.EmailEventType = this.DependencyResolver.Resolve<IEmailEventTypeService>().Get(EmailEventTypeEnum.WelcomePartner);
            entity.SentDate = DateTime.Now;
            entity.Subject = "test subject";
            entity.To = this.GenerateEmail();
            entity.FromEmail = this.GenerateEmail();
            entity.SentSucceeded = true;
            return entity;
        }


        public virtual TaxRule CreateTaxRule(StateProvinceType stateProvince)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<TaxRule>>().Create();
            entity.Rate = (decimal)0.10;
            entity.StateProvinceType = stateProvince;
            entity.StateProvinceTypeId = stateProvince == null ? 0 : stateProvince.Id;
            return entity;
        }

        public virtual CustomerReferral CreateCustomerReferral(
            Customer referringCustomer,
            PromoCode promoCode,
            Discount referringCustomerAwardDiscount = null,
            DiscountUsage referringCustomerAwardDiscountUsage = null,
            Customer referredCustomer = null,
            Discount referredCustomerAwardDiscount = null,
            DiscountUsage referredCustomerAwardDiscountUsage = null)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<CustomerReferral>>().Create();
            entity.ReferringCustomer = referringCustomer;
            entity.PromoCode = promoCode;
            entity.ReferredCustomer = referredCustomer;
            entity.ReferredCustomerAwardDiscount = referredCustomerAwardDiscount;
            entity.ReferredCustomerAwardDiscountUsage = referredCustomerAwardDiscountUsage;
            entity.ReferringCustomerDiscountAward = referringCustomerAwardDiscount;
            entity.ReferringCustomerDiscountAwardUsage = referringCustomerAwardDiscountUsage;
            entity.ReferredCustomerJoinedDate = referredCustomer == null ? null : new Nullable<DateTime>(DateTime.Now);
            return entity;
        }

        public virtual PartnerReferral CreatePartnerReferral(
            Partner partner,
            PartnerReferralCouponRequest request,
            PromoCode promoCode,
            Customer referredCustomer = null,
            Discount referredCustomerAwardDiscount = null,
            DiscountUsage referredCustomerAwardDiscountUsage = null)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<PartnerReferral>>().Create();
            entity.Partner = partner;
            entity.Request = request;
            entity.PromoCode = promoCode;
            entity.ReferredCustomer = referredCustomer;
            entity.ReferredCustomerAwardDiscount = referredCustomerAwardDiscount;
            entity.ReferredCustomerAwardDiscountUsage = referredCustomerAwardDiscountUsage;
            entity.ReferredCustomerJoinedDate = referredCustomer == null ? null : new Nullable<DateTime>(DateTime.Now);
            return entity;
        }

        public virtual PartnerReferralCouponRequest CreatePartnerReferralCouponRequest(Partner partner)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<PartnerReferralCouponRequest>>().Create();
            entity.PartnerReferralCouponRequestStatusType = this.DependencyResolver.Resolve<IPartnerReferralCouponRequestStatusTypeService>().Get(PartnerReferralCouponRequestStatusTypeEnum.Open);
            entity.Partner = partner;
            entity.PartnerId = partner == null ? 0 : partner.Id;
            entity.RequestDate = DateTime.Now;
            entity.Quantity = 25;

            return entity;
        }

        public virtual PartnerRegistrationRequest CreatePartnerRegistrationRequest(int? stateProvinceId, int? referralPlanId)
        {
            PartnerRegistrationRequest request = new PartnerRegistrationRequest();
            //request.FirstName = "Nathan";
            //request.LastName = "LaFratta";
            //request.StreetLine1 = "101 Main Street";
            //request.StreetLine2 = "Apt 2B";
            //request.City = "New York";
            //request.StateProvinceTypeId = stateProvinceId;
            //request.PostalCode = "11111";
            //request.PhoneNumber = "585-455-1111";
            //request.CompanyName = "Zirpl Software";
            //request.ReferralPlanId = referralPlanId;
            request.EmailAddress = this.GenerateEmail();
            request.Password = this.DependencyResolver.Resolve<IMembershipService>().GeneratePassword();
            return request;
        }

        public virtual CustomerRegistrationRequest CreateCustomerRegistrationRequest()
        {
            var request = new CustomerRegistrationRequest();
            request.AcceptTermsOfUse = true;
            request.EmailAddress = this.GenerateEmail();
            request.Password = this.DependencyResolver.Resolve<IMembershipService>().GeneratePassword();
            return request;
        }

        public virtual TierPrice CreateTierPrice(DisplayProduct displayProduct)
        {
            var tierPrice = this.DependencyResolver.Resolve<ISupportsCreate<TierPrice>>().Create();
            tierPrice.DisplayProduct = displayProduct;
            tierPrice.DisplayProductId = displayProduct == null ? 0 : displayProduct.Id;
            tierPrice.Quantity = 10;
            tierPrice.PriceEach = 3;
            return tierPrice;
        }

        public virtual TierShipment CreateTierShipment(DisplayProduct displayProduct)
        {
            var tierShipment = this.DependencyResolver.Resolve<ISupportsCreate<TierShipment>>().Create();
            tierShipment.DisplayProduct = displayProduct;
            tierShipment.DisplayProductId = displayProduct == null ? 0 : displayProduct.Id;
            tierShipment.Quantity = 1;
            tierShipment.BaseWeightInOunces = 1.200m;
            tierShipment.WeightInOuncesEach = 1.000m;

            return tierShipment;
        }

        public virtual SubscriptionChoiceTierPriceCombination CreateSubscriptionChoiceTierPriceCombination(DisplayProduct displayProduct, SubscriptionChoice subscriptionChoice, TierPrice tierPrice)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SubscriptionChoiceTierPriceCombination>>().Create();
            entity.PriceEach = 5;
            entity.DisplayProduct = displayProduct;
            entity.DisplayProductId = displayProduct == null ? 0 : displayProduct.Id;
            entity.SubscriptionChoice = subscriptionChoice;
            entity.SubscriptionChoiceId = subscriptionChoice == null ? null : new Nullable<int>(subscriptionChoice.Id);
            entity.TierPrice = tierPrice;
            entity.TierPriceId = tierPrice == null ? null : new Nullable<int>(tierPrice.Id);
            return entity;
        }

        public virtual SubscriptionChoice CreateSubscriptionChoice(DisplayProduct displayProduct, SubscriptionPeriod subscriptionPeriod)
        {
            var entity = this.DependencyResolver.Resolve<ISupportsCreate<SubscriptionChoice>>().Create();
            entity.SubscriptionPeriod = subscriptionPeriod;
            entity.SubscriptionPeriodId = subscriptionPeriod == null ? 0 : subscriptionPeriod.Id;
            entity.DisplayProduct = displayProduct;
            entity.DisplayProductId = displayProduct == null ? 0 : displayProduct.Id;
            entity.PriceEach = 1;
            return entity;
        }
    }
}
