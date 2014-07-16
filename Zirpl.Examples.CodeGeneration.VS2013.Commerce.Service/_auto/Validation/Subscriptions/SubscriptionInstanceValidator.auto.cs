
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
			this.RuleFor(o => o.TotalShipments).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionInstanceMetadata.TotalShipments_MinValue, SubscriptionInstanceMetadata.TotalShipments_MaxValue);
			this.RuleFor(o => o.ShipmentsRemaining).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionInstanceMetadata.ShipmentsRemaining_MinValue, SubscriptionInstanceMetadata.ShipmentsRemaining_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.Subscription, o => o.SubscriptionId,
                SubscriptionInstanceMetadata.Subscription_Name, SubscriptionInstanceMetadata.SubscriptionId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.CreatedByOrderItem, o => o.CreatedByOrderItemId,
                SubscriptionInstanceMetadata.CreatedByOrderItem_Name, SubscriptionInstanceMetadata.CreatedByOrderItemId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.PendingSubscriptionChange, o => o.PendingSubscriptionChangeId,
                SubscriptionInstanceMetadata.PendingSubscriptionChange_Name, SubscriptionInstanceMetadata.PendingSubscriptionChangeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

