﻿using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class ChargeMethodTypeMapping : DictionaryEntityMapping<ChargeMethodType, byte, ChargeMethodTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(ChargeMethodTypeMetadataConstants.Name_IsRequired).HasMaxLength(ChargeMethodTypeMetadataConstants.Name_MaxLength, ChargeMethodTypeMetadataConstants.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
