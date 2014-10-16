using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class OrderStatusTypeMetadataConstants : DictionaryEntityBaseMetadataConstantsBase
    {
        public const string Name_Name = "Name";
		public const bool Name_IsRequired = true;
		public const bool Name_IsMaxLength = false;
        public const int Name_MinLength = 1;
		public const int Name_MaxLength = 100;

        public const string CustomerFacingName_Name = "CustomerFacingName";
		public const bool CustomerFacingName_IsRequired = true;
		public const bool CustomerFacingName_IsMaxLength = false;
        public const int CustomerFacingName_MinLength = 1;
		public const int CustomerFacingName_MaxLength = 100;

	}
}
