using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class DisplayProductMetadataConstants : MetadataConstantsBase
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

        public const string Sku_Name = "Sku";
		public const bool Sku_IsRequired = false;
		public const bool Sku_IsMaxLength = false;
        public const int Sku_MinLength = 0;
		public const int Sku_MaxLength = 512;

        public const string AdminComment_Name = "AdminComment";
		public const bool AdminComment_IsRequired = false;
		public const bool AdminComment_IsMaxLength = true;
        public const int AdminComment_MinLength = 0;
		public const int AdminComment_MaxLength = MetadataConstantsBase.MaxLength;

        public const string ApplicableDiscounts_Name = "ApplicableDiscounts";
		public const bool ApplicableDiscounts_IsRequired = false;

	}
}
