using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions
{
    public partial class SubscriptionPeriodTypeMetadataConstants : DictionaryEntityBaseMetadataConstantsBase
    {
        public const string Name_Name = "Name";
		public const bool Name_IsRequired = true;
		public const bool Name_IsMaxLength = false;
        public const int Name_MinLength = 1;
		public const int Name_MaxLength = 100;

        public const string PluralName_Name = "PluralName";
		public const bool PluralName_IsRequired = true;
		public const bool PluralName_IsMaxLength = false;
        public const int PluralName_MinLength = 1;
		public const int PluralName_MaxLength = 100;

	}
}
