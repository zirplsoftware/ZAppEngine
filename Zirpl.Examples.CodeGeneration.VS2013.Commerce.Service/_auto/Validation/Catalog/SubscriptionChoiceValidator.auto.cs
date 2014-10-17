
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class SubscriptionChoiceValidator  : DbEntityValidatorBase<SubscriptionChoice>
		
    {
        public SubscriptionChoiceValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                SubscriptionChoiceMetadataConstants.DisplayProduct_Name, SubscriptionChoiceMetadataConstants.DisplayProductId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.SubscriptionPeriod, o => o.SubscriptionPeriodId,
                SubscriptionChoiceMetadataConstants.SubscriptionPeriod_Name, SubscriptionChoiceMetadataConstants.SubscriptionPeriodId_Name);
			this.RuleFor(o => o.PriceEach).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionChoiceMetadataConstants.PriceEach_MinValue, SubscriptionChoiceMetadataConstants.PriceEach_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

