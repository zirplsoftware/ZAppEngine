
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Subscriptions
{
    public partial class SubscriptionValidator  : DbEntityValidatorBase<Subscription>
		
    {
        public SubscriptionValidator()
        {
			this.RuleFor(o => o.StartDate).NotEmpty();
            this.ForeignEntityNotNullAndIdMatches(o => o.StatusType, o => o.StatusTypeId,
                SubscriptionMetadataConstants.StatusType_Name, SubscriptionMetadataConstants.StatusTypeId_Name);
			this.RuleFor(o => o.NextShipmentDate).NotEmpty();
			this.RuleFor(o => o.NextChargeDate).NotEmpty();
			this.RuleFor(o => o.AutoRenew).NotNull();
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                SubscriptionMetadataConstants.Customer_Name, SubscriptionMetadataConstants.CustomerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.ShippingAddress, o => o.ShippingAddressId,
                SubscriptionMetadataConstants.ShippingAddress_Name, SubscriptionMetadataConstants.ShippingAddressId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.CustomerChargeOption, o => o.CustomerChargeOptionId,
                SubscriptionMetadataConstants.CustomerChargeOption_Name, SubscriptionMetadataConstants.CustomerChargeOptionId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.CurrentSubscriptionInstance, o => o.CurrentSubscriptionInstanceId,
                SubscriptionMetadataConstants.CurrentSubscriptionInstance_Name, SubscriptionMetadataConstants.CurrentSubscriptionInstanceId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

