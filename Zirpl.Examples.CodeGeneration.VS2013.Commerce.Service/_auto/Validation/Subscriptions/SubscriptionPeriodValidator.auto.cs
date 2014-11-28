
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Subscriptions
{
    public partial class SubscriptionPeriodValidator  : DbEntityValidatorBase<SubscriptionPeriod>
		
    {
        public SubscriptionPeriodValidator()
        {
			this.RuleFor(o => o.ChargePeriod).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionPeriodMetadataConstants.ChargePeriod_MinValue, SubscriptionPeriodMetadataConstants.ChargePeriod_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ChargePeriodType, o => o.ChargePeriodTypeId,
                SubscriptionPeriodMetadataConstants.ChargePeriodType_Name, SubscriptionPeriodMetadataConstants.ChargePeriodTypeId_Name);
			this.RuleFor(o => o.ShipmentPeriod).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionPeriodMetadataConstants.ShipmentPeriod_MinValue, SubscriptionPeriodMetadataConstants.ShipmentPeriod_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ShipmentPeriodType, o => o.ShipmentPeriodTypeId,
                SubscriptionPeriodMetadataConstants.ShipmentPeriodType_Name, SubscriptionPeriodMetadataConstants.ShipmentPeriodTypeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

