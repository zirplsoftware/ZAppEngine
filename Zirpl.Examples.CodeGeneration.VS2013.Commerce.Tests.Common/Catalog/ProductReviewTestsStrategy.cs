using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class ProductReviewEntityWrapper
    {
        public DisplayProduct DisplayProduct;
    }

    public partial class ProductReviewTestsStrategy
    {
        public virtual void SetUpWrapper(ProductReviewEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(ProductReviewEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateProductReview(this.Data.CreateDisplayProduct());
            wrapper.DisplayProduct = wrapper.Entity.DisplayProduct;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(ProductReviewEntityWrapper entityWrapper, ProductReview entity, ProductReview entityFromDb)
        {
            entityFromDb.Date.Should().Be(entity.Date);
            entityFromDb.ReviewerLocation.Should().Be(entity.ReviewerLocation);
            entityFromDb.ReviewerName.Should().Be(entity.ReviewerName);
            entityFromDb.Stars.Should().Be(entity.Stars);
            entityFromDb.Text.Should().Be(entity.Text);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
        }

        public virtual void OnAssertExpectationsAfterInsert(ProductReviewEntityWrapper entityWrapper, ProductReview entity, ProductReview entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(ProductReviewEntityWrapper entityWrapper, ProductReview entity, ProductReview entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(ProductReviewEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(ProductReviewEntityWrapper entityWrapper, ProductReview entity)
        {
            entity.Stars += 1;
        }

        public virtual void ChangePropertyValuesToFailValidation(ProductReviewEntityWrapper entityWrapper, ProductReview entity)
        {
            entity.Stars = -1;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
