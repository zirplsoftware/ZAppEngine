using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model
{
    public partial class AddressMetadata : MetadataBase
    {
        public const string Prefix_Name = "Prefix";
		public const bool Prefix_IsRequired = false;

		public const string PrefixId_Name = "PrefixId";
		public const bool PrefixId_IsRequired = false;

        public const string FirstName_Name = "FirstName";
		public const bool FirstName_IsRequired = false;
		public const bool FirstName_IsMaxLength = false;
        public const int FirstName_MinLength = 0;
		public const int FirstName_MaxLength = 150;

        public const string MiddleName_Name = "MiddleName";
		public const bool MiddleName_IsRequired = false;
		public const bool MiddleName_IsMaxLength = false;
        public const int MiddleName_MinLength = 0;
		public const int MiddleName_MaxLength = 150;

        public const string LastName_Name = "LastName";
		public const bool LastName_IsRequired = false;
		public const bool LastName_IsMaxLength = false;
        public const int LastName_MinLength = 0;
		public const int LastName_MaxLength = 150;

        public const string Suffix_Name = "Suffix";
		public const bool Suffix_IsRequired = false;

		public const string SuffixId_Name = "SuffixId";
		public const bool SuffixId_IsRequired = false;

        public const string Position_Name = "Position";
		public const bool Position_IsRequired = false;
		public const bool Position_IsMaxLength = false;
        public const int Position_MinLength = 0;
		public const int Position_MaxLength = 150;

        public const string CompanyName_Name = "CompanyName";
		public const bool CompanyName_IsRequired = false;
		public const bool CompanyName_IsMaxLength = false;
        public const int CompanyName_MinLength = 0;
		public const int CompanyName_MaxLength = 150;

        public const string StreetLine1_Name = "StreetLine1";
		public const bool StreetLine1_IsRequired = false;
		public const bool StreetLine1_IsMaxLength = false;
        public const int StreetLine1_MinLength = 0;
		public const int StreetLine1_MaxLength = 150;

        public const string StreetLine2_Name = "StreetLine2";
		public const bool StreetLine2_IsRequired = false;
		public const bool StreetLine2_IsMaxLength = false;
        public const int StreetLine2_MinLength = 0;
		public const int StreetLine2_MaxLength = 150;

        public const string City_Name = "City";
		public const bool City_IsRequired = false;
		public const bool City_IsMaxLength = false;
        public const int City_MinLength = 0;
		public const int City_MaxLength = 150;

        public const string PostalCode_Name = "PostalCode";
		public const bool PostalCode_IsRequired = false;
		public const bool PostalCode_IsMaxLength = false;
        public const int PostalCode_MinLength = 0;
		public const int PostalCode_MaxLength = 10;

        public const string PhoneNumber_Name = "PhoneNumber";
		public const bool PhoneNumber_IsRequired = false;
		public const bool PhoneNumber_IsMaxLength = false;
        public const int PhoneNumber_MinLength = 0;
		public const int PhoneNumber_MaxLength = 20;

        public const string StateProvinceType_Name = "StateProvinceType";
		public const bool StateProvinceType_IsRequired = false;

		public const string StateProvinceTypeId_Name = "StateProvinceTypeId";
		public const bool StateProvinceTypeId_IsRequired = false;

	}
}
