using System;

namespace Zirpl.AppEngine.Model.Membership
{
    public class ChangePasswordRequest : IChangePasswordRequest
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordRequestMetadata 
    {
        public const String UserId_Name = "UserId";
        public const String OldPassword_Name = "OldPassword";
        public const String NewPassword_Name = "NewPassword";
    }
}
