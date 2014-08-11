using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class LoginRequest 
    {
		public virtual string EmailAddress { get; set; }
		public virtual string Password { get; set; }
		public virtual bool RememberMe { get; set; }
    }
}

