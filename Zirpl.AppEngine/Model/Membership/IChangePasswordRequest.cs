using System;

namespace Zirpl.AppEngine.Model.Membership
{
    public interface IChangePasswordRequest
    {
        Guid UserId { get; }
        String OldPassword { get; }
        String NewPassword { get; }
    }
}
