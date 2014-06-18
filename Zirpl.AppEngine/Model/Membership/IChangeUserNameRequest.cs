using System;

namespace Zirpl.AppEngine.Model.Membership
{
    public interface IChangeUserNameRequest
    {
        Guid UserId { get; }
        String NewUserName { get; }
        string NewEmailAddress { get; }
    }
}
