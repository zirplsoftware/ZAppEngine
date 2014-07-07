using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class Category : AuditableBase<int>
    {
		public virtual string SeoId { get; set; }
		public virtual string Description { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.Category Parent { get; set; }
		public virtual int? ParentId { get; set; }
    }
}

