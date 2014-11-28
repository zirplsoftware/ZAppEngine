using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Settings
{
    public partial class SystemSettingMetadataConstants : MetadataConstantsBase
    {
        public const string Name_Name = "Name";
		public const bool Name_IsRequired = true;
		public const bool Name_IsMaxLength = false;
        public const int Name_MinLength = 1;
		public const int Name_MaxLength = 512;

        public const string Value_Name = "Value";
		public const bool Value_IsRequired = false;
		public const bool Value_IsMaxLength = true;
        public const int Value_MinLength = 0;
		public const int Value_MaxLength = MetadataConstantsBase.MaxLength;

	}
}
