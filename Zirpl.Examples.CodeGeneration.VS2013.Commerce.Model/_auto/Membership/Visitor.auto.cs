using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public partial class Visitor  : AuditableBase<int>
    {
		public Visitor()
		{
			if (this.ShoppingCartItems == null)
			{
				this.ShoppingCartItems = new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.ShoppingCartItem>();
			}
		}

		public virtual Guid Token { get; set; }
		public virtual bool IsAnonymous { get; set; }
		public virtual bool IsAbandoned { get; set; }
		public virtual string BotUserAgent { get; set; }
		public virtual DateTime LastActivityDate { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.ShoppingCartItem> ShoppingCartItems { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.User User { get; set; }
		public virtual Guid? UserId { get; set; }
    }
}

