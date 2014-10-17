using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class ResetLostPasswordRequest 
    {
		public virtual string NewPassword { get; set; }
		public virtual Guid UserId { get; set; }
    }
}

