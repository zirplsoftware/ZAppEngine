
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Subscriptions
{
    public partial class PendingSubscriptionChangeValidator  : DbEntityValidatorBase<PendingSubscriptionChange>
		
    {
        public PendingSubscriptionChangeValidator()
        {
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(PendingSubscriptionChangeMetadataConstants.Quantity_MinValue, PendingSubscriptionChangeMetadataConstants.Quantity_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.SubscriptionChoice, o => o.SubscriptionChoiceId,
                PendingSubscriptionChangeMetadataConstants.SubscriptionChoice_Name, PendingSubscriptionChangeMetadataConstants.SubscriptionChoiceId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

