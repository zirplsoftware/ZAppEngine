using System;

namespace Zirpl.AppEngine.Model.Membership
{
    public class ChangeUserNameRequest : IChangeUserNameRequest
    {
        public virtual Guid UserId { get; set; }
        public virtual string NewEmailAddress { get; set; }


        public string NewUserName
        {
            get { return this.NewEmailAddress; }
        }
    }

    public class DefaultChangeUserNameRequestMetadata
    {
        public const String UserId_Name = "UserId";
        public const String NewEmailAddress_Name = "NewEmailAddress";
        public const String NewUserName_Name = "NewUserName";
    }

}
