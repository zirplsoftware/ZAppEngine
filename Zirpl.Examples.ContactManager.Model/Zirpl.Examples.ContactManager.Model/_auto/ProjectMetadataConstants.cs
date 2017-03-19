using System;
using System.Collections;
using System.Collections.Generic;

namespace Zirpl.Examples.ContactManager.Model
{
    public partial class ProjectMetadataConstants
    {
        public const string Id_Name = "Id";
        public const bool Id_IsRequired = false;
        public const long Id_MinValue = long.MinValue;
        public const long Id_MaxValue = long.MaxValue;
        public const string RowVersion_Name = "RowVersion";
        public const bool RowVersion_IsRequired = false;
        public const string CreatedUserId_Name = "CreatedUserId";
        public const bool CreatedUserId_IsRequired = true;
        public const bool CreatedUserId_IsMaxLength = false;
        public const long CreatedUserId_MinLength = 1;
        public const long CreatedUserId_MaxLength = 256;
        public const string UpdatedUserId_Name = "UpdatedUserId";
        public const bool UpdatedUserId_IsRequired = true;
        public const bool UpdatedUserId_IsMaxLength = false;
        public const long UpdatedUserId_MinLength = 1;
        public const long UpdatedUserId_MaxLength = 256;
        public const string CreatedDate_Name = "CreatedDate";
        public const bool CreatedDate_IsRequired = true;
        // TODO: date-times
        public const string UpdatedDate_Name = "UpdatedDate";
        public const bool UpdatedDate_IsRequired = true;
        // TODO: date-times
        public const string Title_Name = "Title";
        public const bool Title_IsRequired = true;
        public const bool Title_IsMaxLength = false;
        public const long Title_MinLength = 0;
        public const long Title_MaxLength = 512;
        public const string SubTitle_Name = "SubTitle";
        public const bool SubTitle_IsRequired = false;
        public const bool SubTitle_IsMaxLength = false;
        public const long SubTitle_MinLength = 0;
        public const long SubTitle_MaxLength = 1024;
        public const string Year_Name = "Year";
        public const bool Year_IsRequired = false;
        public const ushort Year_MinValue = 1900;
        public const ushort Year_MaxValue = ushort.MaxValue;
        public const string Description_Name = "Description";
        public const bool Description_IsRequired = false;
        public const bool Description_IsMaxLength = true;
        public const long Description_MinLength = 0;
        public const long Description_MaxLength = 0;
        public const string UrlSuffix_Name = "UrlSuffix";
        public const bool UrlSuffix_IsRequired = false;
        public const bool UrlSuffix_IsMaxLength = false;
        public const long UrlSuffix_MinLength = 0;
        public const long UrlSuffix_MaxLength = 1024;
        public const string Tags_Name = "Tags";
        public const bool Tags_IsRequired = false;
        // TODO: relationships- ID's already taken care of by above
        public const string Images_Name = "Images";
        public const bool Images_IsRequired = false;
        // TODO: relationships- ID's already taken care of by above
        public const string Urls_Name = "Urls";
        public const bool Urls_IsRequired = false;
        // TODO: relationships- ID's already taken care of by above
    }
}