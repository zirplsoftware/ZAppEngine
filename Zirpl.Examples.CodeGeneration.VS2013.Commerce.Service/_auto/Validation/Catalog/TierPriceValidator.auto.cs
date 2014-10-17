
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class TierPriceValidator  : DbEntityValidatorBase<TierPrice>
		
    {
        public TierPriceValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                TierPriceMetadataConstants.DisplayProduct_Name, TierPriceMetadataConstants.DisplayProductId_Name);
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierPriceMetadataConstants.Quantity_MinValue, TierPriceMetadataConstants.Quantity_MaxValue);
			this.RuleFor(o => o.PriceEach).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TierPriceMetadataConstants.PriceEach_MinValue, TierPriceMetadataConstants.PriceEach_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

