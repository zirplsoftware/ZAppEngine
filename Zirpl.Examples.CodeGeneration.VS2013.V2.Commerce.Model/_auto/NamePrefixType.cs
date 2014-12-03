using System;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Extensibility;

namespace Zirpl.Examples.Commerce.Model
{
	public partial class NamePrefixType : 
Zirpl.AppEngine.Model.IPersistable<int>,
Zirpl.AppEngine.Model.IStaticLookup<int,Zirpl.Examples.Commerce.Model.NamePrefixTypeEnum>
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
	}
}
