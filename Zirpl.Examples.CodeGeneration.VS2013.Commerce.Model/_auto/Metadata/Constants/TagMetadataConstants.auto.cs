using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants
{
    public partial class TagMetadataConstants : MetadataConstantsBase
    {
        public const string Name_Name = "Name";
		public const bool Name_IsRequired = true;
		public const bool Name_IsMaxLength = false;
        public const int Name_MinLength = 1;
		public const int Name_MaxLength = 512;

        public const string SeoId_Name = "SeoId";
		public const bool SeoId_IsRequired = false;
		public const bool SeoId_IsMaxLength = false;
        public const int SeoId_MinLength = 0;
		public const int SeoId_MaxLength = 512;

        public const string Description_Name = "Description";
		public const bool Description_IsRequired = false;
		public const bool Description_IsMaxLength = true;
        public const int Description_MinLength = 0;
		public const int Description_MaxLength = MetadataConstantsBase.MaxLength;

	}
}
