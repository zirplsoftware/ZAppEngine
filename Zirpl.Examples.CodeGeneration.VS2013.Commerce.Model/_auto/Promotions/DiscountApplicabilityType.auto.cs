using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions
{
    public partial class DiscountApplicabilityType : DictionaryEntityBase<byte, DiscountApplicabilityTypeEnum>
    {
		public override string Name { get; set; }
    }
}

