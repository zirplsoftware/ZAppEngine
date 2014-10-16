
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class ProductReviewValidator  : DbEntityValidatorBase<ProductReview>
		
    {
        public ProductReviewValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                ProductReviewMetadataConstants.DisplayProduct_Name, ProductReviewMetadataConstants.DisplayProductId_Name);
			this.RuleFor(o => o.ReviewerName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(ProductReviewMetadataConstants.ReviewerName_MinLength, ProductReviewMetadataConstants.ReviewerName_MaxLength);
			this.RuleFor(o => o.ReviewerLocation).Length(ProductReviewMetadataConstants.ReviewerLocation_MinLength, ProductReviewMetadataConstants.ReviewerLocation_MaxLength);
			this.RuleFor(o => o.Date).NotEmpty();
			this.RuleFor(o => o.Text).Length(ProductReviewMetadataConstants.Text_MinLength, ProductReviewMetadataConstants.Text_MaxLength);
			this.RuleFor(o => o.Stars).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(ProductReviewMetadataConstants.Stars_MinValue, ProductReviewMetadataConstants.Stars_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

