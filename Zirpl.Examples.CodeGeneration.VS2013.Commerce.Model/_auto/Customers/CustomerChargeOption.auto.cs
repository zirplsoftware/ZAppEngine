using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers
{
    public abstract partial class CustomerChargeOption  : AuditableBase<int>
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.Customer Customer { get; set; }
		public virtual int CustomerId { get; set; }
    }
}

