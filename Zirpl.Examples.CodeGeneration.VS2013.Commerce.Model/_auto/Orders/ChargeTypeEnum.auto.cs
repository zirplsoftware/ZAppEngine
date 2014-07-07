using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public enum ChargeTypeEnum : byte
    {
        Authorization = 1,
        AuthorizationAndCapture = 2,
        Capture = 3,
        Refund = 4
	}
}
