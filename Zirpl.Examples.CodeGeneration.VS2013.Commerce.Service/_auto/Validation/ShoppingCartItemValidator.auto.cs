
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class ShoppingCartItemValidator  : DbEntityValidatorBase<ShoppingCartItem>
		
    {
        public ShoppingCartItemValidator()
        {
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(ShoppingCartItemMetadata.Quantity_MinValue, ShoppingCartItemMetadata.Quantity_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                ShoppingCartItemMetadata.DisplayProduct_Name, ShoppingCartItemMetadata.DisplayProductId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Visitor, o => o.VisitorId,
                ShoppingCartItemMetadata.Visitor_Name, ShoppingCartItemMetadata.VisitorId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.SubscriptionChoice, o => o.SubscriptionChoiceId,
                ShoppingCartItemMetadata.SubscriptionChoice_Name, ShoppingCartItemMetadata.SubscriptionChoiceId_Name);
			this.RuleFor(o => o.AddedWhileAnonymous).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

