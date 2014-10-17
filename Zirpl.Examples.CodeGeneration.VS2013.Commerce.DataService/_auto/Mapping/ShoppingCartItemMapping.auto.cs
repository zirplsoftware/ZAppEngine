using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class ShoppingCartItemMapping : CoreEntityMappingBase<ShoppingCartItem, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Quantity).IsRequired(ShoppingCartItemMetadataConstants.Quantity_IsRequired);

            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        ShoppingCartItemMetadataConstants.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Visitor,
                                        o => o.VisitorId,
                                        ShoppingCartItemMetadataConstants.Visitor_IsRequired,
                                        CascadeOnDeleteOption.No,
										o => o.ShoppingCartItems);

            this.HasNavigationProperty(o => o.SubscriptionChoice,
                                        o => o.SubscriptionChoiceId,
                                        ShoppingCartItemMetadataConstants.SubscriptionChoice_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.AddedWhileAnonymous).IsRequired(ShoppingCartItemMetadataConstants.AddedWhileAnonymous_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
