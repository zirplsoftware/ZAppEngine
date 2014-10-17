using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings
{
    public partial class StateProvinceType  : DictionaryEntityBase<int, StateProvinceTypeEnum>
    {
		public virtual string Abbreviation { get; set; }
    }
}

