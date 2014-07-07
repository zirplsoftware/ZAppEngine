﻿using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class SubscriptionOrderItemMapping : CoreEntityMappingBase<SubscriptionOrderItem, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.SubscriptionOrderItemType,
                                        o => o.SubscriptionOrderItemTypeId,
                                        SubscriptionOrderItemMetadata.SubscriptionOrderItemType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.SubscriptionPeriod,
                                        o => o.SubscriptionPeriodId,
                                        SubscriptionOrderItemMetadata.SubscriptionPeriod_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.TriggeredBySubscriptionInstance,
                                        o => o.TriggeredBySubscriptionInstanceId,
                                        SubscriptionOrderItemMetadata.TriggeredBySubscriptionInstance_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
        protected override bool MapCoreEntityBaseProperties
        {
            get
            {
                return false;
            }
        }
    }
}
