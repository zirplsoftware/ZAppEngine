using System;
using System.Collections;
using System.Collections.Generic;

namespace Zirpl.Examples.ContactManager.Model.Common
{
    public partial class TagMetadataConstants
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
        public const string Name_Name = "Name";
        public const bool Name_IsRequired = true;
        public const bool Name_IsMaxLength = false;
        public const long Name_MinLength = 0;
        public const long Name_MaxLength = 512;
        public const string Description_Name = "Description";
        public const bool Description_IsRequired = false;
        public const bool Description_IsMaxLength = false;
        public const long Description_MinLength = 0;
        public const long Description_MaxLength = 1024;
    }
}