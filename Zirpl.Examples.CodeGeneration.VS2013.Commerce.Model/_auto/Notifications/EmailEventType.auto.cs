using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications
{
    public partial class EmailEventType : DictionaryEntityBase<byte, EmailEventTypeEnum>
    {
		public override string Name { get; set; }
    }
}

