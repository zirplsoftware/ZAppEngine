using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Membership
{

    public partial class UserMetadataConstants
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
