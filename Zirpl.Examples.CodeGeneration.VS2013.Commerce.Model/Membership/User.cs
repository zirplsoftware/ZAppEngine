using System;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership
{
    public class User : PersistableBase<Guid>
    {
        public virtual String UserName { get; set; }
        public virtual String LoweredUserName { get; set; }

        //public Guid ApplicationId { get; set; }
        //public bool IsAnonymous { get; set; }
        //public DateTime LastActivityDate { get; set; }
    }

    public class UserMetadata : MetadataBase
    {
        public const String UserName_Name = "UserName";
        public const int UserName_MaxLength = 256;
        public const bool UserName_IsMaxLength = false;
        public const bool UserName_IsRequired = true;

        public const String LoweredUserName_Name = "LoweredUserName";
        public const int LoweredUserName_MaxLength = 256;
        public const bool LoweredUserName_IsMaxLength = false;
        public const bool LoweredUserName_IsRequired = false;

        public const int Password_MinLength = 6;
        public const int Password_MaxLength = 128;
    }
}
