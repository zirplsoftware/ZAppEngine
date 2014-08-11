using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers
{
    public partial class CustomerRegistrationRequest  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.UserRegistrationRequestBase
    {
		public virtual bool AcceptTermsOfUse { get; set; }
    }
}

