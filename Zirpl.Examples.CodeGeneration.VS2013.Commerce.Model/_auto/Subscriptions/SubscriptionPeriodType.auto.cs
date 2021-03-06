﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class SubscriptionPeriodType  : DictionaryEntityBase<byte, SubscriptionPeriodTypeEnum>
    {
		public virtual string PluralName { get; set; }
    }
}

