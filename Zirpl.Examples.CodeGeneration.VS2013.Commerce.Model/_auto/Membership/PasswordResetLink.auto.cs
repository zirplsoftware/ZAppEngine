using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class PasswordResetLink  : EntityBase<int>
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.User User { get; set; }
		public virtual Guid UserId { get; set; }
		public virtual Guid Token { get; set; }
		public virtual DateTime Expires { get; set; }
    }
}

