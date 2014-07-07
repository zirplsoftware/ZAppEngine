using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class ProductReviewMapping : CoreEntityMappingBase<ProductReview, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        ProductReviewMetadata.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.ReviewerName).IsRequired(ProductReviewMetadata.ReviewerName_IsRequired).HasMaxLength(ProductReviewMetadata.ReviewerName_MaxLength, ProductReviewMetadata.ReviewerName_IsMaxLength);
			this.Property(o => o.ReviewerLocation).IsRequired(ProductReviewMetadata.ReviewerLocation_IsRequired).HasMaxLength(ProductReviewMetadata.ReviewerLocation_MaxLength, ProductReviewMetadata.ReviewerLocation_IsMaxLength);
			this.Property(o => o.Date).IsRequired(ProductReviewMetadata.Date_IsRequired).IsDateTime();
			this.Property(o => o.Text).IsRequired(ProductReviewMetadata.Text_IsRequired).HasMaxLength(ProductReviewMetadata.Text_MaxLength, ProductReviewMetadata.Text_IsMaxLength);
			this.Property(o => o.Stars).IsRequired(ProductReviewMetadata.Stars_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
