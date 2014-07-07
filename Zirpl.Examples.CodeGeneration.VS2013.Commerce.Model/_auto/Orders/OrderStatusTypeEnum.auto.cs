using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public enum OrderStatusTypeEnum : byte
    {
        PaymentProcessing = 1,
        PaymentFailed = 2,
        ShipmentPending = 3,
        ShipmentProcessing = 4,
        Shipped = 5,
        Cancelled = 6,
        CompleteNoShipment = 7
	}
}
