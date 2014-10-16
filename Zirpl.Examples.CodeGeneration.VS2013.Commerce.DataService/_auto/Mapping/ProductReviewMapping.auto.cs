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
                                        ProductReviewMetadataConstants.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.ReviewerName).IsRequired(ProductReviewMetadataConstants.ReviewerName_IsRequired).HasMaxLength(ProductReviewMetadataConstants.ReviewerName_MaxLength, ProductReviewMetadataConstants.ReviewerName_IsMaxLength);
			this.Property(o => o.ReviewerLocation).IsRequired(ProductReviewMetadataConstants.ReviewerLocation_IsRequired).HasMaxLength(ProductReviewMetadataConstants.ReviewerLocation_MaxLength, ProductReviewMetadataConstants.ReviewerLocation_IsMaxLength);
			this.Property(o => o.Date).IsRequired(ProductReviewMetadataConstants.Date_IsRequired).IsDateTime();
			this.Property(o => o.Text).IsRequired(ProductReviewMetadataConstants.Text_IsRequired).HasMaxLength(ProductReviewMetadataConstants.Text_MaxLength, ProductReviewMetadataConstants.Text_IsMaxLength);
			this.Property(o => o.Stars).IsRequired(ProductReviewMetadataConstants.Stars_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
