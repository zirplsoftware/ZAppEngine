using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class LoginRequest 
    {
		public virtual string EmailAddress { get; set; }
		public virtual string Password { get; set; }
		public virtual bool RememberMe { get; set; }
    }
}

