using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class ShoppingCartItemMapping : CoreEntityMappingBase<ShoppingCartItem, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Quantity).IsRequired(ShoppingCartItemMetadata.Quantity_IsRequired);

            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        ShoppingCartItemMetadata.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Visitor,
                                        o => o.VisitorId,
                                        ShoppingCartItemMetadata.Visitor_IsRequired,
                                        CascadeOnDeleteOption.No,
										o => o.ShoppingCartItems);

            this.HasNavigationProperty(o => o.SubscriptionChoice,
                                        o => o.SubscriptionChoiceId,
                                        ShoppingCartItemMetadata.SubscriptionChoice_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.AddedWhileAnonymous).IsRequired(ShoppingCartItemMetadata.AddedWhileAnonymous_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
