using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class StripeCharge : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.Charge
    {
		public virtual string StripeChargeId { get; set; }
		public virtual decimal StripeFee { get; set; }
    }
}

