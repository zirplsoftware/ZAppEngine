using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public enum OrderChargeStatusTypeEnum : byte
    {
        Processing = 1,
        Paid = 2,
        PartiallyRefunded = 3,
        Refunded = 4,
        Failed = 5
	}
}
