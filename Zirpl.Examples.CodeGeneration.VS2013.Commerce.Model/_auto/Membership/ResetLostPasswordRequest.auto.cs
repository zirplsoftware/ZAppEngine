using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class ResetLostPasswordRequest 
    {
		public virtual string NewPassword { get; set; }
		public virtual Guid UserId { get; set; }
    }
}

