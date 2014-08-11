﻿using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class PasswordResetLink  : AuditableBase<int>
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.User User { get; set; }
		public virtual Guid UserId { get; set; }
		public virtual Guid Token { get; set; }
		public virtual DateTime Expires { get; set; }
    }
}
