
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class ShoppingCartItemValidator  : DbEntityValidatorBase<ShoppingCartItem>
		
    {
        public ShoppingCartItemValidator()
        {
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(ShoppingCartItemMetadataConstants.Quantity_MinValue, ShoppingCartItemMetadataConstants.Quantity_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.DisplayProduct, o => o.DisplayProductId,
                ShoppingCartItemMetadataConstants.DisplayProduct_Name, ShoppingCartItemMetadataConstants.DisplayProductId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Visitor, o => o.VisitorId,
                ShoppingCartItemMetadataConstants.Visitor_Name, ShoppingCartItemMetadataConstants.VisitorId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.SubscriptionChoice, o => o.SubscriptionChoiceId,
                ShoppingCartItemMetadataConstants.SubscriptionChoice_Name, ShoppingCartItemMetadataConstants.SubscriptionChoiceId_Name);
			this.RuleFor(o => o.AddedWhileAnonymous).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

