﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class TierShipment  : EntityBase<int>
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct DisplayProduct { get; set; }
		public virtual int DisplayProductId { get; set; }
		public virtual int Quantity { get; set; }
		public virtual decimal BaseWeightInOunces { get; set; }
		public virtual decimal WeightInOuncesEach { get; set; }
		public virtual bool RequiresManualShipmentHandling { get; set; }
    }
}

