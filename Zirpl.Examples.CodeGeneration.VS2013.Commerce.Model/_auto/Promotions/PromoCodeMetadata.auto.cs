using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions
{
    public partial class PromoCodeMetadata : MetadataBase
    {
        public const string Code_Name = "Code";
		public const bool Code_IsRequired = true;
		public const bool Code_IsMaxLength = false;
        public const int Code_MinLength = 3;
		public const int Code_MaxLength = 150;

	}
}
