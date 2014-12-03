using System;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Extensibility;

namespace Zirpl.Examples.Commerce.Model
{
	public partial class AddressBaseExtendedFieldValue : 
Zirpl.AppEngine.Model.IPersistable<Guid>,
Zirpl.AppEngine.Model.Extensibility.IExtendedEntityFieldValue<Zirpl.Examples.Commerce.Model.AddressBase,Guid>,
Zirpl.AppEngine.Model.IVersionable
	{
		public virtual Guid Id { get; set; }
		public virtual byte[] RowVersion { get; set; }
		public virtual Zirpl.Examples.Commerce.Model.AddressBase ExtendedEntity { get; set; }
		public virtual Guid ExtendedEntityId { get; set; }
		public virtual string Value { get; set; }
	}
}
