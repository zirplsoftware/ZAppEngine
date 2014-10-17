
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class SubscriptionChoiceTierPriceCombinationValidator  : DbEntityValidatorBase<SubscriptionChoiceTierPriceCombination>
		
    {
        public SubscriptionChoiceTierPriceCombinationValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                SubscriptionChoiceTierPriceCombinationMetadataConstants.DisplayProduct_Name, SubscriptionChoiceTierPriceCombinationMetadataConstants.DisplayProductId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.SubscriptionChoice, o => o.SubscriptionChoiceId,
                SubscriptionChoiceTierPriceCombinationMetadataConstants.SubscriptionChoice_Name, SubscriptionChoiceTierPriceCombinationMetadataConstants.SubscriptionChoiceId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.TierPrice, o => o.TierPriceId,
                SubscriptionChoiceTierPriceCombinationMetadataConstants.TierPrice_Name, SubscriptionChoiceTierPriceCombinationMetadataConstants.TierPriceId_Name);
			this.RuleFor(o => o.PriceEach).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionChoiceTierPriceCombinationMetadataConstants.PriceEach_MinValue, SubscriptionChoiceTierPriceCombinationMetadataConstants.PriceEach_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

