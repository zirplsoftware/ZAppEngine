﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class PartnerRegistrationRequest  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.UserRegistrationRequestBase
    {
		public virtual bool AcceptTermsOfUse { get; set; }
    }
}

