using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class Brand : AuditableBase<int>
    {
		public virtual string SeoId { get; set; }
		public virtual string Description { get; set; }
    }
}

