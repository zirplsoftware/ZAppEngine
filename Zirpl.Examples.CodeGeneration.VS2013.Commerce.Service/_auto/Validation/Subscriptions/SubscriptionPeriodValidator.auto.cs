
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Subscriptions
{
    public partial class SubscriptionPeriodValidator  : DbEntityValidatorBase<SubscriptionPeriod>
		
    {
        public SubscriptionPeriodValidator()
        {
			this.RuleFor(o => o.ChargePeriod).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionPeriodMetadata.ChargePeriod_MinValue, SubscriptionPeriodMetadata.ChargePeriod_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargePeriodType, o => o.ChargePeriodTypeId,
                SubscriptionPeriodMetadata.ChargePeriodType_Name, SubscriptionPeriodMetadata.ChargePeriodTypeId_Name);
			this.RuleFor(o => o.ShipmentPeriod).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionPeriodMetadata.ShipmentPeriod_MinValue, SubscriptionPeriodMetadata.ShipmentPeriod_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ShipmentPeriodType, o => o.ShipmentPeriodTypeId,
                SubscriptionPeriodMetadata.ShipmentPeriodType_Name, SubscriptionPeriodMetadata.ShipmentPeriodTypeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

