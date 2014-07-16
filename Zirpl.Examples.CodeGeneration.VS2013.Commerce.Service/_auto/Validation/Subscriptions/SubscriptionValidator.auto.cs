
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Subscriptions
{
    public partial class SubscriptionValidator  : DbEntityValidatorBase<Subscription>
		
    {
        public SubscriptionValidator()
        {
			this.RuleFor(o => o.StartDate).NotEmpty();
            this.ForeignEntityNotNullAndIdMatches(o => o.StatusType, o => o.StatusTypeId,
                SubscriptionMetadata.StatusType_Name, SubscriptionMetadata.StatusTypeId_Name);
			this.RuleFor(o => o.NextShipmentDate).NotEmpty();
			this.RuleFor(o => o.NextChargeDate).NotEmpty();
			this.RuleFor(o => o.AutoRenew).NotNull();
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                SubscriptionMetadata.Customer_Name, SubscriptionMetadata.CustomerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.ShippingAddress, o => o.ShippingAddressId,
                SubscriptionMetadata.ShippingAddress_Name, SubscriptionMetadata.ShippingAddressId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.CustomerChargeOption, o => o.CustomerChargeOptionId,
                SubscriptionMetadata.CustomerChargeOption_Name, SubscriptionMetadata.CustomerChargeOptionId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.CurrentSubscriptionInstance, o => o.CurrentSubscriptionInstanceId,
                SubscriptionMetadata.CurrentSubscriptionInstance_Name, SubscriptionMetadata.CurrentSubscriptionInstanceId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

