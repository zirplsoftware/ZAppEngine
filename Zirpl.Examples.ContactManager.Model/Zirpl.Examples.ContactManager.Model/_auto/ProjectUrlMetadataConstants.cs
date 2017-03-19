using System;
using System.Collections;
using System.Collections.Generic;

namespace Zirpl.Examples.ContactManager.Model
{
    public partial class ProjectUrlMetadataConstants
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
        public const string Url_Name = "Url";
        public const bool Url_IsRequired = true;
        public const bool Url_IsMaxLength = false;
        public const long Url_MinLength = 0;
        public const long Url_MaxLength = 2048;
        public const string Project_Name = "Project";
        public const bool Project_IsRequired = false;
        // TODO: relationships- ID's already taken care of by above
        public const string ProjectId_Name = "ProjectId";
        public const bool ProjectId_IsRequired = false;
        public const long ProjectId_MinValue = long.MinValue;
        public const long ProjectId_MaxValue = long.MaxValue;
    }
}