
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class SubscriptionChoiceTierPriceCombinationValidator  : DbEntityValidatorBase<SubscriptionChoiceTierPriceCombination>
		
    {
        public SubscriptionChoiceTierPriceCombinationValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                SubscriptionChoiceTierPriceCombinationMetadata.DisplayProduct_Name, SubscriptionChoiceTierPriceCombinationMetadata.DisplayProductId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.SubscriptionChoice, o => o.SubscriptionChoiceId,
                SubscriptionChoiceTierPriceCombinationMetadata.SubscriptionChoice_Name, SubscriptionChoiceTierPriceCombinationMetadata.SubscriptionChoiceId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.TierPrice, o => o.TierPriceId,
                SubscriptionChoiceTierPriceCombinationMetadata.TierPrice_Name, SubscriptionChoiceTierPriceCombinationMetadata.TierPriceId_Name);
			this.RuleFor(o => o.PriceEach).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(SubscriptionChoiceTierPriceCombinationMetadata.PriceEach_MinValue, SubscriptionChoiceTierPriceCombinationMetadata.PriceEach_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

