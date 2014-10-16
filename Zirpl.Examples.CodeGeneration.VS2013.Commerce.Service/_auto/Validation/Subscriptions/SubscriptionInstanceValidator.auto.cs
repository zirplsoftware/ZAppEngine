
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Subscriptions
{
    public partial class SubscriptionInstanceValidator  : DbEntityValidatorBase<SubscriptionInstance>
		
    {
        public SubscriptionInstanceValidator()
        {
			this.RuleFor(o => o.StartDate).NotEmpty();
			this.RuleFor(o => o.TotalShipments).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionInstanceMetadataConstants.TotalShipments_MinValue, SubscriptionInstanceMetadataConstants.TotalShipments_MaxValue);
			this.RuleFor(o => o.ShipmentsRemaining).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionInstanceMetadataConstants.ShipmentsRemaining_MinValue, SubscriptionInstanceMetadataConstants.ShipmentsRemaining_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.Subscription, o => o.SubscriptionId,
                SubscriptionInstanceMetadataConstants.Subscription_Name, SubscriptionInstanceMetadataConstants.SubscriptionId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.CreatedByOrderItem, o => o.CreatedByOrderItemId,
                SubscriptionInstanceMetadataConstants.CreatedByOrderItem_Name, SubscriptionInstanceMetadataConstants.CreatedByOrderItemId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.PendingSubscriptionChange, o => o.PendingSubscriptionChangeId,
                SubscriptionInstanceMetadataConstants.PendingSubscriptionChange_Name, SubscriptionInstanceMetadataConstants.PendingSubscriptionChangeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

