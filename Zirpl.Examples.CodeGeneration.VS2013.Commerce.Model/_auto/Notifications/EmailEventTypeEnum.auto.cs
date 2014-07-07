using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications
{
    public enum EmailEventTypeEnum : byte
    {
        WelcomePartner = 1,
        WelcomeCustomer = 2,
        InitialSubscriptionOrderPlaced = 3,
        PasswordReset = 4,
        CustomerContactUs = 5
	}
}
