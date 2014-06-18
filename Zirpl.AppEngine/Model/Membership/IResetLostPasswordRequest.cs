using System;

namespace Zirpl.AppEngine.Model.Membership
{
    public interface IResetLostPasswordRequest
    {
        String NewPassword { get; }
        Guid UserId { get; }
    }
}
