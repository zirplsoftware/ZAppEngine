using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class ProductReviewMetadataConstants : MetadataConstantsBase
    {
        public const string DisplayProduct_Name = "DisplayProduct";
		public const bool DisplayProduct_IsRequired = true;

		public const string DisplayProductId_Name = "DisplayProductId";
		public const bool DisplayProductId_IsRequired = true;

        public const string ReviewerName_Name = "ReviewerName";
		public const bool ReviewerName_IsRequired = true;
		public const bool ReviewerName_IsMaxLength = false;
        public const int ReviewerName_MinLength = 1;
		public const int ReviewerName_MaxLength = 512;

        public const string ReviewerLocation_Name = "ReviewerLocation";
		public const bool ReviewerLocation_IsRequired = false;
		public const bool ReviewerLocation_IsMaxLength = false;
        public const int ReviewerLocation_MinLength = 0;
		public const int ReviewerLocation_MaxLength = 256;

        public const string Date_Name = "Date";
		public const bool Date_IsRequired = true;

        public const string Text_Name = "Text";
		public const bool Text_IsRequired = false;
		public const bool Text_IsMaxLength = true;
        public const int Text_MinLength = 0;
		public const int Text_MaxLength = MetadataConstantsBase.MaxLength;

        public const string Stars_Name = "Stars";
		public const bool Stars_IsRequired = true;
		public const int Stars_MinValue = 1;
        public const int Stars_MaxValue = 5;

	}
}
