
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
                ProductReviewMetadata.DisplayProduct_Name, ProductReviewMetadata.DisplayProductId_Name);
			this.RuleFor(o => o.ReviewerName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(ProductReviewMetadata.ReviewerName_MinLength, ProductReviewMetadata.ReviewerName_MaxLength);
			this.RuleFor(o => o.ReviewerLocation).Length(ProductReviewMetadata.ReviewerLocation_MinLength, ProductReviewMetadata.ReviewerLocation_MaxLength);
			this.RuleFor(o => o.Date).NotEmpty();
			this.RuleFor(o => o.Text).Length(ProductReviewMetadata.Text_MinLength, ProductReviewMetadata.Text_MaxLength);
			this.RuleFor(o => o.Stars).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(ProductReviewMetadata.Stars_MinValue, ProductReviewMetadata.Stars_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

