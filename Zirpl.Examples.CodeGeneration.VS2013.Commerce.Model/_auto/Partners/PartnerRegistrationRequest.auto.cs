﻿using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class PartnerRegistrationRequest  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.UserRegistrationRequestBase
    {
		public virtual bool AcceptTermsOfUse { get; set; }
    }
}
