
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class SubscriptionChoiceValidator  : DbEntityValidatorBase<SubscriptionChoice>
		
    {
        public SubscriptionChoiceValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                SubscriptionChoiceMetadata.DisplayProduct_Name, SubscriptionChoiceMetadata.DisplayProductId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.SubscriptionPeriod, o => o.SubscriptionPeriodId,
                SubscriptionChoiceMetadata.SubscriptionPeriod_Name, SubscriptionChoiceMetadata.SubscriptionPeriodId_Name);
			this.RuleFor(o => o.PriceEach).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionChoiceMetadata.PriceEach_MinValue, SubscriptionChoiceMetadata.PriceEach_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

