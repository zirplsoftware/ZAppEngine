using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class ShoppingCartItemEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public SubscriptionChoice SubscriptionChoice { get; set; }
        public DisplayProduct DisplayProduct { get; set; }
    }

    public partial class ShoppingCartItemTestsStrategy
    {
        public virtual void SetUpWrapper(ShoppingCartItemEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(ShoppingCartItemEntityWrapper wrapper)
        {
            wrapper.DisplayProduct = this.Data.CreateDisplayProduct();
            wrapper.Entity = this.Data.CreateShoppingCartItem(wrapper.Visitor, wrapper.DisplayProduct);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(ShoppingCartItemEntityWrapper entityWrapper, ShoppingCartItem entity, ShoppingCartItem entityFromDb)
        {
            entityFromDb.Quantity.Should().Be(entity.Quantity);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Visitor, o => o.VisitorId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
            entityFromDb.Visitor.Id.Should().Be(entityWrapper.Visitor.Id);
            entityFromDb.DisplayProduct.Id.Should().Be(entityWrapper.DisplayProduct.Id);
        }

        public virtual void OnAssertExpectationsAfterInsert(ShoppingCartItemEntityWrapper entityWrapper, ShoppingCartItem entity, ShoppingCartItem entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(ShoppingCartItemEntityWrapper entityWrapper, ShoppingCartItem entity, ShoppingCartItem entityFromDb)
        {
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.SubscriptionChoice, o => o.SubscriptionChoiceId);
            entityFromDb.SubscriptionChoice.Id.Should().Be(entityWrapper.SubscriptionChoice.Id);
            entityFromDb.AddedWhileAnonymous.Should().BeTrue();
        }

        public virtual void OnAssertExpectationsAfterDelete(ShoppingCartItemEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IVisitorDataService>().Get(entityWrapper.Visitor.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<ISubscriptionChoiceDataService>().Get(entityWrapper.SubscriptionChoice.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(ShoppingCartItemEntityWrapper entityWrapper, ShoppingCartItem entity)
        {
            entity.Quantity += entity.Quantity;
            entity.SubscriptionChoice = this.Data.CreateSubscriptionChoice(entityWrapper.DisplayProduct, this.Data.CreateSubscriptionPeriod());
            entityWrapper.SubscriptionChoice = entity.SubscriptionChoice;
            entity.AddedWhileAnonymous = true;

        }

        public virtual void ChangePropertyValuesToFailValidation(ShoppingCartItemEntityWrapper entityWrapper, ShoppingCartItem entity)
        {
            entity.Quantity = -1;
        }
    }
}
