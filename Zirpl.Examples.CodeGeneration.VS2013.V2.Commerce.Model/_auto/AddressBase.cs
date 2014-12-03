using System;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Extensibility;

namespace Zirpl.Examples.Commerce.Model
{
	public abstract partial class AddressBase : 
Zirpl.AppEngine.Model.IPersistable<Guid>,
Zirpl.AppEngine.Model.IAuditable,
Zirpl.AppEngine.Model.Extensibility.IExtensible<Zirpl.Examples.Commerce.Model.AddressBase,Zirpl.Examples.Commerce.Model.AddressBaseExtendedFieldValue,Guid>,
Zirpl.AppEngine.Model.IVersionable
	{
		public virtual Guid Id { get; set; }
		public virtual byte[] RowVersion { get; set; }
		public virtual string CreatedUserId { get; set; }
		public virtual string UpdatedUserId { get; set; }
		public virtual DateTime? CreatedDate { get; set; }
		public virtual DateTime? UpdatedDate { get; set; }
		public virtual System.Collections.Generic.IList<Zirpl.Examples.Commerce.Model.AddressBaseExtendedFieldValue> ExtendedFieldValues { get; set; }
	}
}
