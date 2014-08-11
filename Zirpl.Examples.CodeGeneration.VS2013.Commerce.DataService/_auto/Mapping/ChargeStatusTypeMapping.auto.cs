﻿using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class ChargeStatusTypeMapping : DictionaryEntityMapping<ChargeStatusType, byte, ChargeStatusTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(ChargeStatusTypeMetadata.Name_IsRequired).HasMaxLength(ChargeStatusTypeMetadata.Name_MaxLength, ChargeStatusTypeMetadata.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}